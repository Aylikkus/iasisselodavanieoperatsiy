using SimplexLib;
using System;

namespace ConsoleApp22
{
    class Program
    {
        static void Main(string[] args)
        {
            TargetFunction targetFunction = new TargetFunction(new double[] { 2, 1 }, 0, Target.Max);
            
            LinearExpression le1 = new LinearExpression(2);
            le1[0] = 3;
            le1[1] = 1;
            le1.Sign = Sign.LessThanEqual;
            le1.B = 11;
            LinearExpression le2 = new LinearExpression(2);
            le2[0] = 1;
            le2[1] = 2;
            le2.Sign = Sign.LessThanEqual;
            le2.B = 12;
            LinearExpression le3 = new LinearExpression(2);
            le3[0] = 0;
            le3[1] = 1;
            le3.Sign = Sign.LessThanEqual;
            le3.B = 7;
            LinearExpression le4 = new LinearExpression(2);
            le4[0] = 3;
            le4[1] = 0;
            le4.Sign = Sign.LessThanEqual;
            le4.B = 6;
            LinearExpression le5 = new LinearExpression(2);
            le5[0] = 1;
            le5[1] = 0;
            le5.Sign = Sign.GreaterThanEqual;
            le5.B = 0;
            LinearExpression le6 = new LinearExpression(2);
            le6[0] = 0;
            le6[1] = 1;
            le6.Sign = Sign.GreaterThanEqual;
            le6.B = 0;

            Constraints constraints = new Constraints(6, 2);
            constraints[0] = le1;
            constraints[1] = le2;
            constraints[2] = le3;
            constraints[3] = le4;
            constraints[4] = le5;
            constraints[5] = le6;


            SimplexMethod simplexMethod = new SimplexMethod(constraints, targetFunction);
            var res1 = simplexMethod.SolveWithSimplexMethod();


            Constraints constraints2 = new Constraints(4, 2);
            constraints2[0] = le1;
            constraints2[1] = le2;
            constraints2[2] = le3;
            constraints2[3] = le4;
            //Работа с двойственной функцией
            DataModel dataModel = new DataModel(targetFunction, constraints2);
            dataModel.BuildingExtendedMatrix();
            dataModel.TransportationA();
            dataModel.GetDualFunction();
            dataModel.GetDualConstraints();


            SimplexMethod simplexMethod2 = new SimplexMethod(dataModel.DualConstraints, dataModel.DualFunction);
            var res2 = simplexMethod2.SolveWithSimplexMethod();

            Console.WriteLine($"Целевая функция:");
            Print(res1);
            Console.WriteLine($"Двойственная функция: ");
            Print(res2);
            Console.Read();
        }

        public static void Print((double[], double) res)
        {
            for (int i = 0; i < res.Item1.Length; i++)
            {                
                Console.Write($"X[{i}]={res.Item1[i]} ");
            }
            Console.Write($"F={res.Item2}");
            Console.WriteLine();
        }
    }
}
