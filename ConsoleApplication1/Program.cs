using System;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int num = 0;

            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    num += ticTacToe[i, j];
                    count++;
                }
            }

            Console.WriteLine(num / count);
        }
    }
}