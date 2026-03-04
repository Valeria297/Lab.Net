using System;
namespace CalculatorExample
{
    class Program
    {
        static void Main()
        {
            Calc calculator = new Calc();
            int summ = calculator.Add(x: 5, y: 11);

            Console.WriteLine("5 + 11 is {0}.", summ);
            Console.ReadLine();
        }
    }

    class Calc
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
