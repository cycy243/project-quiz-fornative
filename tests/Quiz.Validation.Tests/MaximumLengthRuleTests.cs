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
    public class MaximumLengthRuleTests
    {
        public Validatable<string> StringValidatable { get; set; }
        public Validatable<int> IntValidatable { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            StringValidatable = new Validatable<string>();
            StringValidatable.Validations.Add(new MaximumLengthRule<string>(5));
            IntValidatable = new Validatable<int>();
            IntValidatable.Validations.Add(new MaximumLengthRule<int>(1));
        }

        [TestMethod("When Validable data is int and length is one, the data should only contains one number")]
        public void WhenDataTypeIsIntAndMaximumIsOneAndDataHaveOnlyOneNumberThenReturnTrue() {
            // Arrange
            IntValidatable.Value = 0;

            // Act
            var hasOneNumber = IntValidatable.Validate(); ;

            // Assert
            hasOneNumber.Should().BeFalse();
        }
    }
}
