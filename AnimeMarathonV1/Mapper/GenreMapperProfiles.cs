using AnimeMarahon.Core.Entities;
using AnimeMarathonV1.DTOs;
using AutoMapper;

namespace AnimeMarathonV1.Mapper
{
    public class GenreMapperProfiles : Profile
    {
        public GenreMapperProfiles()
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
        }
    }
    public class AnimeGenreMapperProfiles : Profile
    {
        public AnimeGenreMapperProfiles()
        {
            CreateMap<AnimeGenreDTO, AnimeGenre>().ReverseMap();
        }
    }
}
