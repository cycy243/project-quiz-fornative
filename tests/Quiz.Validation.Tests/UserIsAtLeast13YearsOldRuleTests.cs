using Plugin.ValidationRules;
using FluentAssertions;

namespace Quiz.Validations.Tests
{
    [TestClass]
    public class UserIsAtLeast13YearsOldRuleTests
    {
        public Validatable<DateOnly> Validatable { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            Validatable = new Validatable<DateOnly>();
            Validatable.Validations.Add(new UserIsAtLeast13YearsOldRule { ValidationMessage = "User has to be at least 13 to register" });
        }

        [TestMethod]
        public void WhenDateIsNotAtLeast13YearsAgoThenReturnFalse()
        {
            // Arrange
            Validatable.Value = DateOnly.FromDateTime(DateTime.Now.AddYears(-10));

            // Act
            var is13YearsAgo = Validatable.Validate(); ;

            // Assert
            is13YearsAgo.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDateIsLeast13YearsAgoButMonthsIsGreaterAsNowThenReturnFalse()
        {
            // Arrange
            Validatable.Value = DateOnly.FromDateTime(DateTime.Now.AddYears(-13).AddMonths(1));

            // Act
            var is13YearsAgo = Validatable.Validate(); ;

            // Assert
            is13YearsAgo.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDateIsLeast13YearsAgoButDaysIsGreaterAsNowThenReturnFalse()
        {
            // Arrange
            Validatable.Value = DateOnly.FromDateTime(DateTime.Now.AddYears(-13).AddDays(1));

            // Act
            var is13YearsAgo = Validatable.Validate(); ;

            // Assert
            is13YearsAgo.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDateIsMoreThan13YearsAgoThenReturnTrue()
        {
            // Arrange
            Validatable.Value = DateOnly.FromDateTime(DateTime.Now.AddYears(-33));

            // Act
            var is13YearsAgo = Validatable.Validate(); ;

            // Assert
            is13YearsAgo.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDateIs13YearsAgoThenReturnTrue()
        {
            // Arrange
            Validatable.Value = DateOnly.FromDateTime(DateTime.Now.AddYears(-13));

            // Act
            var is13YearsAgo = Validatable.Validate(); ;

            // Assert
            is13YearsAgo.Should().BeTrue();
        }
    }
}