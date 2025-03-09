using Bank.Core.Entities;
using Bank.Core.Repositories;
using Bank.Models.PersonModels;
using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Bank.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _uow;

        public PersonService(IUnitOfWork uow) => _uow = uow;

        public Person Find(Guid id)
        {
            var person = _uow.PersonRepository.Find(id);
            return person;
        }

        public IEnumerable<Person> GetList()
        {
            return _uow.PersonRepository.List();
        }

        public IEnumerable<Person> GetSortedList()
        {
            var list = GetList();
            return list.OrderBy(_ => _.LastName);
        }

        public Person Create(PersonEditViewModel model)
        {
            var person = Person.Create(
                model.FirstName, 
                model.LastName, 
                model.Patronymic, 
                model.DateOfBirth,
                model.SeriaPassport,
                model.PassportNumber,
                model.KemVidan,
                model.DateVidan,
                model.IdentNumber,
                model.BirthPlace,
                model.CityId,
                model.Address,
                model.HomePhone,
                model.MobPhone,
                model.Email,
                model.WorkPlace,
                model.Position,
                model.FamilyStatus,
                model.Nationality,
                model.Disability,
                model.Pensioner,
                model.Salary,
                model.Army
            );

            _uow.PersonRepository.Add(person);
            _uow.Commit();

            return person;
        }

        public Person Edit(Person p, PersonEditViewModel model)
        {
            p.FirstName = model.FirstName;
            p.LastName = model.LastName;
            p.Patronymic = model.Patronymic;
            p.CityId = model.CityId;
            p.DateOfBirth = model.DateOfBirth;
            p.SeriaPassport = model.SeriaPassport;
            p.PassportNumber = model.PassportNumber;
            p.KemVidan = model.KemVidan;
            p.DateVidan = model.DateVidan;
            p.IdentNumber = model.IdentNumber;
            p.BirthPlace = model.BirthPlace;
            p.Address = model.Address;
            p.HomePhone = model.HomePhone;
            p.MobPhone = model.MobPhone;
            p.Email = model.Email;
            p.WorkPlace = model.WorkPlace;
            p.Position = model.Position;
            p.FamilyStatus = model.FamilyStatus;
            p.Nationality = model.Nationality;
            p.Disability = model.Disability;
            p.Pensioner = model.Pensioner;
            p.Salary = string.IsNullOrWhiteSpace(model.Salary) ? (double?)null : Convert.ToDouble(model.Salary, CultureInfo.InvariantCulture);
            p.Army = model.Army;
            
            _uow.Commit();

            return p;
        }

        public bool Delete(Guid id)
        {
            var p = Find(id);

            _uow.PersonRepository.Remove(p);
            _uow.Commit();

            return true;
        }
    }
}
