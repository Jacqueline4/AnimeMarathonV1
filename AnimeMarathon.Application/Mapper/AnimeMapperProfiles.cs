using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;

namespace AnimeMarathon.Application.Services.Mapper
{
    public class AnimeMapperProfiles : Profile
    {
        public AnimeMapperProfiles()
        {
            CreateMap<AnimeDTO, Anime>().ReverseMap();
            //CreateMap<IEnumerable<Anime>, IEnumerable<AnimeDTO>>();
            //CreateMap<IList<Anime>, IList<AnimeDTO>>();
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
    public class UserMapperProfiles : Profile
    {
        public UserMapperProfiles()
        {
            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<IEnumerable<User>, IEnumerable<UserDTO>>();
        }
    }
    public class UserAnimeMapperProfiles : Profile
    {
        public UserAnimeMapperProfiles()
        {
            CreateMap<UsersAnimeDTO, UsersAnimes>().ReverseMap();
            //CreateMap<IEnumerable<UsersAnimes>, IEnumerable<UsersAnimeDTO>>();
        }
    }

    public class AutoMapperDiscorveryProfile
    {

    }
}
