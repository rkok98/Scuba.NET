using Domain.Nitrox.Calculators;
using UnitsNet;

namespace Domain.Tests.Nitrox.Calculators;

[TestClass]
public class PartialPressureCalculatorTests
{
    private IPartialPressureCalculator _calculator = null!;

    [TestInitialize]
    public void Setup()
    {
        _calculator = new PartialPressureCalculator();
    }

    [TestMethod]
    public void CalculatePartialPressure_WithValidInputs_ReturnsCorrectResult()
    {
        // Arrange
        var ambientPressure = Pressure.FromBars(1); // 1 bar
        var fraction = 0.21; // Air fraction for Oxygen usually
        var expected = Pressure.FromBars(0.21);

        // Act
        var result = _calculator.CalculatePartialPressure(ambientPressure, fraction);

        // Assert
        Assert.AreEqual(expected, result, "Calculated partial pressure does not match expected value.");
    }

    [TestMethod]
    [DataRow(-0.1)]
    [DataRow(0)]
    [DataRow(1.1)]
    public void CalculatePartialPressure_WithInvalidFraction_ThrowsArgumentOutOfRangeException(double fraction)
    {
        // Arrange
        var ambientPressure = Pressure.FromBars(1); // 1 bar

        // Act & Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => _calculator.CalculatePartialPressure(ambientPressure, fraction),
            "Method did not throw expected exception for fraction value: " + fraction);
    }

    [TestMethod]
    public void CalculatePartialPressure_WithFractionOfOne_ReturnsAmbientPressure()
    {
        // Arrange
        var ambientPressure = Pressure.FromBars(1); // 1 bar
        double fraction = 1;
        var expected = Pressure.FromBars(1);

        // Act
        var result = _calculator.CalculatePartialPressure(ambientPressure, fraction);

        // Assert
        Assert.AreEqual(expected, result,
            "Calculated partial pressure should equal ambient pressure when fraction is 1.");
    }
}