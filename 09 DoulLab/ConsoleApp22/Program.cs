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
            le5[0] = 0;
            le5[1] = 0;
            le5.Sign = Sign.GreaterThanEqual;
            le5.B = 0;

            Constraints constraints = new Constraints(5, 2);
            constraints[0] = le1;
            constraints[1] = le2;
            constraints[2] = le3;
            constraints[3] = le4;
            constraints[4] = le5;


            SimplexMethod simplexMethod = new SimplexMethod(constraints, targetFunction);
            var res1 = simplexMethod.SolveWithSimplexMethod();


            //Работа с двойственной функцией
            DataModel dataModel = new DataModel(targetFunction, le1, le2, le3, le4, le5);
            dataModel.BuildingExtendedMatrix(dataModel.Constraints);
            dataModel.TransportationA();
            dataModel.GetDualFunction();
            dataModel.GetDualConstraints();


            SimplexMethod simplexMethod1 = new SimplexMethod(dataModel.DualConstraints, dataModel.DualFunction);
            var res2 = simplexMethod1.SolveWithSimplexMethod();

            Console.WriteLine($"Целевая функция: {res1}");
            Console.WriteLine($"Двойственная функция: {res2}");

        }
    }
}
