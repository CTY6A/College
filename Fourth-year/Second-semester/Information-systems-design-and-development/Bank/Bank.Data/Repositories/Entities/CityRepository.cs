using Bank.Core.Entities;
using Bank.Core.Repositories.EntityRepositories;

namespace Bank.Data.Repositories.Entities
{
    internal class CityRepository : Repository<City> ,ICityRepository
    {
        public CityRepository(BankContext context) : base(context)
        {
        }
    }
}
