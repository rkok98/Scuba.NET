namespace Domain.Nitrox.Models;

/// <summary>
///     Represents a Nitrox gas mixture, commonly used in scuba diving,
///     defined by its oxygen and nitrogen fractions.
/// </summary>
public readonly record struct NitroxGas
{
    /// <summary>
    ///     Initializes a new instance of the NitroxGas struct with specified oxygen and nitrogen fractions.
    ///     Validates the input fractions to ensure they are within acceptable ranges and their sum equals 1.
    /// </summary>
    /// <param name="oxygenFraction">The fraction of oxygen in the mixture.</param>
    /// <param name="nitrogenFraction">The fraction of nitrogen in the mixture.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if either fraction is not between 0 and 1.</exception>
    /// <exception cref="ArgumentException">Thrown if the sum of oxygen and nitrogen fractions does not equal 1.</exception>
    public NitroxGas(decimal oxygenFraction, decimal nitrogenFraction)
    {
        ValidateFractions(oxygenFraction, nitrogenFraction);

        OxygenFraction = oxygenFraction;
        NitrogenFraction = nitrogenFraction;
    }

    /// <summary>
    ///     Initializes a new instance of the NitroxGas struct with a specified oxygen fraction.
    ///     Calculates the nitrogen fraction as the complement to 1 and validates both fractions.
    /// </summary>
    /// <param name="oxygenFraction">The fraction of oxygen in the mixture.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if oxygen fraction is not between 0 and 1.</exception>
    /// <exception cref="ArgumentException">Thrown if the sum of oxygen and nitrogen fractions does not equal 1.</exception>
    public NitroxGas(decimal oxygenFraction)
    {
        var nitrogenFraction = 1 - oxygenFraction;

        ValidateFractions(oxygenFraction, nitrogenFraction);

        OxygenFraction = oxygenFraction;
        NitrogenFraction = nitrogenFraction;
    }

    /// <summary>
    ///     Gets the fraction of oxygen in the Nitrox mixture.
    /// </summary>
    /// <value>
    ///     The fraction of oxygen, a decimal between 0 and 1.
    /// </value>
    public decimal OxygenFraction { get; }

    /// <summary>
    ///     Gets the fraction of nitrogen in the Nitrox mixture.
    /// </summary>
    /// <value>
    ///     The fraction of nitrogen, a decimal between 0 and 1.
    /// </value>
    public decimal NitrogenFraction { get; }

    /// <summary>
    ///     Gets the oxygen percentage of the mixture.
    /// </summary>
    public decimal OxygenPercentage => OxygenFraction * 100;

    /// <summary>
    ///     Gets the nitrogen percentage of the mixture.
    /// </summary>
    public decimal NitrogenPercentage => NitrogenFraction * 100;

    /// <summary>
    ///     Gets the Enriched Air Nitrox (EAN) label of the gas mixture.
    /// </summary>
    public string Ean => Math.Floor(OxygenPercentage) > 100 ? "EAN" + Math.Floor(OxygenPercentage) : "O2";

    /// <summary>
    ///     Returns a string representation of the Nitrox gas mixture in the EAN format.
    /// </summary>
    /// <returns>A string representing the EAN label of the gas mixture.</returns>
    public override string ToString()
    {
        return Ean;
    }

    /// <summary>
    ///     Validates the oxygen and nitrogen fractions.
    ///     Ensures that each fraction is between 0 and 1 and their sum equals 1.
    /// </summary>
    /// <param name="oxygenFraction">The fraction of oxygen in the mixture.</param>
    /// <param name="nitrogenFraction">The fraction of nitrogen in the mixture.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if either fraction is not between 0 and 1.</exception>
    /// <exception cref="ArgumentException">Thrown if the sum of oxygen and nitrogen fractions does not equal 1.</exception>
    private static void ValidateFractions(decimal oxygenFraction, decimal nitrogenFraction)
    {
        if (oxygenFraction is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(oxygenFraction), "Oxygen fraction must be between 0 and 1");

        if (nitrogenFraction is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(nitrogenFraction),
                "Nitrogen fraction must be between 0 and 1");

        if (oxygenFraction + nitrogenFraction != 1)
            throw new ArgumentException("Oxygen and nitrogen fractions must add up to 1");
    }
}