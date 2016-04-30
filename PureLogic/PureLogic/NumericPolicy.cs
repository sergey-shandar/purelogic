namespace PureLogic
{
    public interface INumericPolicy<T>
    {
        T FromLong(long x);
        T Plus(T a, T b);
        T Div(T a, T b);
    }

    public struct NumericPolicy :
        INumericPolicy<long>,
        INumericPolicy<double>,
        INumericPolicy<decimal>
    {
        long INumericPolicy<long>.FromLong(long x) => x;
        double INumericPolicy<double>.FromLong(long x) => x;
        decimal INumericPolicy<decimal>.FromLong(long x) => x;

        public long Plus(long a, long b) => a + b;
        public double Plus(double a, double b) => a + b;
        public decimal Plus(decimal a, decimal b) => a + b;

        public long Div(long a, long b) => a / b;
        public double Div(double a, double b) => a / b;
        public decimal Div(decimal a, decimal b) => a / b;
    }
}
