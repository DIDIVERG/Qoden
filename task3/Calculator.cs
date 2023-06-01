using System;
using System.Collections.Generic;

namespace task3
{
    public static class Calculator
    {
        public static void Calculate(string expression)
        {
            var tokens = expression.Split(' ');
            Stack<int> operands = new Stack<int>();
            foreach (var token in tokens)
            {
                if (int.TryParse(token, out int number))
                {
                    operands.Push(number);
                }
                else
                {
                    int operand2;
                    int operand1;
                    if (operands.Count >= 2)
                    {
                        operand2 = operands.Pop();
                        operand1 = operands.Pop();
                    }
                    else
                    {
                        throw new ArgumentException("Неверное выражение");
                    }
                    int result = Solve(operand1, operand2, token);
                    operands.Push(result);
                }
            }
            if (operands.Count == 1)
            {
                Console.WriteLine(operands.Pop());
            }
            else
            {
                throw new ArgumentException("Неверное выражение");
            }
        }
        
        private static int Solve(int operand1, int operand2, string operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand2 == 0 ? throw new DivideByZeroException() : operand1 / operand2;
                default:
                    throw new ArgumentException("Недопустимый оператор: " + operatorSymbol);
            }
        }
    }
}