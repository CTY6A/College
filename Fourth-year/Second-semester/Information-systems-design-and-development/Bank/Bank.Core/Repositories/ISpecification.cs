using Bank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bank.Core.Repositories
{
    public interface ISpecification<TEntity> where TEntity : Entity
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        IReadOnlyCollection<string> Includes { get; }
    }
}
