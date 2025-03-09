using Bank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Core.Repositories.EntityRepositories
{
    public interface IDepositRepository : IRepository<Deposit, Guid>
    {
    }
}
