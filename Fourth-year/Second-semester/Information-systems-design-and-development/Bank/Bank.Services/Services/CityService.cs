using Bank.Core.Entities;
using Bank.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Services.Services
{
    public class CityService
    {
        private readonly IUnitOfWork _uow;

        public CityService(IUnitOfWork uow) => _uow = uow;

        public IEnumerable<City> GetList()
        {
            var result = _uow.CityRepository.List();
            return result;
        }
    }
}
