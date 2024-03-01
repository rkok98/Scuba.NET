using UnitsNet;

namespace Domain.Nitrox.Services;

using Depth = Length;

/// <summary>
///     This class is responsible for calculating the ambient pressure experienced at a given depth underwater.
/// </summary>
public interface IAmbientPressureCalculator
{
    /// <summary>
    ///     Calculates the total ambient pressure at a given depth underwater.
    /// </summary>
    /// <param name="depth">
    ///     The depth underwater for which to calculate the pressure. Measured in Length (which is aliased to
    ///     'Depth').
    /// </param>
    /// <param name="waterDensity">The density of the water, which affects the water pressure component.</param>
    /// <returns>The total ambient pressure at the specified depth as a Pressure object.</returns>
    public Pressure CalculateAmbientPressure(Depth depth, Density waterDensity);
}