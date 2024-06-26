﻿using AnimeMarahon.Core.Entities.Base;
using AnimeMarahon.Core.Repositories.Base;
using AnimeMarathon.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Data.Repository.Base
{
    public  class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AnimeMarathonContext dbContext;

        public Repository(AnimeMarathonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return dbContext.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<IReadOnlyList<T>> GetAllPaginatedAsync(int top = 10, int skip = 0)
        {
            return await dbContext.Set<T>().Take(top).Skip(skip).AsNoTracking().ToListAsync();

        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
