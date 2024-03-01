using System.Diagnostics.CodeAnalysis;
using Domain.Nitrox.Calculators;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Nitrox;

[ExcludeFromCodeCoverage]
public static class NitroxServiceCollectionExtensions
{
    public static void AddNitroxComponents(this IServiceCollection services)
    {
        services.AddTransient<IAmbientPressureCalculator, AmbientPressureCalculator>();
        services.AddTransient<IBestNitroxForDepthCalculator, BestNitroxForDepthCalculator>();
        services.AddTransient<IMaximumOperatingDepthCalculator, MaximumOperatingDepthCalculator>();
        services.AddTransient<IPartialPressureCalculator, PartialPressureCalculator>();
    }
}