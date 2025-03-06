using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGenerateFileInterface
{
    public interface IGenerateFile
    {
        void GenerateFile();
        string FileName { get; set; }
    }
}
