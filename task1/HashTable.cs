using System;
using System.Text;

namespace task1
{
    public class HashTable
    {
        public ListNode[] values;
        private readonly int _divider;
        public HashTable(int divider)
        {
            _divider = divider;
            values = new ListNode[divider];
        }

        public void Insert(int newValue)
        {
            int value = HashFunction(newValue);
            ListNode current = values[value];
            if (current == null)
            {
                values[value] = new ListNode(newValue);
                return;
            }
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = new ListNode(newValue);
        }

        public void PrintTable()
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                {
                    Console.WriteLine($"{i}: ");
                    continue;
                }
                Console.WriteLine($"{i}: {ByPass(values[i])}");
            }
        }

        private string ByPass(ListNode root)
        {
            StringBuilder result = new StringBuilder();
            while (root != null)
            {
                result.Append($"{root.value} ");
                root = root.next;
            }

            return result.ToString();
        }

        private int HashFunction(int number) => number % _divider;
    }

}