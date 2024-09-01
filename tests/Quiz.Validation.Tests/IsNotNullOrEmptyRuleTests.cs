using FluentAssertions;

namespace Quiz.Validations.Tests
{
    [TestClass]
    public class IsNotNullOrEmptyRuleTests
    {
        [TestMethod]
        [DynamicData(nameof(CheckTestData))]
        public void CheckTest(object value, bool hasToPass, string message)
        {
            // Arrange
            IsNotNullOrEmptyRule<object> rule = new IsNotNullOrEmptyRule<object>();

            // Act
            var validationResult = rule.Check(value);

            // Assert
            hasToPass.Should().Be(validationResult, message);
        }

        public static IEnumerable<object[]> CheckTestData
        {
            get
            {
                return
                [
                    [null, false, "If value is null then fail"],
                    ["", false, "If value is empty then fail"],
                    ["valid_value", true, "If value contains something then pass"],
                    [53, true, "If value contains something then pass"],
                    [true, true, "If value contains something then pass"],
                    [true, true, "If value contains something then pass"]
                ];
            }
        }
    }
}