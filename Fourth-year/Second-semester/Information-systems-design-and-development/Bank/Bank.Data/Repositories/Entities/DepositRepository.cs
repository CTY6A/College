using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class DepositRepository : Repository<Deposit, Guid>, IDepositRepository
    {
        public DepositRepository(BankContext context) : base(context)
        {
        }
    }
}
