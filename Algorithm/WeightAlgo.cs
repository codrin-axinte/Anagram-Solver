using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnagramSolver.Algorithm
{
    class WeightAlgo : IAlgorithm
    {
        public bool IsValid(string anagram)
        {
            return anagram.Length > 0;
        }

        public List<string> Solve(string anagram) {
            List<string> solutions = null;


            return solutions;
        }
    }
}
