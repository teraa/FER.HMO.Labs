namespace Lab2.Solvers;

public static class SaFunctions
{
    public static double DefaultDecrement(double temperature, int iteration)
        => temperature * 0.5;

    /// <summary>
    /// Accept if improving else accept with certain probability.
    /// </summary>
    public static double DefaultProbability(double temperature, double currentValue, double newValue)
    {
        // minimization
        // < 0 better
        // = 0 equal
        // > 0 worse
        var delta = newValue - currentValue;
        if (delta <= 0)
            return 1;

        var exponent = -delta / temperature;
        var p = Math.Exp(exponent);
        return p;
    }

    /// <summary>
    /// Accept with a certain probability.
    /// </summary>
    public static double AlternativeProbability(double temperature, double currentValue, double newValue)
    {
        // minimization
        var delta = newValue - currentValue;
        var exponent = delta / temperature;
        var e = Math.Exp(exponent);
        var p = 1 / (1 + e);
        return p;
    }
}