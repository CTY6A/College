using System;

namespace Bank.Core.Entities
{
    public class Account : Entity<Guid>
    {
        public Person Person { get; set; }

        public Guid? PersonId { get; set; }

        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }

        public string Number { get; set; }

        public bool Passive { get; set; }

        public Currency Currency { get; set; }

        public bool Deleted { get; set; }

        public double Debet { get; set; }

        public double Credit { get; set; }

        public double Balance => Passive ? (Credit - Debet) : (Debet - Credit);  

        //public Guid PersonId { get; set; }

        //public Person Person { get; set; }

        private Account()
        {
        }

        public static Account Create(AccountType type, string number, bool passive, Currency currency, bool deleted, double debet, double credit
            //, Guid personId
            )
        {
            return new Account()
            {
                Id = Guid.NewGuid(),
                AccountType = type,
                Number = number,
                Passive = passive,
                Currency = currency,
                Deleted = deleted,
                Debet = debet,
                Credit = credit
                //,PersonId = personId
            };
        }
    }
}
