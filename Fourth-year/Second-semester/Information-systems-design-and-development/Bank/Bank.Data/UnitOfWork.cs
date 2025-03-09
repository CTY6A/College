using Bank.Core.Entities;
using Bank.Core.Exceptions;
using Bank.Core.Repositories;
using Bank.Core.Repositories.EntityRepositories;
using Bank.Data.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Bank.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextOptions<BankContext> _options;
        private PersonRepository _personRepository;
        private CityRepository _cityRepository;
        private AccountRepository _accountRepository;
        private AccountTypeRepository _accountTypeRepository;
        private TransactionRepository _transactionrepository;
        private DepositInfoRepository depositInfoRepository;
        private DepositRepository depositRepository;
        private CreditInfoRepository creditInfoRepository;
        private CreditRepository creditRepository;


        private BankContext Context { get; }

        public IPersonRepository PersonRepository => _personRepository;

        public ICityRepository CityRepository => _cityRepository;

        public IAccountRepository AccountRepository => _accountRepository;

        public IAccountTypeRepository AccountTypeRepository => _accountTypeRepository;

        public IDepositInfo DepositInfo => depositInfoRepository;

        public IDepositRepository DepositRepository => depositRepository;

        public ICreditInfo CreditInfo => creditInfoRepository;

        public ICreditRepository CreditRepository => creditRepository;

        public ITransactionRepository TransactionRepository => _transactionrepository;


        public UnitOfWork(IOptions<UnitOfWorkOptions> optionsAccessor) : this(optionsAccessor.Value)
        {
        }

        public UnitOfWork(UnitOfWorkOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankContext>();
            optionsBuilder.UseSqlServer(options.ConnectionString, x => x.CommandTimeout(options.CommandTimeout));
            _options = optionsBuilder.Options;

            Context ??= new BankContext();
            _personRepository ??= new PersonRepository(Context);
            _cityRepository ??= new CityRepository(Context);
            _transactionrepository ??= new TransactionRepository(Context);
            _accountRepository ??= new AccountRepository(Context);
            _accountTypeRepository ??= new AccountTypeRepository(Context);
            depositRepository ??= new DepositRepository(Context);
            depositInfoRepository ??= new DepositInfoRepository(Context);
            creditRepository ??= new CreditRepository(Context);
            creditInfoRepository ??= new CreditInfoRepository(Context);


            //_cityRepository.Add(City.Create(Guid.Parse("bab30eb1-4316-e811-9c77-1c1b0d1151db"), "Minsk"));
            //_cityRepository.Add(City.Create(Guid.Parse("bab30eb2-4316-e811-9c77-1c1b0d1151db"), "Brest"));
            //_cityRepository.Add(City.Create(Guid.Parse("bab30eb3-4316-e811-9c77-1c1b0d1151db"), "Grodno"));
            //_cityRepository.Add(City.Create(Guid.Parse("bab30eb4-4316-e811-9c77-1c1b0d1151db"), "Vitebsk"));
            //_cityRepository.Add(City.Create(Guid.Parse("bab30eb5-4316-e811-9c77-1c1b0d1151db"), "Gomel"));
            //Commit();
        }

        public void Commit()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("UnitOfWork");
            }

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString()));
            }
            catch (DbUpdateException ex)
            {
                throw new UpdateException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Commit error.", ex);
            }
        }

        private bool _isDisposed;

        public void Dispose()
        {
            if (Context == null)
            {
                return;
            }

            if (!_isDisposed)
            {
                Context.Dispose();
            }

            _isDisposed = true;
        }
    }
}
