using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class CreditRepository : Repository<Credit, Guid>, ICreditRepository
    {
        public CreditRepository(BankContext context) : base(context)
        {
        }
    }
}
