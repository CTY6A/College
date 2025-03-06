using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISortFileInterface
{   
    public interface ISortFile
    {
        void Sort();
        string FileName { get; set; }
    }
}
