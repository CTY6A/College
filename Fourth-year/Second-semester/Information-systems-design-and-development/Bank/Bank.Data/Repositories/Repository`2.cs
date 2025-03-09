using Bank.Core.Entities;
using Bank.Core.Exceptions;
using Bank.Core.Repositories;
using Bank.Data.Helpers;
using Microsoft.Data.SqlClient;
using System;

namespace Bank.Data.Repositories
{
    internal abstract class Repository<TEntity, TKey> : Repository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        protected Repository(BankContext context) : base(context)
        {
        }

        public virtual TEntity Find(TKey id)
        {
            try
            {
                return DbSet.Find(id);
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
    }
}
