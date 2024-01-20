using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateAndDominantStrategies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //PaymentMatrix matrix = new PaymentMatrix(new int[,] { { 4, 4, 2, 8 },
            //                                                      { 8, 3, 0, 8 },
            //                                                      { 3, 4, 2, 0 },
            //                                                      { 2, 1, 8, 0 },
            //                                                      { 4, 6, 7, 8 } });
            PaymentMatrix matrix = new PaymentMatrix(3, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix.ChangeElem(i, j, rnd.Next(0, 10));
                }
            }
            //matrix.AddColumn(new[,] { {6},{9},{5},{8},{5} });
            matrix.Print();
            if (!matrix.FindSaddlePoint())
            {
                matrix.RemoveDuplicateStrategies();
                matrix.RemoveDominantStrategies();
                matrix.Print();
            }
            Console.Read();
        }
    }
}
