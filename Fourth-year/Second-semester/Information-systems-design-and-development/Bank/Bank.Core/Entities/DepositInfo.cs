using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Core.Entities
{
    public class DepositInfo : Entity<int>
    {
        public string Name { get; set; }

        public double Persent { get; set; }

        public Currency Currency { get; set; }

        public int Duration { get; set; }

        public bool Revocable { get; set; }

        public int MinSum { get; set; }

        private DepositInfo()
        {
        }
    }
}
