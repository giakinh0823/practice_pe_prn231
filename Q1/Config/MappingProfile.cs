using AutoMapper;
using Q1.Dto;
using Q1.Models;

namespace Q1.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<StarRequest, Star>();
            CreateMap<Star, StarResponse>().ForMember(dest => dest.Movies, src => src.MapFrom(star => star.Movies != null ? star.Movies.Select(movie => new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                DirectorId = movie.DirectorId,
                Language = movie.Language,
                ProducerId = movie.ProducerId,
                ReleaseDate = movie.ReleaseDate,
                Genres = new List<Genre>(),
                Stars = new List<Star>()
            }) : new List<MovieResponse>()));
            CreateMap<Movie, MovieResponse>();
        }
    }
}
