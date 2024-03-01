namespace Domain.Trimix.Models;

public readonly record struct TrimixGas
{
    public TrimixGas(decimal oxygenFraction, decimal nitrogenFraction, decimal heliumFraction)
    {
        if (oxygenFraction + nitrogenFraction + heliumFraction != 1)
            throw new ArgumentException("Oxygen, nitrogen and helium percentages must add up to 1");

        OxygenFraction = oxygenFraction;
        HeliumFraction = heliumFraction;
        NitrogenFraction = nitrogenFraction;
    }

    public TrimixGas(decimal oxygenFraction, decimal heliumFraction)
    {
        if (oxygenFraction + heliumFraction is < 0 or > 1)
            throw new ArgumentOutOfRangeException(null, "The sum of oxygen and helium must be between 0 and 1");

        var nitrogenFraction = 1 - oxygenFraction - heliumFraction;

        OxygenFraction = oxygenFraction;
        HeliumFraction = heliumFraction;
        NitrogenFraction = nitrogenFraction;
    }

    public decimal OxygenFraction { get; }
    public decimal HeliumFraction { get; }
    public decimal NitrogenFraction { get; }

    public decimal OxygenPercentage => OxygenFraction * 100;
    public decimal HeliumPercentage => HeliumFraction * 100;

    public decimal NitrogenPercentage => NitrogenFraction * 100;

    public string Tx => $"TX {Math.Floor(OxygenPercentage)}/{Math.Floor(HeliumPercentage)}";

    public override string ToString()
    {
        return Tx;
    }
}