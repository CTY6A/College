using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class DepositInfoRepository : Repository<DepositInfo, int>, IDepositInfo
    {
        public DepositInfoRepository(BankContext context) : base(context)
        {
        }
    }
}
