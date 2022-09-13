using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TweetApp.Common;
using TweetApp.Models;
using TweetApp.Models.Requests;
using TweetApp.Services.Interfaces;

namespace TweetApp.Controllers
{
    [Route(Constants.BaseURL)]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly ILogger<TweetsController> _logger;
        private readonly IUserService _userService;
        private readonly ITweetService _tweetService;
        private readonly IReplyService _replyService;
        public TweetsController(ILogger<TweetsController> logger, IUserService userService, ITweetService tweetService, IReplyService replyService)
        {
            _logger = logger;
            _userService = userService;
            _tweetService = tweetService;
            _replyService = replyService;
        }

        /// <summary>
        /// API to register a user on TweetApp.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost(Constants.RegisterUser)]
        public async Task<ActionResult> RegisterUser([FromBody] UserRequest user)
        {
            try
            {
                var userId = await _userService.RegisterUser(user);

                if (!string.IsNullOrEmpty(userId))
                {
                    var message = string.Format(Constants.UserCreated, user.UserName);
                    return Created(userId, message);
                }

                return BadRequest(Constants.ExistingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.RegistrationError, user.UserName, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to login TweetApp user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>JWT.</returns>
        [HttpGet(Constants.Login)]
        public async Task<ActionResult> LoginUser([FromQuery, Required] string username, [FromQuery, Required] string password)
        {
            try
            {
                var credentials = new Credentials()
                {
                    UserName = username,
                    Password = password
                };

                var (token, userName) = await _userService.LoginUser(credentials);

                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(userName))
                {
                    return Ok(new { Message = Constants.LoginSuccess, JWToken = token, UserName = userName });
                }

                return BadRequest(Constants.InvalidCredentials);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.LoginError, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to reset password.
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        [HttpPut(Constants.ResetPassword)]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var passwordUpdated = await _userService.ResetPassword(resetPasswordRequest);

                if (passwordUpdated)
                {
                    return Ok(Constants.UpdatedPassword);
                }

                return BadRequest(Constants.FailedUpdatingPassword);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(Constants.ResetPasswordError, resetPasswordRequest.EmailId, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to get all the tweets of TweetApp.
        /// </summary>
        /// <returns>List of all the tweets.</returns>
        [HttpGet(Constants.AllTweets)]
        public async Task<ActionResult> GetAllTweets()
        {
            try
            {
                var tweets = await _tweetService.GetAllTweets();
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GetAllTweetsError, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to retrieve all the TweetApp users.
        /// </summary>
        /// <returns>List of all the users.</returns>
        [HttpGet(Constants.AllUsers)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GetAllUsersError, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to lookup TweetApp users.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List of users matching input.</returns>
        [HttpGet(Constants.SearchUser)]
        public async Task<ActionResult> SearchUser([FromRoute, MinLength(3)] string username)
        {
            try
            {
                var users = await _userService.SearchUserByUserName(username);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.SearchUserError, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to get all the tweets of a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List of all the tweets of a user.</returns>
        [HttpGet(Constants.AllTweetsOfUser)]
        public async Task<ActionResult> GetAllTweetsOfUser([FromRoute] string username)
        {
            try
            {
                var tweets = await _tweetService.GetAllTweetsOfUser(username);

                if (tweets != null)
                {
                    return Ok(tweets);
                }

                return BadRequest(Constants.FailedGettingTweets);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GetAllTweetsOfUserError, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to post a tweet.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="tweet"></param>
        /// <returns></returns>
        [HttpPost(Constants.AddTweet)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostTweet([FromRoute] string username, [FromBody] TweetRequest tweet)
        {
            try
            {
                var tweetid = await _tweetService.PostTweet(username, tweet);

                if (!string.IsNullOrEmpty(tweetid))
                {
                    var message = string.Format(Constants.TweetAdded, username);
                    return Created(tweetid, message);
                }

                return BadRequest(Constants.FailedPostingTweet);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.AddingTweetError, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to update a tweet.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="tweetid"></param>
        /// <param name="tweet"></param>
        /// <returns></returns>
        [HttpPut(Constants.UpdateTweet)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateTweet([FromRoute] string username, [FromRoute] string tweetid, [FromBody] TweetRequest tweet)
        {
            try
            {
                var updatedTweet = await _tweetService.UpdateTweet(username, tweetid, tweet);

                if (updatedTweet != null)
                {
                    return Ok(updatedTweet);
                }

                return BadRequest(Constants.FailedUpdatingTweet);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.UpdateTweetError, tweetid, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to delete a tweet.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="tweetid"></param>
        /// <returns></returns>
        [HttpDelete(Constants.DeleteTweet)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteTweet([FromRoute] string username, [FromRoute] string tweetid)
        {
            try
            {
                var deleted = await _tweetService.DeleteTweet(username, tweetid);

                if (deleted)
                {
                    return Ok(Constants.TweetDeleted);
                }

                return BadRequest(Constants.FailedDeletingTweet);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(Constants.DeleteTweetError, tweetid, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to like a tweet.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="tweetid"></param>
        /// <returns></returns>
        [HttpPut(Constants.LikeTweet)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> LikeTweet([FromRoute] string username, [FromRoute] string tweetid)
        {
            try
            {
                var tweet = await _tweetService.LikeTweet(username, tweetid);

                if (tweet != null)
                {
                    return Ok(tweet);
                }

                return BadRequest(Constants.FailedLikingTweet);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.LikingTweetError, tweetid, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to add reply to a tweet.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="tweetid"></param>
        /// <param name="reply"></param>
        /// <returns></returns>
        [HttpPost(Constants.AddReply)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddReplyToTweet([FromRoute] string username, [FromRoute] string tweetid, [FromBody] ReplyRequest reply)
        {
            try
            {
                var replyid = await _replyService.AddReplyToTweet(username, tweetid, reply);

                if (!string.IsNullOrEmpty(replyid))
                {
                    var message = string.Format(Constants.ReplyAdded);
                    return Created(replyid, message);
                }

                return BadRequest(Constants.FailedAddingReply);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.AddReplyError, tweetid, username, ex.Message, ex.StackTrace);
                return Problem();
            }
        }

        /// <summary>
        /// API to get a tweet by tweetid.
        /// </summary>
        /// <param name="tweetid"></param>
        /// <returns>return a tweet.</returns>
        [HttpGet(Constants.GetTweet)]
        public async Task<ActionResult> GetTweetById([FromRoute] string tweetid)
        {
            try
            {
                var tweet = await _tweetService.GetTweetById(tweetid);

                if (tweet != null)
                {
                    return Ok(tweet);
                }

                return BadRequest(Constants.FailedGettingTweet);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(Constants.GetTweetError, tweetid, ex.Message, ex.StackTrace);
                return Problem();
            }
        }
    }
}
