using FluentAssertions;

namespace Quiz.Validations.Tests
{
    [TestClass()]
    public class IsNotNullOrEmptyRuleTests
    {
        [DataTestMethod()]
        [DynamicData(nameof(CheckTestData))]
        public void CheckTest<T>(T? value, bool hasToPass, string message)
        {
            // Arrange
            IsNotNullOrEmptyRule<T> rule = new IsNotNullOrEmptyRule<T>();

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