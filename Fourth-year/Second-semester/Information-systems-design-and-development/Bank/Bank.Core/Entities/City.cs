using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Core.Entities
{
    public class City : Entity<Guid>
    {
        public string Name { get; set; }

        private City()
        {
        }

        public static City Create(string name)
        {
            return new City
            {
                Id = Guid.NewGuid(),
                Name = name,
            };
        }

        public static City Create(Guid id, string name)
        {
            return new City
            {
                Id = id,
                Name = name,
            };
        }
    }
}
