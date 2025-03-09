namespace Bank.Core.Entities
{
    public class AccountType : Entity<int>
    {
        public string Name { get; set; }

        private AccountType()
        {
        }
    }
}
