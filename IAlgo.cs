using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver
{
    interface IAlgo
    {
        bool IsValid(string anagram);
        List<string> Solve(string anagram);
    }
}
