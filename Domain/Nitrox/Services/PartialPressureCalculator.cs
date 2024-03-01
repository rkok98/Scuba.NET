using UnitsNet;
using UnitsNet.Units;

namespace Domain.Nitrox.Services;

public class PartialPressureCalculator : IPartialPressureCalculator
{
    public Pressure CalculatePartialPressure(Pressure ambientPressure, double fraction)
    {
        if (fraction is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(fraction), "Fraction must be between 0 and 1");

        return (ambientPressure * fraction).ToUnit(PressureUnit.Bar);
    }
}