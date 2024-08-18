using FluentAssertions;
using Plugin.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Validations.Tests
{
    [TestClass]
    public class ContainsUpperLetterRuleTests
    {
        [TestMethod]
        public void WhenContains0LowerLetterAndNeedAtLeast5ThenReturnFalse()
        {
            // Arrange
            Validatable<string> validatable = new Validatable<string>() { Value = "sdf" };
            validatable.Validations.Add(new ContainsUpperLetterRule(5));

            // Act
            bool result = validatable.Validate();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenContains4LowerLetterAndNeedAtLeast5ThenReturnFalse()
        {
            // Arrange
            Validatable<string> validatable = new Validatable<string>() { Value = "DDDD" };
            validatable.Validations.Add(new ContainsUpperLetterRule(5));

            // Act
            bool result = validatable.Validate();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenContains5LowerLetterAndNeedAtLeast5ThenReturnTrue()
        {
            // Arrange
            Validatable<string> validatable = new Validatable<string>() { Value = "D" };
            validatable.Validations.Add(new ContainsUpperLetterRule(1));

            // Act
            bool result = validatable.Validate();

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenContains7LowerLetterAndNeedAtLeast5ThenReturnTrue()
        {
            // Arrange
            Validatable<string> validatable = new Validatable<string>() { Value = "DFGFDEZ" };
            validatable.Validations.Add(new ContainsUpperLetterRule(5));

            // Act
            bool result = validatable.Validate();

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenContains5LowerLetterAnd3UpperLettersAndNeedAtLeast3ThenReturnTrue()
        {
            // Arrange
            Validatable<string> validatable = new Validatable<string>() { Value = "ddAdDFeg" };
            validatable.Validations.Add(new ContainsUpperLetterRule(3));

            // Act
            bool result = validatable.Validate();

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenContains1UpperLetterAndNeed0ThenReturnFalse()
        {
            // Arrange
            Validatable<string> validatable = new Validatable<string>() { Value = "ddAdDFeg" };
            validatable.Validations.Add(new ContainsUpperLetterRule(0));

            // Act
            bool result = validatable.Validate();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenContains0UpperLetterAndNeed0ThenReturnTrue()
        {
            // Arrange
            Validatable<string> validatable = new Validatable<string>() { Value = "ddeg" };
            validatable.Validations.Add(new ContainsUpperLetterRule(0));

            // Act
            bool result = validatable.Validate();

            // Assert
            result.Should().BeTrue();
        }
    }
}
