using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Core.Entities
{
    public class Transaction : Entity<Guid>
    {
        public DateTime Date { get; set; }

        public double Sum { get; set; }

        public double FromPrevBalance { get; set; }

        public double FromCurrBalance { get; set; }

        public double ToPrevBalance { get; set; }

        public double ToCurrBalance { get; set; }

        public Guid? AccountToId { get; set; }

        public Account AccountTo { get; set; }

        public Guid? AccountFromId { get; set; }

        public Account AccountFrom { get; set; }

        public int SequenceNumber { get; set; }

        private Transaction()
        {
        }

        public static Transaction Create(DateTime date, 
            double sum, 
            //double fromPrev, double fromCurr, double toPrev, double toCurr, 
            Account accountTo, 
            Account accountFrom)
        {
            return new Transaction()
            {
                Date = date,
                Sum = sum,
                //FromPrevBalance = fromPrev,
                //FromCurrBalance = fromCurr,
                //ToPrevBalance = toPrev,
                //ToCurrBalance = toCurr,
                AccountTo = accountTo,
                AccountFrom = accountFrom
            };
        }
    }
}
