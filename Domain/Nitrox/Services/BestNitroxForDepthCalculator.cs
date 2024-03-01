using Domain.Nitrox.Models;
using UnitsNet;

namespace Domain.Nitrox.Services;

using Depth = Length;

public class BestNitroxForDepthCalculator : IBestNitroxForDepthCalculator
{
    private readonly IAmbientPressureCalculator _ambientPressureCalculator;

    public BestNitroxForDepthCalculator(IAmbientPressureCalculator ambientPressureCalculator)
    {
        _ambientPressureCalculator = ambientPressureCalculator;
    }

    public NitroxGas CalculateBestNitroxForDepth(Depth depth, Pressure partialPressureOfOxygen, Density waterDensity)
    {
        var ambientPressure = _ambientPressureCalculator.CalculateAmbientPressure(depth, waterDensity);

        var oxygenFraction = decimal.Divide((decimal)partialPressureOfOxygen.Bars, (decimal)ambientPressure.Bars);

        return new NitroxGas(oxygenFraction);
    }
}