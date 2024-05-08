using AnimeMarahon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryList();
        Task<IEnumerable<Category>> GetCategoryByName(string category);
        Task<Category> Create(Category category);
        Task Update(Category category);
        Task Delete(int categoryId);
    }
}
