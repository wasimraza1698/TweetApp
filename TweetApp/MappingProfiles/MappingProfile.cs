using AutoMapper;
using TweetApp.Models.DataModels;
using TweetApp.Models.Requests;
using TweetApp.Models.Responses;
using TweetApp.Services.Interfaces;

namespace TweetApp.MappingProfiles
{
    public class MappingProfile : Profile
    {
        private readonly IAuthService _authService;
        public MappingProfile(IAuthService authService)
        {
            _authService = authService;

            var username = _authService.GetUserNameFromToken();

            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>();

            CreateMap<TweetRequest, Tweet>();

            CreateMap<ReplyRequest, Reply>();

            CreateMap<Tweet, TweetResponse>()
                .ForMember(dest => dest.TweetedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.TweetedAt, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.TweetLiked, opt => opt.MapFrom(src => src.TweetLikedBy.Contains(username)));

            CreateMap<Reply, ReplyResponse>()
                .ForMember(dest => dest.RepliedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.RepliedAt, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.ReplyLiked, opt => opt.MapFrom(src => src.ReplyLikedBy.Contains(username)));
        }
    }
}
