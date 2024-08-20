using FluentAssertions;

namespace Quiz.Validations.Tests
{
    [TestClass]
    public class MinimumLengthRuleTests
    {
        [DataTestMethod]
        [DataRow("SAO", false, "If has less than 8 character then fails")]
        [DataRow("uq holder", true, "If has exactly 8 character then pass")]
        [DataRow("one piece", true, "If has more than 8 character then pass")]
        public void HasAtLeast5Characters(string value, bool pass, string message)
        {
            // Arrange
            MinimumLengthRule rule = new MinimumLengthRule(8);

            // Act
            bool validationresult = rule.Check(value);

            // Assert
            pass.Should().Be(validationresult, message);
        }

        [TestMethod]
        public void WhenSet0HasMaximumLengthThenThrowArgumentException()
        {
            // Arrange
            // Act
            Action act = () => new MaximumLengthRule(-5);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
