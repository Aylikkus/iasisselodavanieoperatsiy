using System;
using System.Collections.Generic;
using System.Text;
using SimplexLib;

namespace ConsoleApp22
{
    class DataModel
    {
        private TargetFunction targetFunction;

        public TargetFunction TargetFunction
        {
            get { return targetFunction; }
            set { targetFunction = value; }
        }

        private TargetFunction dualFunction;

        public TargetFunction DualFunction
        {
            get { return dualFunction; }
            set { dualFunction = value; }
        }


        private Constraints dualConstraints;

        public Constraints DualConstraints
        {
            get { return dualConstraints; }

            set { dualConstraints = value; }
        }

        private Constraints constraints;

        public Constraints Constraints
        {
            get { return constraints; }

            set { constraints = value; }
        }


        private double[,] a;

        public double[,] A
        {
            get { return a; }

            set { a = value; }
        }

        private double[,] at;

        public double[,] At
        {
            get { return at; }

            set { at = value; }
        }

        public DataModel(TargetFunction targetFunction, params LinearExpression[] linearExpression)
        {
            TargetFunction = targetFunction;

            Constraints = new Constraints(linearExpression.Length - 1, TargetFunction.coefficients.Length - 1);
            for (int i = 0; i < linearExpression.Length - 1; i++)
            {
                Constraints[i] = linearExpression[i];
            }
            A = new double[linearExpression.Length , TargetFunction.coefficients.Length + 1];
            At = new double[TargetFunction.coefficients.Length + 1, linearExpression.Length];
        }


        public double[,] BuildingExtendedMatrix(Constraints constraints)
        {
            for (int i = 0; i <= constraints.Rows - 1; i++)
            {
                for (int j = 0; j <= constraints.Columns + 1; j++)
                {
                    if (j == constraints.Columns + 1)
                        A[i, j] = constraints[i].B;
                    else
                        A[i, j] = constraints[i, j];        //Не уверен
                }
            }

            for (int i = 0; i <= constraints.Columns + 1; i++)
            {
                if (i == constraints.Columns + 1)
                    A[constraints.Rows, i] = 0;              // f(x) || f(y)
                else
                    A[constraints.Rows, i] = TargetFunction[i];
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

        // Если целевая функция стремится к max => Двойственная будет стремится к min
        // dualTarget = Min , если целевая стремится к max

        Target dualTarget = Target.Min;
        public TargetFunction GetDualFunction()
        {
            if (TargetFunction.Target == Target.Min)
                dualTarget = Target.Max;


            double[] coefficients = new double[At.GetLength(1) - 1];
            for (int i = 0; i < At.GetLength(1) - 1; i++)
            {
                coefficients[i] = At[At.GetLength(0) - 1, i];
            }

            DualFunction = new TargetFunction(coefficients, 0, dualTarget);
            return DualFunction;
        }

        public Constraints GetDualConstraints()
        {
            int countLinerExpression = At.GetLength(0) - 1;
            DualConstraints = new Constraints(At.GetLength(0) - 1, countLinerExpression);

            int i = 0;
            LinearExpression le;
            while(i < countLinerExpression)
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
            return DualConstraints;
        }
       

    }
}
