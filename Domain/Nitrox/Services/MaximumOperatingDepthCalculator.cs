using UnitsNet;

namespace Domain.Nitrox.Services;

using Depth = Length;

public class MaximumOperatingDepthCalculator : IMaximumOperatingDepthCalculator
{
    public Depth CalculateMaximumOperatingDepth(Pressure partialPressureO2, double fractionOfO2)
    {
        if (fractionOfO2 is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(fractionOfO2), "Fraction must be between 0 and 1");

        var mod = 10 * (partialPressureO2.Bars / fractionOfO2 - 1.0);
        return Depth.FromMeters(mod);
    }
}