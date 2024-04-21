using AnimeMarahon.Core.Entities;
using AnimeMarathonV1.DTOs;
using AutoMapper;

namespace AnimeMarathonV1.Mapper
{
    public class AnimeMapperProfiles : Profile
    {
        public AnimeMapperProfiles() 
        {
            CreateMap<AnimeDTO,Anime>().ReverseMap();
        }
    }
}
