using AutoMapper;
using Core.Domain;
using WebApi.Models;
using WebApi.Models.Post;

namespace WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostInterest, ReadInterestModel>()
                 .ForMember(dest => dest.Id, opts => opts.MapFrom(source => source.Interest.Id))
                 .ForMember(dest => dest.Name , opts => opts.MapFrom(source => source.Interest.Name))
                 .ForMember(dest => dest.ThumbnailImgUrl, opts => opts.MapFrom(source => source.Interest.ThumbnailImgUrl));

            CreateMap<Post, ReadPostModel>()
                .ForMember(dest => dest.Interests, opts => opts.Ignore());
        }
    }
}
