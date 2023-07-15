using AutoMapper;
using PE_BEST_PRACTICE.Dto;
using PE_BEST_PRACTICE.Models;

namespace PE_BEST_PRACTICE.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // chuyển từ director sang director response
            CreateMap<Director, DirectorResponse>()
               .ForMember(dto => dto.Movies,
               opt => opt.MapFrom(source => source.Movies != null ? source.Movies.ToList().Select(mv => mv.Title).ToList() : null));

            // chuyển từ request sang director để tạo
            CreateMap<DirectorRequest, Director>();
        }
    }
}
