namespace MatrixGames
{
    class Program
    {
        private static void printOptimal(double[] a, double[] b)
        {
            Console.Write("Оптимальные решения игрока A: ");
            foreach (var d in a)
                Console.Write(d.ToString("0.000") + " ");
            
            Console.Write("\nОптимальные решения игрока B: ");
            foreach (var d in b)
                Console.Write(d.ToString("0.000") + " ");
        }

        public static void Main()
        {
            double[,] matrix = {
                {2, 3, 7},
                {5, 4, 6},
                {6, 2, 1}
            };

            Console.WriteLine("Решение для платёжной матрицы с седловой точкой.");
            PaymentMatrix sadleMatrix = new PaymentMatrix(matrix);
            (double[] a, double[] b) = sadleMatrix.Solve();
            printOptimal(a, b);

            matrix = new double[,] {
                {4, 0, 7},
                {6, 6, 2},
                {1, 0, 3}
            };

            Console.WriteLine("\n\nРешение для платёжной матрицы без седловой точки (методом линейного программирования).");
            PaymentMatrix noSadleMatrix = new PaymentMatrix(matrix);
            (a, b) = noSadleMatrix.Solve();
            printOptimal(a, b);
        }
    }
}