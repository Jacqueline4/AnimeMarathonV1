using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Entities.Base;
using AnimeMarathon.Application.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces.Base
{
    public interface IBaseServices<TDto, TEntity>
        where TDto : BaseDTO
        where TEntity : BaseEntity

    {
        Task<TDto> Create(TDto entity);
        Task<IEnumerable<TDto>> GetList();
        Task Delete(int id);
    }
}
