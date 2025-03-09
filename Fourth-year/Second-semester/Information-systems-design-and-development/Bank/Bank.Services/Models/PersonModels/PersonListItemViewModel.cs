using Bank.Core.Entities;
using System;

namespace Bank.Models.PersonModels
{
    public class PersonListItemViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public PersonListItemViewModel(Person person)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Patronymic = person.Patronymic;
        }
    }
}
