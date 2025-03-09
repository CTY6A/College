using Bank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class ProgramModel
    {
        public int ProgramId { get; set; }

        public IEnumerable<DepositInfo> Programs { get; set; }

        public ProgramModel()
        {
        }
    }
}
