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
  
    public class CategoryMapperProfiles : Profile
    {
        public CategoryMapperProfiles()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
    public class AnimeCategoryMapperProfiles : Profile
    {
        public AnimeCategoryMapperProfiles()
        {
            CreateMap<AnimeCategoryDTO, AnimeCategory>().ReverseMap();
        }
    }
}
