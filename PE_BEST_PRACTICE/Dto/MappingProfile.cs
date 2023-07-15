using AutoMapper;
using PE_BEST_PRACTICE.Models;

namespace PE_BEST_PRACTICE.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Director, DirectorResponse>()
                .ForMember(dto => dto.Movies,
                opt => opt.MapFrom(source => source.Movies != null ? source.Movies.ToList().Select(mv => mv.Title).ToList() : null));
        }
    }
}
