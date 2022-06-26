using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TweetApp.Common;
using TweetApp.Models.DataModels;
using TweetApp.Models.Requests;
using TweetApp.Repositories.Interfaces;
using TweetApp.Services.Interfaces;

namespace TweetApp.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IReplyRepository _replyRepository;
        private readonly ILogger<ReplyService> _logger;
        private readonly ITweetRepository _tweetRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public ReplyService(IReplyRepository replyRepository, ILogger<ReplyService> logger, ITweetRepository tweetRepository, IMapper mapper, IAuthService authService)
        {
            _replyRepository = replyRepository;
            _logger = logger;
            _tweetRepository = tweetRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<string> AddReplyToTweet(string username, string tweetid, ReplyRequest reply)
        {
            _logger.LogInformation(Constants.RetrievingUserNameFromToken);

            var usernameFromToken = _authService.GetUserNameFromToken();

            if (usernameFromToken.Equals(username))
            {
                _logger.LogInformation(Constants.RetrievingTweetById, tweetid);

                var tweet = await _tweetRepository.GetTweetById(tweetid);

                if (tweet != null)
                {
                    _logger.LogInformation(Constants.TweetRetrievedById, tweetid);
                    _logger.LogInformation(Constants.AddingReplyToTweet, tweetid, username);

                    var newReply = _mapper.Map<ReplyRequest, Reply>(reply);
                    newReply.TweetId = tweetid;
                    newReply.CreatedBy = username;

                    await _replyRepository.AddReply(newReply);

                    _logger.LogInformation(Constants.ReplyAddedToTweet, tweetid, username);

                    return newReply.ReplyId.ToString();
                }

                _logger.LogInformation(Constants.TweetNotFound, tweetid);
                return null;
            }

            _logger.LogInformation(Constants.Unauthorized, username, usernameFromToken);
            return null;
        }
    }
}
