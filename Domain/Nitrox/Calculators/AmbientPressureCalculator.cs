using UnitsNet;
using UnitsNet.Units;

namespace Domain.Nitrox.Calculators;

public class AmbientPressureCalculator : IAmbientPressureCalculator
{
    // The atmospheric pressure at sea level in atmospheres.
    private static readonly Pressure AtmosphericPressure = Pressure.FromAtmospheres(1);

    private static readonly Acceleration Gravity = Acceleration.FromMetersPerSecondSquared(9.81);

    public Pressure CalculateAmbientPressure(Depth depth, Density waterDensity)
    {
        if (depth < Depth.Zero)
            throw new ArgumentException("Depth must be non-negative.", nameof(depth));

        if (waterDensity <= Density.Zero)
            throw new ArgumentException("Water density must be larger then zero.", nameof(waterDensity));

        return AtmosphericPressure + CalculateWaterPressure(depth, waterDensity);
    }

    /// <summary>
    ///     Calculates the pressure exerted by the water at a given depth.
    /// </summary>
    /// <param name="depth">The depth underwater for which to calculate the water pressure.</param>
    /// <param name="waterDensity">The density of the water.</param>
    /// <returns>The water pressure at the specified depth as a Pressure object.</returns>
    private static Pressure CalculateWaterPressure(Depth depth, Density waterDensity)
    {
        return (waterDensity * Gravity * depth).ToUnit(PressureUnit.Bar);
    }
}