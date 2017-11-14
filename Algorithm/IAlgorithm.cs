using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Algorithm
{
    interface IAlgorithm
    {
        bool IsValid(string anagram);
        List<string> Solve(string anagram);
    }
}
