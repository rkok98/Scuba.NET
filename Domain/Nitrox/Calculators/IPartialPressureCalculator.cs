using UnitsNet;

namespace Domain.Nitrox.Calculators;

/// <summary>
///     This class is responsible for calculating the partial pressure of a gas component within a gas mixture at a given
///     ambient pressure.
/// </summary>
public interface IPartialPressureCalculator
{
    /// <summary>
    ///     Calculates the partial pressure of a gas component based on the total ambient pressure and the fraction of the gas
    ///     component in the mixture.
    /// </summary>
    /// <param name="ambientPressure">The total ambient pressure of the gas mixture.</param>
    /// <param name="fraction">
    ///     The fraction of the specific gas component in the gas mixture, expressed as a value between 0
    ///     and 1.
    /// </param>
    /// <returns>The partial pressure of the gas component as a Pressure object.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the fraction is not between 0 and 1.</exception>
    public Pressure CalculatePartialPressure(Pressure ambientPressure, double fraction);
}