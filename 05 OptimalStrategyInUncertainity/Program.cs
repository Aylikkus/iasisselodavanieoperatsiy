namespace OptimalStrategyInUncertainity
{
    class Program
    {
        public static void Main()
        {
            // Матрица из презентации
            double[,] values = {
                {5, 25, 19, 21},
                {9, 10, 8, 14},
                {18, 15, 24, 16},
                {12, 19, 23, 13}
            };
            PaymentMatrix matrix = new PaymentMatrix(values);

            Console.WriteLine("Минимаксный критерий Вальда: " + 
                matrix.GetOptimalStrategy(new ValdCriterion()));
            Console.WriteLine("Максимальный критерий: " + 
                matrix.GetOptimalStrategy(new MaxCriterion()));
            Console.WriteLine("Критерий Гурвица с Alpha=0.5: " + 
                matrix.GetOptimalStrategy(new GurvicCriterion(0.5)));
            Console.WriteLine("Критерий Сэвиджа: " + 
                matrix.GetOptimalStrategy(new SevidjCriterion()));
            Console.WriteLine("Критерий Лапласа: " + 
                matrix.GetOptimalStrategy(new LaplasCriterion()));
        }
    }
}