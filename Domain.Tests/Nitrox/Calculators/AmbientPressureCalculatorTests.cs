using Domain.Nitrox.Calculators;
using Domain.Tests.Helpers;
using UnitsNet;

namespace Domain.Tests.Nitrox.Calculators;

[TestClass]
public class AmbientPressureCalculatorTests
{
    private IAmbientPressureCalculator _calculator = null!;

    [TestInitialize]
    public void SetUp()
    {
        _calculator = new AmbientPressureCalculator();
    }

    [TestMethod]
    public void CalculateAmbientPressure_ZeroDepth_ReturnsAtmosphericPressure()
    {
        // Arrange
        var depth = Depth.FromMeters(0);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1000);

        // Act
        var result = _calculator.CalculateAmbientPressure(depth, waterDensity);

        // Assert
        Assert.AreEqual(Pressure.FromAtmospheres(1), result,
            "Ambient pressure at zero depth should equal atmospheric pressure.");
    }

    [TestMethod]
    public void CalculateAmbientPressure_ReturnsAtmosphericPressure()
    {
        // Arrange
        var depth = Depth.FromMeters(10);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1000);

        // Act
        var result = _calculator.CalculateAmbientPressure(depth, waterDensity);

        // Assert
        Assert.AreEqual(1.96, result.Atmospheres, 0.1,
            "Ambient pressure at a depth of ten meters should equal two atmospheric pressure.");
    }

    [TestMethod]
    [ExpectedExceptionWithMessage(typeof(ArgumentException), "Depth must be non-negative. (Parameter 'depth')")]
    public void CalculateAmbientPressure_NegativeDepth_ThrowsArgumentException()
    {
        // Arrange
        var depth = Depth.FromMeters(-1);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1030);

        // Act
        _calculator.CalculateAmbientPressure(depth, waterDensity);
    }

    [TestMethod]
    [ExpectedExceptionWithMessage(typeof(ArgumentException),
        "Water density must be larger then zero. (Parameter 'waterDensity')")]
    public void CalculateAmbientPressure_ZeroDensity_ThrowsArgumentException()
    {
        // Arrange
        var depth = Depth.FromMeters(10);
        var waterDensity = Density.FromKilogramsPerCubicMeter(0);

        // Act
        _calculator.CalculateAmbientPressure(depth, waterDensity);
    }

    [TestMethod]
    [ExpectedExceptionWithMessage(typeof(ArgumentException),
        "Water density must be larger then zero. (Parameter 'waterDensity')")]
    public void CalculateAmbientPressure_NegativeDensity_ThrowsArgumentException()
    {
        // Arrange
        var depth = Depth.FromMeters(10);
        var waterDensity = Density.FromKilogramsPerCubicMeter(-1000);

        // Act
        _calculator.CalculateAmbientPressure(depth, waterDensity);
    }
}