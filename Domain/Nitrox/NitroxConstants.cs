using System.Diagnostics.CodeAnalysis;
using Domain.Nitrox.Models;
using UnitsNet;

namespace Domain.Nitrox;

/// <summary>
///     Provides a set of constants relevant to scuba diving calculations, particularly for nitrox diving.
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class NitroxConstants
{
    // The atmospheric pressure at sea level in atmospheres.
    public static readonly Pressure SurfacePressure = Pressure.FromAtmospheres(1);

    // The acceleration due to gravity in meters per second squared.
    public static readonly Acceleration Gravity = Acceleration.FromMetersPerSecondSquared(9.81);

    // Constants representing the densities of different types of water.
    public static readonly Density FreshWaterDensity = Density.FromKilogramsPerCubicMeter(1000);
    public static readonly Density BrackishWaterDensity = Density.FromKilogramsPerCubicMeter(1020);
    public static readonly Density SaltWaterDensity = Density.FromKilogramsPerCubicMeter(1030);

    // Constants for common Nitrox gas mixes.
    public static readonly NitroxGas Air = new(0.21m, 0.79m);
    public static readonly NitroxGas Ean32 = new(0.32m, 0.68m);
    public static readonly NitroxGas Ean36 = new(0.36m, 0.64m);
}