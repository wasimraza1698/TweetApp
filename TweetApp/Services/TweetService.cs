using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Common;
using TweetApp.Models.DataModels;
using TweetApp.Models.Requests;
using TweetApp.Models.Responses;
using TweetApp.Repositories.Interfaces;
using TweetApp.Services.Interfaces;

namespace TweetApp.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly ILogger<TweetService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IReplyRepository _replyRepository;
        private readonly IUserService _userService;
        public TweetService(ITweetRepository tweetRepository, ILogger<TweetService> logger, IMapper mapper, IAuthService authService, IReplyRepository replyRepository, IUserService userService)
        {
            _tweetRepository = tweetRepository;
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
            _replyRepository = replyRepository;
            _userService = userService;
        }
        public async Task<string> PostTweet(string username, TweetRequest tweet)
        {
            _logger.LogInformation(Constants.RetrievingUserNameFromToken);

            var usernameFromToken = _authService.GetUserNameFromToken();

            if (usernameFromToken.Equals(username))
            {
                _logger.LogInformation(Constants.AddingTweet, username);

                var newTweet = _mapper.Map<TweetRequest, Tweet>(tweet);
                newTweet.CreatedBy = username;
                await _tweetRepository.AddTweet(newTweet);

                _logger.LogInformation(Constants.TweetAddedByUser, username);

                return newTweet.TweetId.ToString();
            }

            _logger.LogInformation(Constants.Unauthorized, username, usernameFromToken);
            return null;
        }

        public async Task<TweetResponse> UpdateTweet(string username, string tweetid, TweetRequest tweet)
        {
            _logger.LogInformation(Constants.RetrievingUserNameFromToken);

            var usernameFromToken = _authService.GetUserNameFromToken();

            if(usernameFromToken == username)
            {
                _logger.LogInformation(Constants.RetrievingTweetById, tweetid);

                var existingTweet = await _tweetRepository.GetTweetById(tweetid);

                if (existingTweet != null)
                {
                    _logger.LogInformation(Constants.TweetRetrievedById, tweetid);

                    if (existingTweet.CreatedBy == username)
                    {
                        existingTweet.TweetText = tweet.TweetText;
                        existingTweet.TweetTag = tweet.TweetTag;
                        existingTweet.UpdatedBy = username;
                        existingTweet.UpdatedOn = DateTime.Now;

                        _logger.LogInformation(Constants.UpdatingTweetById, tweetid);

                        await _tweetRepository.UpdateTweet(existingTweet);

                        _logger.LogInformation(Constants.UpdatedTweetById, tweetid);

                        var updatedTweet = _mapper.Map<Tweet, TweetResponse>(existingTweet);

                        _logger.LogInformation(Constants.MapppingRepliesToTweet, tweetid);
                        updatedTweet.Replies = await GetRepliesByTweetId(tweetid);
                        _logger.LogInformation(Constants.MappedRepliesToTweet, tweetid);

                        return updatedTweet;
                    }

                    _logger.LogInformation(Constants.UnauthorizedTweetUpdate, tweetid, existingTweet.CreatedBy);
                    return null;
                }

                _logger.LogInformation(Constants.TweetNotFound, tweetid);
                return null;
            }

            _logger.LogInformation(Constants.Unauthorized, username, usernameFromToken);
            return null;
        }

        public async Task<bool> DeleteTweet(string username, string tweetid)
        {
            _logger.LogInformation(Constants.RetrievingUserNameFromToken);

            var usernameFromToken = _authService.GetUserNameFromToken();

            if (usernameFromToken == username)
            {
                _logger.LogInformation(Constants.RetrievingTweetById, tweetid);

                var tweet = await _tweetRepository.GetTweetById(tweetid);

                if (tweet != null)
                {
                    _logger.LogInformation(Constants.TweetRetrievedById, tweetid);

                    if (tweet.CreatedBy == username)
                    {
                        _logger.LogInformation(Constants.DeletedTweetById, tweetid);

                        await _tweetRepository.DeleteTweet(tweetid);

                        _logger.LogInformation(Constants.DeletedTweetById, tweetid);
                        return true;
                    }

                    _logger.LogInformation(Constants.UnauthorizedTweetDelete, tweetid, tweet.CreatedBy);
                    return false;
                }

                _logger.LogInformation(Constants.TweetNotFound, tweetid);
                return false;
            }

            _logger.LogInformation(Constants.Unauthorized, username, usernameFromToken);
            return false;
        }

        public async Task<TweetResponse> LikeTweet(string username, string tweetid)
        {
            _logger.LogInformation(Constants.RetrievingUserNameFromToken);

            var usernameFromToken = _authService.GetUserNameFromToken();

            if (usernameFromToken == username)
            {
                _logger.LogInformation(Constants.RetrievingTweetById, tweetid);

                var tweet = await _tweetRepository.GetTweetById(tweetid);

                if (tweet != null)
                {
                    _logger.LogInformation(Constants.TweetRetrievedById, tweetid);

                    if (tweet.TweetLikedBy.Contains(username))
                    {
                        tweet.TweetLikesCount -= 1;
                        tweet.TweetLikedBy.Remove(username);
                    }
                    else
                    {
                        tweet.TweetLikesCount += 1;
                        tweet.TweetLikedBy.Add(username);
                    }

                    tweet.UpdatedBy = username;
                    tweet.UpdatedOn = DateTime.Now;

                    _logger.LogInformation(Constants.UpdatingTweetById, tweetid);

                    await _tweetRepository.UpdateTweet(tweet);

                    _logger.LogInformation(Constants.UpdatedTweetById, tweetid);

                    var tweetResponse = _mapper.Map<Tweet, TweetResponse>(tweet);

                    _logger.LogInformation(Constants.MapppingRepliesToTweet, tweetid);
                    tweetResponse.Replies = await GetRepliesByTweetId(tweetid);
                    _logger.LogInformation(Constants.MappedRepliesToTweet, tweetid);

                    return tweetResponse;
                }

                _logger.LogInformation(Constants.TweetNotFound, tweetid);
                return null;
            }

            _logger.LogInformation(Constants.Unauthorized, username, usernameFromToken);
            return null;
        }

        public async Task<List<TweetResponse>> GetAllTweets()
        {
            _logger.LogInformation(Constants.RetrievingAllTweets);

            var tweets = await _tweetRepository.GetAllTweets();
            var tweetsResponse = _mapper.Map<List<Tweet>, List<TweetResponse>>(tweets);

            _logger.LogInformation(Constants.AllTweetsRetrieved);
            _logger.LogInformation(Constants.MappingRepliesForAllTweets);

            for (int i = 0; i < tweetsResponse.Count; i++)
            {
                tweetsResponse[i].Replies = await GetRepliesByTweetId(tweetsResponse[i].TweetId);
            }

            _logger.LogInformation(Constants.RepliesMappedForAllTweets);

            return tweetsResponse;
        }

        public async Task<List<TweetResponse>> GetAllTweetsOfUser(string username)
        {
            _logger.LogInformation(Constants.SearchingUserByUserName, username);
            var users = await _userService.SearchUserByUserName(username);
            var isValidUsername = users.Exists(u => u.UserName == username);

            if (isValidUsername)
            {
                _logger.LogInformation(Constants.RetrievingAllTweetsOfUser, username);

                var tweets = await _tweetRepository.GetAllTweetsOfUser(username);

                if (tweets == null)
                {
                    return new List<TweetResponse>();
                }

                var tweetsResponse = _mapper.Map<List<Tweet>, List<TweetResponse>>(tweets);

                _logger.LogInformation(Constants.AllTweetsRetrievedOfUser, username);
                _logger.LogInformation(Constants.MappingRepliesForAllTweetsOfUser, username);

                for (int i = 0; i < tweetsResponse.Count; i++)
                {
                    tweetsResponse[i].Replies = await GetRepliesByTweetId(tweetsResponse[i].TweetId);
                }

                _logger.LogInformation(Constants.RepliesMappedForAllTweetsOfUser, username);

                return tweetsResponse;
            }

            _logger.LogInformation(Constants.UserNotFoundByUserName, username);
            return null;
        }

        public async Task<TweetResponse> GetTweetById(string tweetid)
        {
            _logger.LogInformation(Constants.RetrievingTweetById, tweetid);
            var tweet = await _tweetRepository.GetTweetById(tweetid);

            if (tweet != null)
            {
                _logger.LogInformation(Constants.TweetRetrievedById, tweetid);

                var response = _mapper.Map<Tweet, TweetResponse>(tweet);
                response.Replies = await GetRepliesByTweetId(tweetid);

                return response;
            }

            _logger.LogInformation(Constants.TweetNotFound, tweetid);
            return null;
        }

        private async Task<List<ReplyResponse>> GetRepliesByTweetId(string tweetid)
        {
            _logger.LogInformation(Constants.GettingRepliesByTweetId, tweetid);

            var replies = await _replyRepository.GetRepliesOfTweet(tweetid);

            _logger.LogInformation(Constants.FetchedRepliesByTweetId, tweetid);
            return _mapper.Map<List<Reply>, List<ReplyResponse>>(replies);
        }
    }
}
