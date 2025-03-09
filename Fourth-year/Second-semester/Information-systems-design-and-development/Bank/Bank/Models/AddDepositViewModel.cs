using Bank.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class AddDepositViewModel
    {
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"\d{8}", ErrorMessage = "Incorrect format for number of deposit - 8 numbers")]
        public string Number { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"\d+(\.\d{1,2}){0,1}", ErrorMessage = "Incorrect format for sum")]
        public double Sum { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [HiddenInput]
        [Required]
        public int DepositInfoId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int Duration { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Currency { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Persent { get; set;}

        public List<SelectListItem> Persons { get; set; }

        //public List<SelectListItem> DepositsInfo { get; set; }

        public AddDepositViewModel()
        {
        }

        public AddDepositViewModel(DepositInfo program, DateTime date)
        {
            Name = program.Name;
            Duration = program.Duration;
            DepositInfoId = program.Id;
            Currency = program.Currency.ToString();
            Persent = program.Persent.ToString();
            StartDate = date;
            EndDate = StartDate.AddDays(Duration).Date;
        }

        public void SetParams(IEnumerable<Person> persons)
        {
            Persons = persons.Select(x => new SelectListItem($"{x.LastName} {x.FirstName} {x.Patronymic}", x.Id.ToString(), x.Id == PersonId)).ToList();
            //DepositsInfo = depositsInfo.Select(x => new SelectListItem($"{x.Name} {(x.Revocable ? "Отзывной" : "Безотзывной")}", x.Id.ToString(), x.Id == DepositInfoId)).ToList();
        }
    }
}
