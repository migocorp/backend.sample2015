﻿[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Using Win32 naming for consistency.")]

namespace Sample2015.Core.DAL.Repo
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.DAL.DbContextScope;

    public abstract class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity> where TDbContext : DbContext where TEntity : class
    {
        private readonly IAmbientDbContextLocator ambientDbContextLocator;

        public GenericRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            this.ambientDbContextLocator = ambientDbContextLocator;
        }

        public virtual TDbContext DbContext
        {
            get
            {
                var dbContext = this.ambientDbContextLocator.Get<TDbContext>();
                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type " + typeof(TDbContext).Name + " found.");
                }
                
                return dbContext;
            }
        }

        public virtual DbSet<TEntity> DbSet
        {
            get
            {
                var dbContext = this.DbContext;
                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type " + typeof(TDbContext).Name + " found.");
                }

                return dbContext.Set<TEntity>();
            }
        }

        public virtual IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this.DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public virtual void DeleteByID(object id)
        {
            TEntity entityToDelete = this.DbSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.DbSet.Attach(entityToDelete);
            }

            this.DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this.DbSet.Attach(entityToUpdate);
            this.DbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
