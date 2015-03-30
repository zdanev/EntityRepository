using System;
using System.Data.Entity;
using System.Linq;

using EntityRepository.Interfaces;
using EntityRepository.Models;

namespace EntityRepository.Components
{
    public class EntityRepository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : Entity 
        where TDbContext: DbContext
    {
        private readonly TDbContext context;
        private readonly DbSet<TEntity> set;

        public EntityRepository(TDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
            set = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity is TrackedEntity)
            {
                var track = entity as TrackedEntity;
                track.CreatedBy = UserContext.Current.UserName;
                track.CreateTime = UserContext.Current.CurrentTime;
            }

            set.Add(entity);
        }

        public IQueryable<TEntity> Query()
        {
            return set.AsQueryable();
        }

        public TEntity Get(Guid id)
        {
            return set.SingleOrDefault(e => e.Id == id);
        }

        public void Update(TEntity entity)
        {
            if (entity is TrackedEntity)
            {
                var track = entity as TrackedEntity;
                track.ModifiedBy = UserContext.Current.UserName;
                track.ModifiedTime = UserContext.Current.CurrentTime;
            }

            var original = Get(entity.Id);

            context.Entry(original).CurrentValues.SetValues(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity is TrackedEntity)
            {
                set.Attach(entity);

                var track = entity as TrackedEntity;
                track.IsDeleted = true;
                track.ModifiedBy = UserContext.Current.UserName;
                track.ModifiedTime = UserContext.Current.CurrentTime;
            }
            else
            {
                set.Remove(entity);
            }
        }

        public int Count()
        {
            return set.Count();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}