using Bank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bank.Core.Repositories
{
    public class Specification<TEntity> : ISpecification<TEntity> where TEntity : Entity
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public IReadOnlyCollection<string> Includes { get; }

        public Specification() : this(criteria: null, null)
        {
        }

        public Specification(Expression<Func<TEntity, bool>> criteria) : this(criteria, null)
        {
        }

        public Specification(params string[] includes) : this(null, includes)
        {
        }

        public Specification(Expression<Func<TEntity, bool>> criteria = null, params string[] includes)
        {
            Criteria = criteria;
            Includes = includes;
        }
    }
}
