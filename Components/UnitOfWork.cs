using System;
using System.Data.Entity;

using EntityRepository.Interfaces;
using EntityRepository.Models;

namespace EntityRepository.Components
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext: DbContext
    {
        private readonly TDbContext context;

        public UnitOfWork()
        {
            context = Activator.CreateInstance<TDbContext>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            var repository = new EntityRepository<TEntity, TDbContext>(context);

            return repository;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}