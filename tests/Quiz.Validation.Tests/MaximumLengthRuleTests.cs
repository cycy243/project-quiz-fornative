using FluentAssertions;

namespace Quiz.Validations.Tests
{
    [TestClass]
    public class MaximumLengthRuleTests
    {
        [DataTestMethod]
        [DataRow("dddd", true, "If has less than 5 character then pass")]
        [DataRow("ddddd", true, "If has exactly 5 character then pass")]
        [DataRow("ddddsgfsdg", false, "If has more than 5 character then fails")]
        public void HasAtMaximum5Characters(string value, bool pass, string message)
        {
            // Arrange
            MaximumLengthRule rule = new MaximumLengthRule(5);

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
