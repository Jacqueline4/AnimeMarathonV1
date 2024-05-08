using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces.Base
{
    public interface IBaseServices<T> where T : BaseEntity
    {
        Task<T> Create(T entity);
        Task Delete(int id);
    }
}
