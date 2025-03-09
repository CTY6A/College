using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;
using System;

namespace Bank.Data.Repositories.Entities
{
    internal class CreditInfoRepository : Repository<CreditInfo, int>, ICreditInfo
    {
        public CreditInfoRepository(BankContext context) : base(context)
        {
        }
    }
}
