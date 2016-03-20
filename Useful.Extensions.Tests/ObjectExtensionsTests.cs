using System;
using FluentAssertions;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
        #region Get Value From Anon Tests

        [Fact]
        public void test_get_value_from_anonymous_type_for_a_string_returns_the_expected_value()
        {
            // Arrange
            var anonObject = new { Name = "Bob" };

            // Act
            var result = ObjectExtensions.GetValueFromAnonymousType<string>(anonObject, "Name");

            // Assert            
            result.Should().Be("Bob");
        }

        [Fact]
        public void test_get_value_from_anonymous_type_for_an_int_value_returns_the_expected_value()
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act
            var result = ObjectExtensions.GetValueFromAnonymousType<int>(anonObject, "Number");

            // Assert
            result.Should().Be(3);            
        }

        #endregion Get Value From Anon Tests

        #region Object Clone Tests

        [Serializable]
        private class Details
        {
            public string Input { get; set; }
            public int Number { get; set; }
            public AdditionalDetails additional { get; set; }
        }

        [Serializable]
        private class AdditionalDetails
        {
            public DateTime DateOfBirth { get; set; }
        }

        [Fact]
        public void test_object_clone_throws_exception_if_not_a_serializable_object()
        {
            // Arrange
            var person = new { Input = "test1", Number = 57 };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => person.Clone());
        }

        [Fact]
        public void test_object_clone_returns_default_when_object_is_null()
        {
            // Arrange
            // Act
            var result = ((Details)null).Clone();

            // Assert            
            result.ShouldBeEquivalentTo(default(Details));            
        }

        [Fact]
        public void test_object_clone_returns_an_equivalent_object()
        {
            // Arrange
            var details = new Details { Input = "test1", Number = 57, additional = new AdditionalDetails {DateOfBirth = new DateTime(1973, 5, 1)} };

            // Act
            var result = details.Clone();

            // Assert
            result.ShouldBeEquivalentTo(details);            
        }


        [Fact]
        public void test_object_clone_does_not_return_the_same_reference_object()
        {
            // Arrange
            var details = new Details { Input = "test1", Number = 57, additional = new AdditionalDetails { DateOfBirth = new DateTime(1973, 5, 1) } };

            // Act
            var result = details.Clone();

            // Assert
            result.Should().NotBeSameAs(details);
        }
        #endregion Object Clone Tests
    }
}