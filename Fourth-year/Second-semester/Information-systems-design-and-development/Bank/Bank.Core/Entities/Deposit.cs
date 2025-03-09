using System;

namespace Bank.Core.Entities
{
    public class Deposit : Entity<Guid>
    {
        //public Guid PersonId { get; set; }

        //public Person Person { get; set; }

        public int DepositInfoId { get; set; }

        public DepositInfo DepositInfo { get; set; }

        public Guid MainAccountId { get; set; }

        public Account MainAccount { get; set; }

        public Guid PersentAccountId { get; set; }

        public Account PersentAccount { get; set; }

        public string Number { get; set; }

        public double Sum { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        private Deposit()
        {

        }

        public static Deposit Create(
            //Person person, 
            int depositId, Account mainAcc, Account perAcc, string number, double sum, DateTime start, DateTime end)
        {
            return new Deposit()
            {
                Id = Guid.NewGuid(),
                //Person = person,
                DepositInfoId = depositId,
                MainAccount = mainAcc,
                PersentAccount = perAcc,
                Number = number,
                Sum = sum,
                StartDate = start,
                EndDate = end
            };
        }
    }
}
