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
    public class ContainsLowerLetterRuleTests
    {
        public Validatable<string> StringValidatable { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            StringValidatable = new Validatable<string>();
            StringValidatable.Validations.Add(new ContainsLowerLetterRule(5));
        }

        [TestMethod]
        public void WhenContains0LowerLetterAndNeedAtLeast5ThenReturnFalse()
        {
            // Arrange
            var validatable = new Validatable<string>() { Value = "DDD" };
            validatable.Validations.Add(new ContainsLowerLetterRule(5));

            // Act
            var result = validatable.Validate();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenContains4LowerLetterAndNeedAtLeast5ThenReturnFalse()
        {
            // Arrange
            var validatable = new Validatable<string>() { Value = "ddse" };
            validatable.Validations.Add(new ContainsLowerLetterRule(5));

            // Act
            var result = validatable.Validate();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenContains5LowerLetterAndNeedAtLeast5ThenReturnTrue()
        {
            // Arrange
            var validatable = new Validatable<string>() { Value = "ddseg" };
            validatable.Validations.Add(new ContainsLowerLetterRule(5));

            // Act
            var result = validatable.Validate();

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenContains7LowerLetterAndNeedAtLeast5ThenReturnTrue()
        {
            // Arrange
            var validatable = new Validatable<string>() { Value = "dddfseg" };
            validatable.Validations.Add(new ContainsLowerLetterRule(5));

            // Act
            var result = validatable.Validate();

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenContains5LowerLetterAnd3UpperLettersAndNeedAtLeast5ThenReturnTrue()
        {
            // Arrange
            var validatable = new Validatable<string>() { Value = "ddAdDFeg" };
            validatable.Validations.Add(new ContainsLowerLetterRule(5));

            // Act
            var result = validatable.Validate();

            // Assert
            result.Should().BeTrue();
        }
    }
}
