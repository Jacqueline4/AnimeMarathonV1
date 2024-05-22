using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoryList();
        Task<IEnumerable<CategoryDTO>> GetCategoryByAnimeId(int animeId);
        Task<CategoryDTO> Create(CategoryDTO category);
        Task Update(CategoryDTO category);
        Task Delete(int categoryId);
    }
}
