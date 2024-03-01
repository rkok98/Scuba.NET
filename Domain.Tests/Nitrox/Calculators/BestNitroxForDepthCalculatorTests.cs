using Domain.Nitrox.Calculators;
using Moq;
using UnitsNet;

namespace Domain.Tests.Nitrox.Calculators;

[TestClass]
public class BestNitroxForDepthCalculatorTests
{
    private Mock<IAmbientPressureCalculator> _ambientPressureCalculatorMock = null!;
    private IBestNitroxForDepthCalculator _calculator = null!;

    [TestInitialize]
    public void SetUp()
    {
        _ambientPressureCalculatorMock = new Mock<IAmbientPressureCalculator>();
        _calculator = new BestNitroxForDepthCalculator(_ambientPressureCalculatorMock.Object);
    }

    [TestMethod]
    public void CalculateBestNitroxForDepth_ValidInputs_ExpectedOutput()
    {
        // Arrange
        const decimal expectedOxygenFraction = 0.35m;

        var depth = Depth.FromMeters(30);
        var partialPressureOfOxygen = Pressure.FromBars(1.4);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1025);

        _ambientPressureCalculatorMock.Setup(x => x.CalculateAmbientPressure(depth, waterDensity))
            .Returns(Pressure.FromBars(4));

        // Act
        var result = _calculator.CalculateBestNitroxForDepth(depth, partialPressureOfOxygen, waterDensity);

        // Assert
        Assert.AreEqual(expectedOxygenFraction, result.OxygenFraction);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateBestNitroxForDepth_NegativeDepth_ThrowsArgumentException()
    {
        // Arrange
        var depth = Depth.FromMeters(-1); // Invalid depth
        var partialPressureOfOxygen = Pressure.FromBars(1.4);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1025);

        // Act
        var result = _calculator.CalculateBestNitroxForDepth(depth, partialPressureOfOxygen, waterDensity);

        // Assert is handled by the ExpectedException attribute
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateBestNitroxForDepth_NegativePartialPressureOfOxygen_ThrowsArgumentException()
    {
        // Arrange
        var depth = Depth.FromMeters(30);
        var partialPressureOfOxygen = Pressure.FromBars(-0.5); // Invalid partial pressure
        var waterDensity = Density.FromKilogramsPerCubicMeter(1025);

        // Act
        _calculator.CalculateBestNitroxForDepth(depth, partialPressureOfOxygen, waterDensity);

        // Assert is handled by the ExpectedException attribute
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateBestNitroxForDepth_NegativeWaterDensity_ThrowsArgumentException()
    {
        // Arrange
        var depth = Depth.FromMeters(30);
        var partialPressureOfOxygen = Pressure.FromBars(1.4);
        var waterDensity = Density.FromKilogramsPerCubicMeter(-100); // Invalid density

        // Act
        _calculator.CalculateBestNitroxForDepth(depth, partialPressureOfOxygen, waterDensity);

        // Assert is handled by the ExpectedException attribute
    }

    [TestMethod]
    public void CalculateBestNitroxForDepth_ZeroDepth_ExpectedOxygenFraction()
    {
        // Arrange
        const decimal expectedOxygenFraction = 1m;

        var depth = Depth.FromMeters(5);
        var partialPressureOfOxygen = Pressure.FromBars(1.4);
        var waterDensity = Density.FromKilogramsPerCubicMeter(1025);

        _ambientPressureCalculatorMock.Setup(x => x.CalculateAmbientPressure(depth, waterDensity))
            .Returns(Pressure.FromBars(1));

        // Act
        var result = _calculator.CalculateBestNitroxForDepth(depth, partialPressureOfOxygen, waterDensity);

        // Assert
        Assert.AreEqual(expectedOxygenFraction, result.OxygenFraction);
    }
}