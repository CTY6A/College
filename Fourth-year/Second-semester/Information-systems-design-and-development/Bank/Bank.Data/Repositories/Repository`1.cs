using Bank.Common;
using Bank.Core.Entities;
using Bank.Core.Exceptions;
using Bank.Core.Repositories;
using Bank.Data.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Data.Repositories
{
    internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly BankContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(BankContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public TEntity Find(ISpecification<TEntity> specification)
        {
            Expect.ArgumentNotNull(specification, nameof(specification));

            try
            {
                return Apply(specification).SingleOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public IEnumerable<TEntity> List(ISpecification<TEntity> specification = null)
        {
            try
            {
                return Apply(specification).ToList();
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public void Add(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, nameof(entity));

            try
            {
                DbSet.Add(entity);
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public void Remove(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, nameof(entity));

            try
            {
                DbSet.Remove(entity);
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        protected IQueryable<TEntity> Apply(ISpecification<TEntity> specification)
        {
            var query = DbSet.AsQueryable();

            if (specification == null)
            {
                return query;
            }

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.Includes?.Count > 0)
            {
                query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
