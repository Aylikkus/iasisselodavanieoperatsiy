using System;
using System.Collections.Generic;
using System.Text;
using SimplexLib;

namespace ConsoleApp22
{
    class DataModel
    {

        public TargetFunction TargetFunction { get; set; }

        public TargetFunction DualFunction { get; set; }


        public Constraints DualConstraints { get; set; }

        public Constraints Constraints { get; set; }

        public double[,] A { get; set; }

        public double[,] At { get; set; }

        Target dualTarget;
        public DataModel(TargetFunction targetFunction, Constraints constraints)
        {
            TargetFunction = targetFunction;
            Constraints = constraints;
            A = new double[constraints.Rows + 1, constraints.Columns + 1];
            At = new double[constraints.Columns + 1,constraints.Rows + 1];
        }


        public double[,] BuildingExtendedMatrix()
        {
            for (int i = 0; i < Constraints.Rows; i++)
            {
                for (int j = 0; j <= Constraints.Columns; j++)
                {
                    if (j == Constraints.Columns)
                        A[i, j] = Constraints[i].B;
                    else
                        A[i, j] = Constraints[i, j];        
                }
            }

            for (int j = 0; j < TargetFunction.CoefficientsCount; j++)
            {
                A[A.GetLength(0) - 1, j] = TargetFunction.coefficients[j];
            }

            return A;
        }

        public double[,] TransportationA()
        {
            for(int i = 0; i < A.GetLength(1); i++)
            {
                for(int j = 0; j < A.GetLength(0); j++)
                {
                    At[i, j] = A[j, i];
                }
            }
            return At;
        }

        
        public TargetFunction GetDualFunction()
        {
            if (TargetFunction.Target == Target.Min) dualTarget = Target.Max;
            else dualTarget = Target.Min;


            double[] coefficients = new double[At.GetLength(1) - 1];
            for (int j = 0; j < At.GetLength(1) - 1; j++)
            {
                coefficients[j] = At[At.GetLength(0) - 1, j];
            }

            DualFunction = new TargetFunction(coefficients, 0, dualTarget);
            return DualFunction;
        }

        public Constraints GetDualConstraints()
        {
            int i = 0;
            LinearExpression le;
            int countLE = At.GetLength(0) - 1;
            DualConstraints = new Constraints(countLE + (At.GetLength(1) - 1), At.GetLength(1) - 1);

            while(i < countLE)
            {
                le = new LinearExpression(At.GetLength(1) - 1);
                for(int j = 0; j <= At.GetLength(1) - 1; j++)
                {
                    if (j == At.GetLength(1) - 1)
                        le.B = At[i, j];
                    else
                        le[j] = At[i, j];
                }
                le.Sign = Sign.GreaterThanEqual;      
                DualConstraints[i] = le;
                i++;
            }
            for (int e = countLE; e < countLE + At.GetLength(1) - 1; e++)
            {
                le = new LinearExpression(At.GetLength(1) - 1);
                for (int j = 0; j < At.GetLength(1) - 1; j++)
                {
                    if (j == e - countLE) le[j] = 1;
                    else le[j] = 0;
                }
                le.Sign = Sign.GreaterThanEqual;
                DualConstraints[e] = le;
            }

            return DualConstraints;
        }
       

    }
}
