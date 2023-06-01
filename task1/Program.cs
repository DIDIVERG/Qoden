using System;

namespace task1
{
    class Program
    {
        static void Main()
        {
           int divider = Int32.Parse(Console.ReadLine());
           var hashTable = new HashTable(divider);
           string[] values = (Console.ReadLine() ?? string.Empty).Split(' ');
           foreach (var item in values)
           {
               hashTable.Insert(int.Parse(item));
           }
           hashTable.PrintTable();
        }
    }
}