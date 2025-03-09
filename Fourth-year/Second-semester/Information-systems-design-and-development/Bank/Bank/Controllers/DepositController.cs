using Bank.Core.Entities;
using Bank.Core.Repositories;
using Bank.Models;
using Bank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Controllers
{
    public class DepositController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IUnitOfWork _uow;

        public DepositController(IPersonService personService, IUnitOfWork uof)
        {
            _personService = personService;
            _uow = uof;
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult SelectProgram()
        {
            var programs = _uow.DepositInfo.List();
            var model = new ProgramModel
            {
                Programs = programs
            };
            return View("SelectProgram", model);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult SelectCreditProgram()
        {
            var programs = _uow.CreditInfo.List();
            var model = new ProgramModelCredit
            {
                Programs = programs
            };
            return View("SelectCreditProgram", model);
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult SelectProgram(ProgramModel model)
        {
            var program = _uow.DepositInfo.Find(model.ProgramId);
            if (program == null)
            {
                return NotFound();
            }
            return RedirectToAction("Add", new { programId = model.ProgramId });
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult SelectCreditProgram(ProgramModelCredit model)
        {
            var program = _uow.CreditInfo.Find(model.ProgramId);
            if (program == null)
            {
                return NotFound();
            }
            return RedirectToAction("AddCredit", new { programId = model.ProgramId });
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult Add(int programId)
        {
            var persons = _personService.GetSortedList();
            //var types = _uow.DepositInfo.List();
            var program = _uow.DepositInfo.Find(programId);
            var date = GetCurrDate();
            var model = new AddDepositViewModel(program, date);
            model.SetParams(persons);

            return View("Add", model);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult AddCredit(int programId)
        {
            var persons = _personService.GetSortedList();
            //var types = _uow.DepositInfo.List();
            var program = _uow.CreditInfo.Find(programId);
            var date = GetCurrDate();
            var model = new AddCreditViewModel(program, date);
            model.SetParams(persons);

            return View("AddCredit", model);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GraphicPay(Guid id)
        {
            var credits = _uow.CreditRepository.List(new Specification<Credit>("CreditInfo")).ToList();
            var credit = credits.First(x => x.Id == id);
            var creditInfo = credit.CreditInfo;

            // type of credit
            var list = new List<double>();
            if (creditInfo.Differantal)
            {
                var currDate = credit.StartDate.Date;
                //var main = Math.Round(credit.Sum / creditInfo.Duration, 2, MidpointRounding.ToEven);
                var main = credit.Sum / creditInfo.Duration;
                var rem = credit.Sum;
                for (var q = 0; q < creditInfo.Duration; q++)
                {
                    //var percent = Math.Round(rem * creditInfo.Persent / 100 / 365 * DateTime.DaysInMonth(currDate.Year, currDate.Month), 2, MidpointRounding.ToEven);
                    var percent = rem * creditInfo.Persent / 100 / 365 * DateTime.DaysInMonth(currDate.Year, currDate.Month);
                    var sum = percent + main;
                    list.Add(sum);
                    currDate.AddMonths(1);
                    rem -= main;
                }
            }
            else
            {
                //var i = Math.Round(creditInfo.Persent / 12 / 100, 2, MidpointRounding.ToEven);
                //var k = Math.Round(i * Math.Pow(1 + i, creditInfo.Duration) / (Math.Pow(1 + i, creditInfo.Duration) - 1), 2, MidpointRounding.ToEven);
                var i = creditInfo.Persent / 12 / 100;
                var k = i * Math.Pow(1 + i, creditInfo.Duration) / (Math.Pow(1 + i, creditInfo.Duration) - 1);
                var A = k * credit.Sum;

                for (var j = 0; j < creditInfo.Duration; j++)
                    list.Add(A);
            }

            return View("CreditGraphicPay", list);
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Add(AddDepositViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var persons = _personService.GetSortedList();
                model.SetParams(persons);
                return View("Add", model);
            }
            var person = _personService.Find(model.PersonId);
            var accountTypes = _uow.AccountTypeRepository.List();
            var clientType = accountTypes.First(x => x.Name == "Client");
            
            var mainClientAccount = Account.Create(clientType, GetClientNumber(), true, (Currency)Enum.Parse(Currency.BYN.GetType(), model.Currency), false, 0, 0);
            mainClientAccount.Person = person;
            var persentClientAccount = Account.Create(clientType, GetClientNumber(), true, (Currency)Enum.Parse(Currency.BYN.GetType(), model.Currency), false, 0, 0);
            persentClientAccount.Person = person;
            
            if (model.Currency != Currency.BYN.ToString())
            {
                switch (model.Currency)
                {
                    case "USD":
                        model.Sum = 1.00 * Math.Round(model.Sum * 2.5, 2, MidpointRounding.ToEven);
                        break;
                }
            };

            var deposit = Deposit.Create(
                //person, 
                model.DepositInfoId, mainClientAccount, persentClientAccount, model.Number, model.Sum, model.StartDate.Date, model.EndDate.Date);

            _uow.DepositRepository.Add(deposit);
            _uow.AccountRepository.Add(mainClientAccount);
            _uow.AccountRepository.Add(persentClientAccount);


            var accounts = _uow.AccountRepository.List(new Specification<Account>("AccountType"));
            var cashierAccount = accounts.First(x => x.AccountTypeId == 1);
            var fondAccount = accounts.First(x => x.AccountTypeId == 2);


            var transactionToCashier = Transaction.Create(DateTime.Now, model.Sum, cashierAccount, null);
            cashierAccount.Debet += model.Sum;
            var transactionToMainClientAccount = Transaction.Create(DateTime.Now, model.Sum, mainClientAccount, cashierAccount);
            cashierAccount.Credit += model.Sum;
            var transactionFrommainClientToFond = Transaction.Create(DateTime.Now, model.Sum, fondAccount, mainClientAccount);
            mainClientAccount.Credit += model.Sum;
            mainClientAccount.Debet += model.Sum;
            _uow.TransactionRepository.Add(transactionFrommainClientToFond);
            _uow.TransactionRepository.Add(transactionToCashier);
            _uow.TransactionRepository.Add(transactionToMainClientAccount);
            fondAccount.Credit += model.Sum;
            

            _uow.Commit();

            return RedirectToAction("GetAccountsList");
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult AddCredit(AddCreditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var persons = _personService.GetSortedList();
                model.SetParams(persons);
                return View("AddCredit", model);
            }
            var person = _personService.Find(model.PersonId);
            var accountTypes = _uow.AccountTypeRepository.List();
            var clientType = accountTypes.First(x => x.Name == "Client");

            //!!!!!
            var mainClientAccount = Account.Create(clientType, GetClientNumber(), false, (Currency)Enum.Parse(Currency.BYN.GetType(), model.Currency), false, 0, 0);
            mainClientAccount.Person = person;
            var persentClientAccount = Account.Create(clientType, GetClientNumber(), false, (Currency)Enum.Parse(Currency.BYN.GetType(), model.Currency), false, 0, 0);
            persentClientAccount.Person = person;

            if (model.Currency != Currency.BYN.ToString())
            {
                switch (model.Currency)
                {
                    case "USD":
                        model.Sum = 1.00 * Math.Round(model.Sum * 2.5, 2, MidpointRounding.ToEven);
                        break;
                }
            };

            var credit = Credit.Create(model.CreditInfoId, mainClientAccount, persentClientAccount, model.Number, model.Sum, model.StartDate.Date, model.EndDate.Date, false);
            
            _uow.CreditRepository.Add(credit);
            _uow.AccountRepository.Add(mainClientAccount);
            _uow.AccountRepository.Add(persentClientAccount);

            var accounts = _uow.AccountRepository.List(new Specification<Account>("AccountType"));
            var cashierAccount = accounts.First(x => x.AccountTypeId == 1);
            var fondAccount = accounts.First(x => x.AccountTypeId == 2);

            
            var trans1 = Transaction.Create(DateTime.Now, model.Sum, mainClientAccount, fondAccount);
            fondAccount.Debet += model.Sum;
            mainClientAccount.Debet += model.Sum;
            _uow.TransactionRepository.Add(trans1);
            _uow.Commit();

            var trans2 = Transaction.Create(DateTime.Now, model.Sum, cashierAccount, mainClientAccount);
            mainClientAccount.Credit += model.Sum;
            cashierAccount.Debet += model.Sum;
            _uow.TransactionRepository.Add(trans2);
            _uow.Commit();

            var trans3 = Transaction.Create(DateTime.Now, model.Sum, null, cashierAccount);
            cashierAccount.Credit += model.Sum;
            _uow.TransactionRepository.Add(trans3);
            _uow.Commit();

            //_uow.TransactionRepository.Add(trans2);
            //_uow.TransactionRepository.Add(trans3);


            // type of credit
            //var creditInfo = _uow.CreditInfo.Find(model.CreditInfoId);
            //var list = new List<double>();
            //if (creditInfo.Differantal)
            //{
            //    var currDate = model.StartDate.Date;
            //    var main = Math.Round(model.Sum / model.Duration, 2, MidpointRounding.ToEven);
            //    var rem = model.Sum;
            //    for (var q = 0; q < model.Duration; q++)
            //    {
            //        var percent = rem * creditInfo.Persent / 100 / 365 * DateTime.DaysInMonth(currDate.Year, currDate.Month);
            //        var sum = percent + main;
            //        list.Add(sum);
            //        currDate.AddMonths(1);
            //        rem -= main;
            //    }
            //}
            //else
            //{
            //    var i = Math.Round(creditInfo.Persent / model.Duration / 100, 2, MidpointRounding.ToEven);
            //    var k = Math.Round(i * Math.Pow(1 + i, model.Duration) / (Math.Pow(1 + i, model.Duration) - 1));
            //    var A = k * model.Sum;

            //    for (var j = 0; j < model.Duration; j++)
            //        list.Add(A);
            //}


            //var deposit = Deposit.Create(
            //    //person, 
            //    model.DepositInfoId, mainClientAccount, persentClientAccount, model.Number, model.Sum, model.StartDate.Date, model.EndDate.Date);

            //_uow.DepositRepository.Add(deposit);
            //_uow.AccountRepository.Add(mainClientAccount);
            //_uow.AccountRepository.Add(persentClientAccount);


            //var accounts = _uow.AccountRepository.List(new Specification<Account>("AccountType"));
            //var cashierAccount = accounts.First(x => x.AccountTypeId == 1);
            //var fondAccount = accounts.First(x => x.AccountTypeId == 2);


            //var transactionToCashier = Transaction.Create(DateTime.Now, model.Sum, cashierAccount, null);
            //cashierAccount.Debet += model.Sum;
            //var transactionToMainClientAccount = Transaction.Create(DateTime.Now, model.Sum, mainClientAccount, cashierAccount);
            //cashierAccount.Credit += model.Sum;
            //var transactionFrommainClientToFond = Transaction.Create(DateTime.Now, model.Sum, fondAccount, mainClientAccount);
            //mainClientAccount.Credit += model.Sum;
            //mainClientAccount.Debet += model.Sum;
            //_uow.TransactionRepository.Add(transactionFrommainClientToFond);
            //_uow.TransactionRepository.Add(transactionToCashier);
            //_uow.TransactionRepository.Add(transactionToMainClientAccount);
            //fondAccount.Credit += model.Sum;


            _uow.Commit();

            return RedirectToAction("GraphicPay", new { id = credit.Id });
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GetAccountsList()
        {
            var accounts = _uow.AccountRepository.List(new Specification<Account>("AccountType", "Person")).ToList()
                .Where(x => !x.Deleted)
                .OrderBy(x => x.AccountTypeId)
                .ThenBy(x => x.Passive);

            //var systemAcc = _uow.AccountRepository.List(new Specification<Account>("AccountType")).ToList()
            //    .Where(x => !x.Deleted && x.AccountTypeId != 3);

            //var list = new List<(Account, bool?)>();
            ////foreach(var ac in accounts)
            ////{
            ////    list.Add((ac, false));
            ////}
            //var credits = _uow.CreditRepository.List();
            //var deposits = _uow.DepositRepository.List();
            //foreach(var c in credits)
            //{
            //    var mainAcc = accounts.First(x => x.Id == c.MainAccountId);
            //    var persentAccount = accounts.First(x => x.Id == c.PersentAccountId);
            //    list.Add((mainAcc, true));
            //    list.Add((persentAccount, false));
            //}
            //foreach (var d in deposits)
            //{
            //    var mainAcc = accounts.First(x => x.Id == d.MainAccountId);
            //    var persentAccount = accounts.First(x => x.Id == d.PersentAccountId);
            //    list.Add((mainAcc, true));
            //    list.Add((persentAccount, false));
            //}
            //Func<Account, (Account, bool?)> fnc = (x => (x, null));
            //list.AddRange(systemAcc.Select(fnc));
            //var sortedList = list.OrderBy(x => x.Item1.AccountTypeId).OrderBy(x => x.Item1.Passive);



            return View("AccountsList", accounts);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GetTransactionsList()
        {
            var transactions = _uow.TransactionRepository.List(new Specification<Transaction>("AccountFrom", "AccountTo")).ToList().OrderBy(x => x.SequenceNumber);

            return View("TransactionList", transactions);
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult CloseDay()
        {
            var prevDate = GetCurrDate();
            var currDate = prevDate.AddDays(1);

            var accounts = _uow.AccountRepository.List(new Specification<Account>("AccountType"));
            var cashierAccount = accounts.First(x => x.AccountTypeId == 1);
            var fondAccount = accounts.First(x => x.AccountTypeId == 2);
            
            
            var clientAccounts = accounts.Where(x => x.AccountTypeId == 3);

            var deposits = _uow.DepositRepository.List(new Specification<Deposit>("DepositInfo"));
            var credits = _uow.CreditRepository.List(new Specification<Credit>("CreditInfo"));
            foreach(var acc in clientAccounts)
            {
                if (acc.Deleted) continue;
                var deposit = deposits.FirstOrDefault(x => x.PersentAccountId == acc.Id);
                var credit = credits.FirstOrDefault(x => x.PersentAccountId == acc.Id);
                if (deposit != null)
                {
                    Transaction trans = null;
                    if (acc.Passive)
                    {
                        var addValue = Math.Round(deposit.Sum * deposit.DepositInfo.Persent / (100 * 365), 2, MidpointRounding.ToEven);
                        acc.Credit += addValue;
                        fondAccount.Debet += addValue;
                        trans = Transaction.Create(DateTime.Now, addValue, acc, fondAccount);
                    }
                    _uow.TransactionRepository.Add(trans);
                }

                if (credit != null)
                {
                    var creditInfo = credit.CreditInfo;
                    double value;
                    if (creditInfo.Differantal)
                    {
                        var startDate = credit.StartDate;
                        startDate = startDate.AddMonths(1);
                        //var main = Math.Round(credit.Sum / creditInfo.Duration, 2, MidpointRounding.ToEven);
                        var main = credit.Sum / creditInfo.Duration;
                        var rem = credit.Sum;
                        while (startDate < currDate)
                        {
                            startDate = startDate.AddMonths(1);
                            rem -= main;
                        }

                        //var percent = Math.Round(rem * creditInfo.Persent / 100 / 365 * DateTime.DaysInMonth(currDate.Year, currDate.Month), 2, MidpointRounding.ToEven);
                        var percent = rem * creditInfo.Persent / 100 / 365 * DateTime.DaysInMonth(currDate.Year, currDate.Month);
                        //value = Math.Round((percent + main) / DateTime.DaysInMonth(currDate.Year, currDate.Month), 2, MidpointRounding.ToEven);
                        //value = (percent + main) / DateTime.DaysInMonth(currDate.Year, currDate.Month);
                        value = (percent) / DateTime.DaysInMonth(currDate.Year, currDate.Month);
                    }
                    else
                    {
                        //var i = Math.Round(creditInfo.Persent / 12 / 100, 2, MidpointRounding.ToEven);
                        //var k = Math.Round(i * Math.Pow(1 + i, creditInfo.Duration) / (Math.Pow(1 + i, creditInfo.Duration) - 1), 2, MidpointRounding.ToEven);
                        //var A = k * credit.Sum;
                        //value = Math.Round(A / DateTime.DaysInMonth(currDate.Year, currDate.Month), 2, MidpointRounding.ToEven);
                        var i = creditInfo.Persent / 12 / 100;
                        var k = i * Math.Pow(1 + i, creditInfo.Duration) / (Math.Pow(1 + i, creditInfo.Duration) - 1);
                        var A = k * credit.Sum;
                        var perc = (A * creditInfo.Duration - credit.Sum) / creditInfo.Duration;
                        value = perc / DateTime.DaysInMonth(currDate.Year, currDate.Month); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    }

                    var trans = Transaction.Create(DateTime.Now, value, fondAccount, acc);
                    acc.Credit += value;
                    fondAccount.Credit += value;
                    _uow.TransactionRepository.Add(trans);
                    _uow.Commit();
                }
            }
                
            foreach (var acc in clientAccounts)
            {
                if (acc.Deleted) continue;
                var deposit = deposits.FirstOrDefault(x => x.PersentAccountId == acc.Id);
                var credit = credits.FirstOrDefault(x => x.PersentAccountId == acc.Id);
                if (deposit != null)
                {
                    var (need, _) = NeedGoToCashier(currDate, deposit.StartDate, deposit.EndDate);
                    var depositEnd = currDate.Date == deposit.EndDate.Date;
                    if ((need && deposit.DepositInfo.Revocable) || depositEnd)
                    {
                        if (acc.Passive)
                        {
                            //var sum = Math.Round(acc.Credit - acc.Debet, 2, MidpointRounding.ToEven);
                            var sum = acc.Credit - acc.Debet;
                            acc.Debet += sum;
                            cashierAccount.Debet += sum;
                            var transToCash = Transaction.Create(DateTime.Now, sum, cashierAccount, acc);
                            cashierAccount.Credit += sum;
                            var transFromCash = Transaction.Create(DateTime.Now, sum, null, cashierAccount);
                            _uow.TransactionRepository.Add(transToCash);
                            _uow.TransactionRepository.Add(transFromCash);
                            if (depositEnd)
                            {
                                acc.Deleted = true;
                            }
                        }
                    }
                }

                if (credit != null)
                {
                    var (need, ind) = NeedGoToCashier(currDate, credit.StartDate, credit.EndDate);
                    var creditEnd = currDate.Date == credit.EndDate.Date;
                    if (need || creditEnd)
                    {
                        var creditInfo = credit.CreditInfo;
                        double percentValue;
                        double mainValue;
                        if (creditInfo.Differantal) 
                        {
                            //var startDate = credit.StartDate;
                            //startDate = startDate.AddMonths(1);
                            //var main = Math.Round(credit.Sum / creditInfo.Duration, 2, MidpointRounding.ToEven);
                            var main = credit.Sum / creditInfo.Duration;
                            var rem = credit.Sum;
                            //while (startDate < currDate)
                            //{
                            //    startDate = startDate.AddMonths(1);
                            //    rem -= main;
                            //}
                            for (var i = 1; i < ind; i++)
                                rem -= main;

                            //var percent = Math.Round(rem * creditInfo.Persent / 100 / 365 * DateTime.DaysInMonth(currDate.Year, currDate.Month), 2, MidpointRounding.ToEven);
                            var percent = rem * creditInfo.Persent / 100 / 365 * DateTime.DaysInMonth(currDate.Year, currDate.Month);
                            percentValue = percent;
                            mainValue = main;
                            //value = Math.Round((percent + main) / DateTime.DaysInMonth(currDate.Year, currDate.Month), 2, MidpointRounding.ToEven);
                        }
                        else
                        {
                            //var i = Math.Round(creditInfo.Persent / 12 / 100, 2, MidpointRounding.ToEven);
                            //var k = Math.Round(i * Math.Pow(1 + i, creditInfo.Duration) / (Math.Pow(1 + i, creditInfo.Duration) - 1), 2, MidpointRounding.ToEven);
                            //var A = k * credit.Sum;
                            //percentValue = Math.Round((A * creditInfo.Duration - credit.Sum) / creditInfo.Duration, 2, MidpointRounding.ToEven);
                            var i = creditInfo.Persent / 12 / 100;
                            var k = i * Math.Pow(1 + i, creditInfo.Duration) / (Math.Pow(1 + i, creditInfo.Duration) - 1);
                            var A = k * credit.Sum;
                            percentValue = (A * creditInfo.Duration - credit.Sum) / creditInfo.Duration;

                            //mainValue = Math.Round(A - percentValue, 2, MidpointRounding.ToEven);
                            mainValue = A - percentValue;
                        }

                        //persents
                        var trans1 = Transaction.Create(DateTime.Now, percentValue, cashierAccount, null);
                        cashierAccount.Debet += percentValue;
                        _uow.TransactionRepository.Add(trans1);
                        _uow.Commit();

                        var trans2 = Transaction.Create(DateTime.Now, percentValue, acc, cashierAccount);
                        cashierAccount.Credit += percentValue;
                        acc.Debet += percentValue;
                        _uow.TransactionRepository.Add(trans2);
                        _uow.Commit();

                        //main
                        var trans3 = Transaction.Create(DateTime.Now, mainValue, cashierAccount, null);
                        cashierAccount.Debet += mainValue;
                        _uow.TransactionRepository.Add(trans3);
                        _uow.Commit();

                        var mainAcc = _uow.AccountRepository.Find(credit.MainAccountId);
                        var trans4 = Transaction.Create(DateTime.Now, mainValue, mainAcc, cashierAccount);
                        cashierAccount.Credit += mainValue;
                        mainAcc.Debet += mainValue;
                        _uow.TransactionRepository.Add(trans4);
                        _uow.Commit();
                        if (creditEnd)
                        {
                            acc.Deleted = true;
                        }
                    }
                }
            }

            foreach(var acc in clientAccounts)
            {
                if (acc.Deleted) continue;
                var deposit = deposits.FirstOrDefault(x => x.MainAccountId == acc.Id);
                var credit = credits.FirstOrDefault(x => x.MainAccountId == acc.Id);
                if (deposit != null)
                {
                    var endDeposit = currDate.Date == deposit.EndDate.Date;
                    if (endDeposit)
                    {
                        var sum = deposit.Sum;
                        fondAccount.Debet += sum;
                        acc.Credit += sum;
                        var trans1 = Transaction.Create(DateTime.Now, sum, acc, fondAccount);
                        acc.Debet += sum;
                        cashierAccount.Debet += sum;
                        var trans2 = Transaction.Create(DateTime.Now, sum, cashierAccount, acc);
                        cashierAccount.Credit += sum;
                        var trans3 = Transaction.Create(DateTime.Now, sum, null, cashierAccount);
                        _uow.TransactionRepository.Add(trans1);
                        _uow.Commit();
                        _uow.TransactionRepository.Add(trans2);
                        _uow.Commit();
                        _uow.TransactionRepository.Add(trans3);
                        _uow.Commit();
                        if (endDeposit)
                        {
                            acc.Deleted = true;
                        }
                    }
                }

                if (credit != null)
                {
                    var endCredit = currDate.Date == credit.EndDate.Date;
                    if (endCredit)
                    {
                        acc.Credit += credit.Sum;
                        fondAccount.Credit += credit.Sum;
                        var trans = Transaction.Create(DateTime.Now, credit.Sum, fondAccount, acc);
                        _uow.TransactionRepository.Add(trans);
                        _uow.Commit();
                        if (endCredit)
                        {
                            acc.Deleted = true;
                            credit.Finished = true;
                        }
                    }
                }
            }

            _uow.Commit();
            System.IO.File.WriteAllText("Date.txt", currDate.ToString());
            return RedirectToAction("GetAccountsList");
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult CloseNDays()
        {
            for (var i = 1; i <= 30; i++)
            {
                CloseDay();
            };

            return RedirectToAction("GetAccountsList");
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Close10Days()
        {
            for (var i = 1; i <= 10; i++)
            {
                CloseDay();
            };

            return RedirectToAction("GetAccountsList");
        }


        private static string GetClientNumber()
        {
            var r = new Random(DateTime.Now.Millisecond);
            var number = $"301400011{r.Next(1, 999)}";
            return number;
        }

        private static (bool, int) NeedGoToCashier(DateTime currDate, DateTime startDate, DateTime endDate)
        {
            var i = 0;
            while (startDate.Date < endDate.Date) 
            {
                i++;
                startDate = startDate.AddMonths(1);
                if (currDate.Date == startDate.Date)
                {
                    return (true, i);
                }
            }
            return (false, i);
        }

        private static DateTime GetCurrDate()
        {
            DateTime currDate;
            if (System.IO.File.Exists("Date.txt"))
            {
                currDate = DateTime.Parse(System.IO.File.ReadAllText("Date.txt"));
            }
            else
            {
                currDate = DateTime.Now;
            }

            return currDate;
        }

        private static double Divide(double v1, double v2)
        {
            var result = Math.Round(v1 / v2, 2, MidpointRounding.ToEven);
            return result;
        }
    }
}
