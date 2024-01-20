using SimplexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestsOfIntegerProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TargetFunction targetFunction = new TargetFunction(new double[] { 2, 1 }, 0, Target.Max);
            LinearExpression le1 = new LinearExpression(2);
            le1.B = 29;
            le1.Sign = Sign.LessThanEqual;
            le1[0] = 6;
            le1[1] = 2;
            LinearExpression le2 = new LinearExpression(2);
            le2.B = 55;
            le2.Sign = Sign.LessThanEqual;
            le2[0] = 10;
            le2[1] = 6;
            LinearExpression le3 = new LinearExpression(2);
            le3.B = 0;
            le3.Sign = Sign.GreaterThanEqual;
            le3[0] = 1;
            le3[1] = 0;
            LinearExpression le4 = new LinearExpression(2);
            le4.B = 0;
            le4.Sign = Sign.GreaterThanEqual;
            le4[0] = 0;
            le4[1] = 1;
            Constraints constraints = new Constraints(4, 2);
            constraints[0] = le1;
            constraints[1] = le2;
            constraints[2] = le3;
            constraints[3] = le4;
            BranchAndBoundSolver solution = new BranchAndBoundSolver(targetFunction, constraints);
            solution.Print();
            Console.Read();

        }
    }
}
