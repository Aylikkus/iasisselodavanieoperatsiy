using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateAndDominantStrategies
{
    internal class PaymentMatrix
    {
        private int[,] matrix;


        public PaymentMatrix(int rows, int columns)
        {
            matrix = new int[rows, columns];
        }

        public PaymentMatrix(int[,] matrix)
        {
            this.matrix = matrix;
        }

        public void RemoveDominantStrategies()
        {            
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            bool haveChanges = true;

            while (haveChanges)
            {
                haveChanges = false;
                // Поиск доминирующих строк
                for (int i = 0; i < rows; i++)
                {
                    int dominantRowIndex = 0;
                    bool isDominant = false;
                    for (int j = 0; j < rows; j++)
                    {
                        if (i != j && IsRowDominant(matrix, i, j))
                        {
                            isDominant = true;
                            dominantRowIndex = j;
                            break;
                        }
                    }

                    if (isDominant)
                    {
                        matrix = RemoveRow(matrix, dominantRowIndex);
                        rows = matrix.GetLength(0);
                        columns = matrix.GetLength(1);
                        haveChanges = true;
                    }
                }

                // Поиск доминирующих столбцов
                for (int j = 0; j < columns; j++)
                {
                    int dominantColumnIndex = 0;
                    bool isDominant = false;
                    for (int i = 0; i < columns; i++)
                    {
                        if (i != j && IsColumnDominant(matrix, i, j))
                        {
                            isDominant |= true;
                            dominantColumnIndex = j;
                            break;
                        }
                    }
                    if (isDominant)
                    {
                        matrix = RemoveColumn(matrix, dominantColumnIndex);
                        rows = matrix.GetLength(0);
                        columns = matrix.GetLength(1);
                        haveChanges = true;
                    }
                }
            }

            


        }

        public bool IsColumnDominant(int[,] matrix, int targetColumnIndex, int stepColumnIndex)
        {
            int rows = matrix.GetLength(0);

            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, targetColumnIndex] > matrix[i, stepColumnIndex])
                {
                    return false;
                }
            }

            return true;
        }

        public void RemoveRowDominantStrategies()
        {
            
        }

        public int[,] RemoveColumn(int[,] matrix, int columnIndex)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1) - 1;

            int[,] reducedMatrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                int reducedColumn = 0;

                for (int j = 0; j < columns + 1; j++)
                {
                    if (j != columnIndex)
                    {
                        reducedMatrix[i, reducedColumn] = matrix[i, j];
                        reducedColumn++;
                    }
                }
            }

            return reducedMatrix;
        }

        // Проверка на доминирование целевой строки над другой
        private bool IsRowDominant(int[,] matrix, int targetRowIndex, int stepRowIndex)
        {
            int columns = matrix.GetLength(1);

            for (int j = 0; j < columns; j++)
            {
                if (matrix[targetRowIndex, j] < matrix[stepRowIndex, j])
                {
                    return false;
                }
            }

            return true;
        }

        public int[,] RemoveRow(int[,] matrix, int rowIndex)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            int[,] reducedMatrix = new int[rows - 1, columns];
            int newRow = 0;

            for (int i = 0; i < rows; i++)
            {
                if (i != rowIndex)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        reducedMatrix[newRow, j] = matrix[i, j];
                    }
                    newRow++;
                }
            }

            return reducedMatrix;
        }

        public bool FindSaddlePoint()
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            int[,] operationMatrix = new int[rows + 1, columns + 1];

            // Копирование значений из исходной матрицы в операционную матрицу
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    operationMatrix[i, j] = matrix[i, j];
                }
            }

            // Вычисление нижней цены игры (max по строкам) и поиск максимального значения и его позиции
            int maxRowMinValue = int.MinValue;
            int maxRowMinStrategy = 0;
            for (int i = 0; i < rows; i++)
            {
                int rowMinValue = operationMatrix[i, 0];
                for (int j = 1; j < columns; j++)
                {
                    if (operationMatrix[i, j] < rowMinValue)
                    {
                        rowMinValue = operationMatrix[i, j];
                    }
                }

                operationMatrix[i, columns] = rowMinValue;

                if (rowMinValue > maxRowMinValue)
                {
                    maxRowMinValue = rowMinValue;
                    maxRowMinStrategy = i;
                }
            }

            // Вычисление верхней цены игры (min по столбцам) и поиск минимального значения и его позиции
            int minColumnMaxValue = int.MaxValue;
            int minColumnMaxStrategy = 0;
            for (int j = 0; j < columns; j++)
            {
                int columnMaxValue = operationMatrix[0, j];
                for (int i = 1; i < rows; i++)
                {
                    if (operationMatrix[i, j] > columnMaxValue)
                    {
                        columnMaxValue = operationMatrix[i, j];
                    }
                }

                operationMatrix[rows, j] = columnMaxValue;

                if (columnMaxValue < minColumnMaxValue)
                {
                    minColumnMaxValue = columnMaxValue;
                    minColumnMaxStrategy = j;
                }
            }

            // Проверка наличия седловой точки
            if (maxRowMinValue == minColumnMaxValue)
            {
                Console.WriteLine($"Нижняя цена игры: {maxRowMinValue};\nВерхняя цена игры: {minColumnMaxValue};\n" +
                    $"Седловая точка ({maxRowMinStrategy}, {minColumnMaxStrategy}) указывает решение на пару альтернатив:\n" +
                    $"(A{maxRowMinStrategy}, B{minColumnMaxStrategy}).\n" +
                    $"Цена игры равна: {operationMatrix[maxRowMinStrategy, minColumnMaxStrategy]}");
                return true;
            }
            else
            {
                Console.WriteLine($"Нижняя цена игры: {maxRowMinValue};\nВерхняя цена игры: {minColumnMaxValue};\nСедловая точка отсутствует!");
                return false;
            }
        }


        public void RemoveDuplicateStrategies()
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            List<int> uniqueRows = new List<int>();
            List<int> uniqueColumns = new List<int>();

            // Поиск уникальных строк
            for (int i = 0; i < rows; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < uniqueRows.Count; j++)
                {
                    if (IsRowEqual(matrix, i, uniqueRows[j]))
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    uniqueRows.Add(i);
                }
            }

            // Поиск уникальных столбцов
            for (int j = 0; j < columns; j++)
            {
                bool isDuplicate = false;
                for (int i = 0; i < uniqueColumns.Count; i++)
                {
                    if (IsColumnEqual(matrix, j, uniqueColumns[i]))
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    uniqueColumns.Add(j);
                }
            }

            // Создание новой матрицы с уникальными строками и столбцами
            int[,] uniqueMatrix = new int[uniqueRows.Count, uniqueColumns.Count];
            for (int i = 0; i < uniqueRows.Count; i++)
            {
                for (int j = 0; j < uniqueColumns.Count; j++)
                {
                    uniqueMatrix[i, j] = matrix[uniqueRows[i], uniqueColumns[j]];
                }
            }

            // Замена исходной матрицы на новую с уникальными строками и столбцами
            matrix = uniqueMatrix;
        }

        private bool IsRowEqual(int[,] matrix, int row1, int row2)
        {
            int columns = matrix.GetLength(1);
            for (int j = 0; j < columns; j++)
            {
                if (matrix[row1, j] != matrix[row2, j])
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsColumnEqual(int[,] matrix, int column1, int column2)
        {
            int rows = matrix.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, column1] != matrix[i, column2])
                {
                    return false;
                }
            }
            return true;
        }


        public void AddColumn(int[,] columnMatrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            int[,] newMatrix = new int[rows, columns + 1];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }

                newMatrix[i, columns] = columnMatrix[i, 0];
            }
            matrix = newMatrix;
        }

        public void AddRow(int[,] rowMatrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            int[,] newMatrix = new int[rows + 1, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            for (int j = 0; j < columns; j++)
            {
                newMatrix[rows, j] = rowMatrix[0, j];
            }

            matrix = newMatrix;
        }

        public void ChangeElem(int i, int j, int value)
        {
            matrix[i, j] = value;
        }

        public void Print()
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            Console.WriteLine($"Платежная матрица:\n" +
                $"Размер: {rows} x {columns}");
            Console.Write("|  |");
            for (int temp = 0; temp < matrix.GetLength(1); temp++)
            {
                Console.Write($" B{temp}|");
            }
            Console.WriteLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write($"|A{i}|");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($" {matrix[i,j]} |");
                }
                
                Console.WriteLine();
            }
        }
    }  
}
