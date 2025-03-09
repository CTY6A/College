using Bank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class ProgramModelCredit
    {
        public int ProgramId { get; set; }

        public IEnumerable<CreditInfo> Programs { get; set; }

        public ProgramModelCredit()
        {
        }
    }
}
