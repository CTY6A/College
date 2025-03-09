using Bank.Core.Exceptions;
using Bank.Core.Repositories;
using Bank.Models.PersonModels;
using Bank.Services.Interfaces;
using Bank.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Bank.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly CityService _cityService;
        private readonly IUnitOfWork _uow;


        public PersonController(IPersonService personService, CityService cityService, IUnitOfWork uof)
        {
            _personService = personService;
            _cityService = cityService;
            _uow = uof;
        }

        [HttpGet("")]
        [HttpGet("[controller]/[action]")]
        public IActionResult Index()
        {
            var list = _personService.GetSortedList();
            var model = list.Select(x => new PersonListItemViewModel(x));
            return View(model);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult Create()
        {
            var model = new PersonEditViewModel(_cityService.GetList());
            return View("Add", model);
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Create(PersonEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SetSities(_cityService.GetList());
                return View("Add", model);
            }
            try
            {
                var person = _personService.Create(model);
            }
            catch(UpdateException ex)
            {
                ModelState.AddModelError("UpdateDB", ex.InnerException.InnerException.Message);
                model.SetSities(_cityService.GetList());
                return View("Add", model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("[controller]/[action]/{id:Guid}")]
        public IActionResult Edit(Guid id)
        {
            var person = _personService.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            var model = new PersonEditViewModel(person, _cityService.GetList());
            return View("Edit", model);
        }

        [HttpPost("[controller]/[action]/{id:Guid}")]
        public IActionResult Edit(PersonEditViewModel model)
        {
            var person = _personService.Find(model.Id);
            if (person == null)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                model.SetSities(_cityService.GetList());
                return View("Edit", model);
            }

            try
            {
                var editedPerson = _personService.Edit(person, model);
            }
            catch(UpdateException ex)
            {
                model.SetSities(_cityService.GetList());
                ModelState.AddModelError("UpdateDB", ex.InnerException.InnerException.Message);
                return View("Edit", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Delete(Guid id)
        {
            var accounts = _uow.AccountRepository.List();
            var personAccounts = accounts.Where(x => x.PersonId == id);
            var credits = _uow.CreditRepository.List();
            var credit = credits.FirstOrDefault(x => personAccounts.Any(y => y.Id == x.MainAccountId));
            if (credit != null)
            {
                ModelState.AddModelError("Delete Person", "Can not delete threre is a non finished credit");
                var list = _personService.GetSortedList();
                var model = list.Select(x => new PersonListItemViewModel(x));
                //return View(model);
                return View("Index", model);
                //return RedirectToAction("Index");
            }

            var result = _personService.Delete(id);

            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
