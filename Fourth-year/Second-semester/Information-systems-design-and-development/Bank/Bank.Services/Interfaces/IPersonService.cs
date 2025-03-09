using Bank.Core.Entities;
using Bank.Models.PersonModels;
using System;
using System.Collections.Generic;

namespace Bank.Services.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetList();
        IEnumerable<Person> GetSortedList();
        Person Find(Guid id);
        Person Create(PersonEditViewModel model);
        Person Edit(Person p, PersonEditViewModel model);
        bool Delete(Guid id);
    }
}
