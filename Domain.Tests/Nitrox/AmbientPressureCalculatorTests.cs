using Domain.Nitrox.Services;
using UnitsNet;

namespace Domain.Tests.Nitrox;

[TestClass]
public class AmbientPressureCalculatorTests
{
    private AmbientPressureCalculator _calculator = null!;

    [TestInitialize]
    public void SetUp()
    {
        _calculator = new AmbientPressureCalculator();
    }

    [TestMethod]
    public void CalculateAmbientPressure_ZeroDepth_ReturnsAtmosphericPressure()
    {
        // Arrange
        var depth = Length.FromMeters(0);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1000); // Approx. density of freshwater

        // Act
        var result = _calculator.CalculateAmbientPressure(depth, waterDensity);

        // Assert
        Assert.AreEqual(Pressure.FromAtmospheres(1), result,
            "Ambient pressure at zero depth should equal atmospheric pressure.");
    }

    [TestMethod]
    public void CalculateAmbientPressure_NegativeDepth_ThrowsArgumentException()
    {
        // Arrange
        var depth = Length.FromMeters(-1);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1000);

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _calculator.CalculateAmbientPressure(depth, waterDensity),
            "Method should throw ArgumentException for negative depth values.");
    }

    [TestMethod]
    public void CalculateAmbientPressure_NegativeDensity_ThrowsArgumentException()
    {
        // Arrange
        var depth = Length.FromMeters(10);
        var waterDensity = Density.FromKilogramsPerCubicMeter(-1000);

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _calculator.CalculateAmbientPressure(depth, waterDensity),
            "Method should throw ArgumentException for negative density values.");
    }
}