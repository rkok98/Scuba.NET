using Domain.Nitrox.Models;
using UnitsNet;

namespace Domain.Nitrox.Calculators;

/// <summary>
///     Provides functionality to calculate the best nitrox mix for a given depth based on a desired partial pressure of
///     oxygen.
/// </summary>
public interface IBestNitroxForDepthCalculator
{
    /// <summary>
    ///     Calculates the most suitable nitrox gas mix for a given depth and desired partial pressure of oxygen.
    /// </summary>
    /// <param name="depth">The depth for which the nitrox mix is being calculated, measured in Length (aliased to 'Depth').</param>
    /// <param name="partialPressureOfOxygen">The desired partial pressure of oxygen at that depth.</param>
    /// <param name="waterDensity">The density of the water, which affects the ambient pressure calculation.</param>
    /// <returns>A NitroxGas object representing the best nitrox mix for the specified depth and partial pressure of oxygen.</returns>
    public NitroxGas CalculateBestNitroxForDepth(Depth depth, Pressure partialPressureOfOxygen, Density waterDensity);
}