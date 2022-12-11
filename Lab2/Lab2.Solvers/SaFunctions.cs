namespace Lab2.Solvers;

/// <param name="delta">Negative if improving.</param>
public delegate double ProbabilityFunction(double temperature, double delta);

public delegate double DecrementFunction(double temperature, int iteration);

public static class SaFunctions
{
    public static double VerySlowDecrement(double temperature, int iteration)
        => temperature / (1 + 0.2 * temperature);

    public static double LinearDecrement(double temperature, int iteration)
        => temperature * 0.95;

    /// <summary>
    /// Accept if improving else accept with certain probability.
    /// </summary>
    /// <inheritdoc cref="ProbabilityFunction"/>
    public static double DefaultProbability(double temperature, double delta)
    {
        if (delta <= 0)
            return 1;

        var exponent = -delta / temperature;
        var p = Math.Exp(exponent);
        return p;
    }

    /// <summary>
    /// Accept with a certain probability.
    /// </summary>
    /// <inheritdoc cref="ProbabilityFunction"/>
    public static double AlternativeProbability(double temperature, double delta)
    {
        var exponent = delta / temperature;
        var e = Math.Exp(exponent);
        var p = 1 / (1 + e);
        return p;
    }
}
