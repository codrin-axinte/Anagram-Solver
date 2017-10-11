using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver
{
    public static class UI
    {
        public static string Input(string label, string operatorFormat = ">>> ")
        {
            Console.WriteLine("[-] " + label);
            Console.Write(">>> ");
            return Console.ReadLine();
        }

        public static void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public static void Warning(string message)
        {
            Console.WriteLine("[WARNING] " + message);
        }

        public static void Error(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }

        public static void Success(string message)
        {
            Console.WriteLine("[SUCCESS] " + message);
        }

        public static void Ok(string message)
        {
            Console.WriteLine("[OK] " + message);
        }

        public static void List(List<string> items, string label = null, int start = 1, int end = 0)
        {
            if (label != null)
                Info(label);
            var length = end == 0 ? items.Count : end;
            for(var i = start; i< length; i++)
                Console.WriteLine(" [-] " + items[i]);   
        }

        public static void Pause()
        {
            Console.ReadLine();
        }

        public static void Blank()
        {
            Console.WriteLine();
        }
    }
}

