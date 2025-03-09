namespace Bank.Core.Entities
{
    public class Entity<Tkey> : Entity
    {
        public Tkey Id { get; set; }
    }
}
