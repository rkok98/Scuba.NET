using UnitsNet;

namespace Domain.Nitrox.Calculators;

/// <summary>
///     This class is responsible for calculating the maximum operating depth (MOD) for a given partial pressure of oxygen
///     and fraction of oxygen in a gas mix.
/// </summary>
public interface IMaximumOperatingDepthCalculator
{
    /// <summary>
    ///     Calculates the maximum operating depth based on the partial pressure of oxygen and the fraction of oxygen in the
    ///     breathing gas.
    /// </summary>
    /// <param name="partialPressureO2">The partial pressure of oxygen in the gas mix.</param>
    /// <param name="fractionOfO2">The fraction of oxygen in the gas mix, expressed as a value between 0 and 1.</param>
    /// <returns>The maximum operating depth as a Depth object.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the fraction of O2 is not between 0 and 1.</exception>
    public Depth CalculateMaximumOperatingDepth(Pressure partialPressureO2, double fractionOfO2);
}