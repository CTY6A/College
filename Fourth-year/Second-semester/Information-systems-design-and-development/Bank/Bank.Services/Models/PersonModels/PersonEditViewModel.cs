using Bank.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bank.Models.PersonModels
{
    public class PersonEditViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[А-Яа-я-]{1,48}$", ErrorMessage = "Неверный формат имени")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[А-Яа-я-]{1,48}$", ErrorMessage = "Неверный формат фамилии")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Отчество обязательно")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[А-Яа-я]{1,48}$", ErrorMessage = "Неверны формат отчества")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Обязательное поле, проверьте правильность даты")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage ="Серия паспорта обязательна")]
        [DataType(DataType.Text)]
        public string SeriaPassport { get; set; }

        [Required(ErrorMessage = "Номер паспорта обязателен")]
        [DataType(DataType.Text)]
        [RegularExpression(@"\d{7}", ErrorMessage = "Неверный формат номера пасспорта")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "Кем выдан обязательно")]
        [DataType(DataType.Text)]
        public string KemVidan { get; set; }

        [Required(ErrorMessage = "Когда выдан обязательно, проверьте правильность даты")]
        [DataType(DataType.Date)]
        public DateTime DateVidan { get; set; }

        [Required(ErrorMessage = "Идентификационный номер обязатален")]
        [DataType(DataType.Text)]
        [RegularExpression(@"\d{7}[A-Z]\d{3}[A-Z]{2}\d", ErrorMessage = "Неверный формат идентификационного номера")]
        public string IdentNumber { get; set; }

        [Required(ErrorMessage = "Место рождения обязательно")]
        [DataType(DataType.Text)]
        public string BirthPlace { get; set; }

        [Required(ErrorMessage = "Город проживания обязателен")]
        public Guid CityId { get; set; }

        public List<SelectListItem> Cities { get; set; }

        [Required(ErrorMessage = "Адрес проживания обязателен")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{5,7}", ErrorMessage = "Неверный формат домашнего телефона")]
        public string HomePhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[+]375\d{9}", ErrorMessage = "Неверный формат мобильного телефона")]
        public string MobPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string WorkPlace { get; set; }

        [DataType(DataType.Text)]
        public string Position { get; set; }

        [Required(ErrorMessage = "Семейное положение обязательно")]
        public FamilyStatus FamilyStatus { get; set; }

        [Required(ErrorMessage = "Гражданство обязательно")]
        public Nationality Nationality { get; set; }

        [Required(ErrorMessage = "Инвалидность обязательно")]
        public Disability Disability { get; set; }

        [Required(ErrorMessage = "Поле пенсионер обязательно")]
        public bool Pensioner { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression(@"\d+(\.\d{1,2}){0,1}", ErrorMessage = "Неверный формат поля зарплата")]
        public string Salary { get; set; }

        [Required(ErrorMessage = "Поле военнообязаный обязательно")]
        public bool Army { get; set; }

        public PersonEditViewModel()
        {
        }

        public PersonEditViewModel(IEnumerable<City> cities)
        {
            Cities = cities.Select(x => new SelectListItem(x.Name, x.Id.ToString(), CityId == x.Id)).ToList();
        }

        public PersonEditViewModel(Person p, IEnumerable<City> cities) : this(cities)
        {
            Id = p.Id;
            FirstName = p.FirstName;
            LastName = p.LastName;
            Patronymic = p.Patronymic;
            CityId = p.CityId;
            DateOfBirth = p.DateOfBirth;
            SeriaPassport = p.SeriaPassport;
            PassportNumber = p.PassportNumber;
            KemVidan = p.KemVidan;
            DateVidan = p.DateVidan;
            IdentNumber = p.IdentNumber;
            BirthPlace = p.BirthPlace;
            Address = p.Address;
            HomePhone = p.HomePhone;
            MobPhone = p.MobPhone;
            Email = p.Email;
            WorkPlace = p.WorkPlace;
            Position = p.Position;
            FamilyStatus = p.FamilyStatus;
            Nationality = p.Nationality;
            Disability = p.Disability;
            Pensioner = p.Pensioner;
            Salary = p.Salary.ToString();
            Army = p.Army;
        }

        public void SetSities(IEnumerable<City> cities)
        {
            Cities = cities.Select(x => new SelectListItem(x.Name, x.Id.ToString(), CityId == x.Id)).ToList();
        }
    }
}
