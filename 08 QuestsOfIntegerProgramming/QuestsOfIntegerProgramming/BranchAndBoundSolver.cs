using SimplexLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestsOfIntegerProgramming
{
    public class BranchAndBoundSolver
    {
        private SimplexMethod SimplexMethod { get; set; }
        private double[] xSolves;
        private double B;

        public BranchAndBoundSolver(TargetFunction targetFunction, Constraints constraints)
        {
            SimplexMethod = new SimplexMethod(constraints, targetFunction);

            (double[], double) SetOfSolves = SimplexMethod.SolveWithSimplexMethod();
            xSolves = SetOfSolves.Item1;
            B = SetOfSolves.Item2;
            SetOfSolves = BrandAndBoundSolve(xSolves, B, SimplexMethod);
        }

        // Вывод решения
        public void Print()
        {
            Console.WriteLine($"Значения X: ");
            for (int i = 0; i < xSolves.Length; i++)
            {
                Console.Write($"X{i+1} = {xSolves[i]}, ");
            }
            Console.WriteLine($"\nЗначение F={B}");
        }

        // Проверка на наличие нулевых решений
        private bool IsSolvesHaveZeroValues(double[] solves)
        {
            for (int i = 0; i < solves.Length; i++)            
            {
                if (solves[i] != 0) return false;
            }
            return true;
        }

        // Проверка на совместность системы
        private bool IsSystemCompatibility(double[] solves, double b)
        {
            if (IsSolvesHaveZeroValues(solves) && b == 0) return false;
            else return true;
        }

        // Вычисление целочисленного решения
        private (double[],double) BrandAndBoundSolve(double[] xSolves, double B, SimplexMethod sm) 
        {
            if (IsIntegerSolutions(xSolves))
            {
                return (this.xSolves = xSolves, this.B = B);

            }
            else
            {
                for (int i = 0; i < xSolves.Length; i++)
                {
                    if (!IsIntegerSolution(xSolves[i]))
                    {
                        Constraints constraints1 = new Constraints(sm.Constraints.Rows + 1, sm.Constraints.Columns);
                        LinearExpression le1 = new LinearExpression(sm.Constraints.Columns);
                        le1.Sign = Sign.LessThanEqual;
                        le1.B = ReturnFloorBound(xSolves[i]);
                        le1[i] = 1;
                        for (int j = 0; j < le1.CoefficientsCount(); j++)
                        {
                            if (i != j) le1[j] = 0;
                        }
                        for (int row = 0; row < sm.Constraints.Rows; row++)
                        {
                            constraints1[row] = sm.Constraints[row];
                        }
                        constraints1[constraints1.Rows - 1] = le1;
                        SimplexMethod sm1 = new SimplexMethod(constraints1, SimplexMethod.TargetFunction);
                        (double[], double) solves1 = sm1.SolveWithSimplexMethod();
                        double[] xSolves1 = solves1.Item1;
                        double B1 = solves1.Item2;
                        if (IsSystemCompatibility(xSolves1, B1))
                        {
                            BrandAndBoundSolve(xSolves1, B1, sm1);

                        }
                    }
                    else if (!IsIntegerSolution(xSolves[i]))
                    {
                        Constraints constraints2 = new Constraints(SimplexMethod.Constraints.Rows + 1, SimplexMethod.Constraints.Columns);
                        LinearExpression le2 = new LinearExpression(SimplexMethod.Constraints.Columns);
                        le2.Sign = Sign.GreaterThanEqual;
                        le2.B = ReturnCeilingBound(xSolves[i]);
                        le2[i] = 1;
                        for (int j = 0; j < le2.CoefficientsCount(); j++)
                        {
                            if (i != j) le2[j] = 0;
                        }
                        constraints2[constraints2.Rows] = le2;
                        SimplexMethod sm2 = new SimplexMethod(constraints2, SimplexMethod.TargetFunction);
                        (double[], double) solves2 = sm2.SolveWithSimplexMethod();
                        double[] xSolves2 = solves2.Item1;
                        double B2 = solves2.Item2;
                        if (IsSystemCompatibility(xSolves2, B2))
                        {
                            BrandAndBoundSolve(xSolves2, B2, sm2);
                            if (IsIntegerSolutions(xSolves2)) break;
                        }
                    }
                }
            }
            return (null, double.NaN);
        }

        // Является ли массив переменных целыми числами
        private bool IsIntegerSolutions(double[] solves)
        {
            for (int i = 0; i < solves.Length; i++)
            {
                if (solves[i] % 1 != 0) return false;
            }
            return true;
        }

        // Является ли переменная целым числом
        private bool IsIntegerSolution(double x)
        {
            if (x % 1 != 0)
            {
                return false;
            }
            return true;
        }

        // Возврат нижней границы
        private double ReturnFloorBound(double x)
        {
            return Math.Floor(x);
        }

        // Возвращение верхней границы
        private double ReturnCeilingBound(double x)
        {
            return Math.Ceiling(x);
        }
    }
}
