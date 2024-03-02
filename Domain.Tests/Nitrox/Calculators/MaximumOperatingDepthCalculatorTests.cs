using Domain.Nitrox.Calculators;
using UnitsNet;

namespace Domain.Tests.Nitrox.Calculators;

[TestClass]
public class MaximumOperatingDepthCalculatorTests
{
    private IMaximumOperatingDepthCalculator _calculator = null!;

    [TestInitialize]
    public void Setup()
    {
        _calculator = new MaximumOperatingDepthCalculator();
    }

    [TestMethod]
    public void CalculateMaximumOperatingDepth_ValidInputs_ReturnsCorrectDepth()
    {
        // Arrange
        const double fractionOfO2 = 0.32;
        var partialPressureO2 = Pressure.FromBars(1.4);

        // Act
        var result = _calculator.CalculateMaximumOperatingDepth(partialPressureO2, fractionOfO2);

        // Assert
        Assert.AreEqual(33, result.Meters, 0.001, "The calculated MOD should be correct.");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CalculateMaximumOperatingDepth_FractionOfO2LessThanZero_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var partialPressureO2 = Pressure.FromBars(1.4);
        var fractionOfO2 = -0.01; // Invalid value

        // Act
        _calculator.CalculateMaximumOperatingDepth(partialPressureO2, fractionOfO2);

        // Assert is handled by ExpectedExceptionWithMessage
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CalculateMaximumOperatingDepth_FractionOfO2MoreThanOne_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var partialPressureO2 = Pressure.FromBars(1.4);
        var fractionOfO2 = 1.01; // Invalid value

        // Act
        _calculator.CalculateMaximumOperatingDepth(partialPressureO2, fractionOfO2);

        // Assert is handled by ExpectedExceptionWithMessage
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CalculateMaximumOperatingDepth_ZeroFractionOfO2_ThrowsDivideByZeroOrSimilar()
    {
        // Arrange
        const int fractionOfO2 = 0;
        var partialPressureO2 = Pressure.FromBars(1.4);

        // Act & Assert
        _calculator.CalculateMaximumOperatingDepth(partialPressureO2, fractionOfO2);
    }
}