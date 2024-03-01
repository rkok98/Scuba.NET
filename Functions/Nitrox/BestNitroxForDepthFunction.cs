using System.Globalization;
using Domain.Nitrox;
using Domain.Nitrox.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace Functions;

public class BestNitroxForDepthFunction
{
    private readonly IBestNitroxForDepthCalculator _bestNitroxForDepthCalculator;

    public BestNitroxForDepthFunction(
        IBestNitroxForDepthCalculator bestNitroxForDepthCalculator
    )
    {
        _bestNitroxForDepthCalculator = bestNitroxForDepthCalculator;
    }

    [FunctionName("BestNitroxForDepthFunction")]
    public IActionResult RunAsync(
        ILogger log,
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req
    )
    {
        if (!req.Query.TryGetValue("depth", out var depthStr) ||
            !decimal.TryParse(
                depthStr,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var depthValue
            ))
            return new BadRequestObjectResult("Please pass a valid depth in meters as a query parameter.");

        if (!req.Query.TryGetValue("partialPressure", out var partialPressureStr) ||
            !decimal.TryParse(
                partialPressureStr,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var partialPressureValue
            ))
            return new BadRequestObjectResult(
                "Please pass a valid partial pressure of oxygen in bars as a query parameter."
            );

        var depth = Length.FromMeters(depthValue);
        var partialPressureOfOxygen = Pressure.FromBars(partialPressureValue);

        var best = _bestNitroxForDepthCalculator.CalculateBestNitroxForDepth(
            depth,
            partialPressureOfOxygen,
            NitroxConstants.FreshWaterDensity
        );

        return new OkObjectResult(
            new BestNitroxForDepthResult
            {
                Ean = best.Ean,
                NitrogenFraction = best.NitrogenFraction,
                OxygenFraction = best.OxygenFraction
            }
        );
    }

    private readonly record struct BestNitroxForDepthResult
    {
        public string Ean { get; init; }
        public decimal NitrogenFraction { get; init; }
        public decimal OxygenFraction { get; init; }
    }
}