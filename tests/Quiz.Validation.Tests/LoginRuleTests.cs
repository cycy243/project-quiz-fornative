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
    public class LoginRuleTests
    {
        public Validatable<string> PseudoValidatable { get; set; }
        public LoginRule Rule;

        [TestInitialize]
        public void SetUp()
        {
            Rule = new LoginRule();
        }

        [TestMethod]
        public void WhenValueIsNotLikeAMailOrAPseudoThenCheckReturnsFalse()
        {
            // Arrange
            string value = "kjg";

            // Act
            var result = Rule.Check(value);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenValueIsNotLikeAMailButLikeAPseudoThenCheckReturnsTrue()
        {
            // Arrange
            string value = "cycy243";

            // Act
            var result = Rule.Check(value);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenValueIsNotLikeAPseudoButLikeAMailThenCheckReturnsTrue()
        {
            // Arrange
            string value = "cycy243@mail.com";

            // Act
            var result = Rule.Check(value);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenValueIsNullThenCheckReturnsFalse()
        {
            // Arrange
            string? value = null;

            // Act
            var result = Rule.Check(value);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenValueIsEmptyThenCheckReturnsFalse()
        {
            // Arrange
            string value = "";

            // Act
            var result = Rule.Check(value);

            // Assert
            result.Should().BeFalse();
        }
    }
}
