using Bank.Core.Repositories.EntityRepositories;

namespace Bank.Core.Repositories
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        ICityRepository CityRepository { get; }

        IAccountRepository AccountRepository { get; }

        IAccountTypeRepository AccountTypeRepository { get; }

        IDepositInfo DepositInfo {get; }

        IDepositRepository DepositRepository { get; }

        ITransactionRepository TransactionRepository { get; }

        ICreditInfo CreditInfo { get; }

        ICreditRepository CreditRepository { get; }

        void Commit();
    }
}
