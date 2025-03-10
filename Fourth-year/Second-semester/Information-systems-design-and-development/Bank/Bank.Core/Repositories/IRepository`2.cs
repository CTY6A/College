﻿using Bank.Core.Entities;

namespace Bank.Core.Repositories
{
    public interface IRepository<TEntity, in TKey> : IRepository<TEntity> where TEntity : Entity<TKey>
    {
        TEntity Find(TKey id);
    }
}
