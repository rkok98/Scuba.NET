using Domain.Nitrox.Models;
using UnitsNet;

namespace Domain.Nitrox.Calculators;

public class BestNitroxForDepthCalculator : IBestNitroxForDepthCalculator
{
    private readonly IAmbientPressureCalculator _ambientPressureCalculator;

    public BestNitroxForDepthCalculator(IAmbientPressureCalculator ambientPressureCalculator)
    {
        _ambientPressureCalculator = ambientPressureCalculator;
    }

    public NitroxGas CalculateBestNitroxForDepth(Depth depth, Pressure partialPressureOfOxygen, Density waterDensity)
    {
        if (depth < Depth.Zero)
            throw new ArgumentException(
                $"Depth must be non-negative, got {depth}.",
                nameof(depth));

        if (partialPressureOfOxygen < Pressure.Zero)
            throw new ArgumentException(
                $"Partial pressure of oxygen must be non-negative, got {partialPressureOfOxygen}.",
                nameof(partialPressureOfOxygen)
            );

        if (waterDensity < Density.Zero)
            throw new ArgumentException(
                $"Water density must be non-negative, got {waterDensity}.",
                nameof(waterDensity)
            );

        var ambientPressure = _ambientPressureCalculator.CalculateAmbientPressure(depth, waterDensity);
        var oxygenFraction = (decimal)partialPressureOfOxygen.Bars / (decimal)ambientPressure.Bars;

        return oxygenFraction > 1
            ? new NitroxGas(1)
            : new NitroxGas(oxygenFraction);
    }
}