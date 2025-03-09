using Bank.Core.Entities;
using System.Collections.Generic;

namespace Bank.Core.Repositories
{
    public interface IRepository<TEntity> : IRepository where TEntity: Entity
    {
        TEntity Find(ISpecification<TEntity> specification);
        IEnumerable<TEntity> List(ISpecification<TEntity> specification = null);

        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
