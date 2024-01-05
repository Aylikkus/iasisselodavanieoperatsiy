namespace BimatrixGames
{
    class Program
    {
        public static void Main()
        {
            DataModel model = new DataModel();

            var p = model.P;
            var q = model.Q;
            Console.WriteLine($"P* = [{p.Item1}, {p.Item2}]");
            Console.WriteLine($"Q* = [{q.Item1}, {q.Item2}]");
            Console.WriteLine($"Средний выигрыш игрока A (1-ая стратегия): " +
                $"{model.AverageWin(false, false)}");
            Console.WriteLine($"Средний выигрыш игрока B (1-ая стратегия): " +
                $"{model.AverageWin(true, false)}");
            Console.WriteLine($"Средний выигрыш игрока A (2-ая стратегия): " +
                $"{model.AverageWin(false, true)}");
            Console.WriteLine($"Средний выигрыш игрока B (2-ая стратегия): " +
                $"{model.AverageWin(true, true)}");

            int x = 0;
        }
    }
}