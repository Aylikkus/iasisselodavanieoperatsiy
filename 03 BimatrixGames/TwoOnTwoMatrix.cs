namespace BimatrixGames
{
    public class TwoOnTwoMatrix
    {
        private double[,] matrix = new double[2,2];

        public TwoOnTwoMatrix(double a11, double a12, double a21, double a22)
        {
            matrix[0,0] = a11;
            matrix[0, 1] = a12;
            matrix[1,0] = a21;
            matrix[1,1] = a22;
        }

        public double this[int i, int j]
        {
            get
            {
                return matrix[i - 1, j - 1];
            }
            set
            {
                matrix[i - 1, j - 1] = value;
            }
        }
    }
}
