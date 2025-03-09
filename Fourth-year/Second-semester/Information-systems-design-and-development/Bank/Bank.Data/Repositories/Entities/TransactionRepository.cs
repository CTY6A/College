using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class TransactionRepository : Repository<Transaction, Guid>, ITransactionRepository
    {
        public TransactionRepository(BankContext context) : base(context)
        {
        }
    }
}
