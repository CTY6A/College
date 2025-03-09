using System;

namespace Bank.Core.Entities
{
    public class Credit : Entity<Guid>
    {
        //public Guid PersonId { get; set; }

        //public Person Person { get; set; }

        public int CreditInfoId { get; set; }

        public CreditInfo CreditInfo { get; set; }

        public Guid MainAccountId { get; set; }

        public Account MainAccount { get; set; }

        public Guid PersentAccountId { get; set; }

        public Account PersentAccount { get; set; }

        public string Number { get; set; }

        public double Sum { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Finished { get; set; }

        private Credit()
        {

        }

        public static Credit Create(
            //Person person, 
            int creditId, Account mainAcc, Account perAcc, string number, double sum, DateTime start, DateTime end, bool fin)
        {
            return new Credit()
            {
                Id = Guid.NewGuid(),
                //Person = person,
                CreditInfoId = creditId,
                MainAccount = mainAcc,
                PersentAccount = perAcc,
                Number = number,
                Sum = sum,
                StartDate = start,
                EndDate = end,
                Finished = fin
            };
        }
    }
}
