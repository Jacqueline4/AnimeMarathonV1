using AnimeMarahon.Core.Entities.Base;
using AnimeMarahon.Core.Repositories.Base;
using AnimeMarathon.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Services.Base
{
    public class BaseServices<T> : IBaseServices<T> where T : BaseEntity
    {
        private readonly IRepository<T> repository;

        public BaseServices(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual async Task<T> Create(T entity)
        {
            //await ValidateEntityIfNotExist(entity);

            var newEntity = await repository.AddAsync(entity);
            return newEntity;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
                throw new ApplicationException($"Entity with id {id} not found.");

            await repository.DeleteAsync(entity);
        }

        //protected virtual async Task ValidateEntityIfNotExist(T entity)
        //{
        //    var existingEntity = await repository.GetByIdAsync(entity.Id);
        //    if (existingEntity != null)
        //        throw new ApplicationException($"Entity with id {entity.Id} already exists.");
        //}
    }
}
