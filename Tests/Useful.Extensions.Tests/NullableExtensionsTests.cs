using FluentAssertions;
using System;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class NullableExtensionsTests
    {
        #region ToStringOrEmpty

        [Fact]
        public void test_to_string_or_empty_on_a_nullable_type_without_a_value_returns_an_empty_string()
        {
            // Arrange
            DateTime? now = null;

            // Act
            var response = now.ToStringOrEmpty();

            // Assert
            response.Should().Be(string.Empty);
        }

        [Fact]
        public void test_to_string_or_empty_on_a_nullable_type_with_a_value_returns_the_expected_value()
        {
            // Arrange
            var now = DateTime.UtcNow;
            DateTime? i = now;

            // Act
            var response = i.ToStringOrEmpty();

            // Assert
            response.Should().Be(now.ToString());
        }

        #endregion ToStringOrEmpty

        #region IsEqual

        [Theory]
        [InlineData(null, 1)]
        [InlineData(0, 1)]
        public void test_is_value_on_a_nullable_without_a_value_or_a_non_equal_value_returns_false(int? value, int compare)
        {
            // Arrange
            // Act
            var response = value.IsEqual(compare);

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public void test_is_value_on_a_nullable_with_a_value_returns_the_correct_result()
        {
            // Arrange
            int? value = 1;
            int compare = 1;

            // Act
            var response = value.IsEqual(compare);

            // Assert
            response.Should().BeTrue();
        }

        #endregion IsEqual

        #region IsNullOrDefault

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public void test_nullable_value_is_null_or_default(int? value)
        {
            // Arrange
            // Act
            var response = value.IsNullOrDefault();

            // Assert
            response.Should().BeTrue();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(-1)]
        public void test_nullable_value_is_not_null_or_default(int? value)
        {
            // Arrange
            // Act
            var response = value.IsNullOrDefault();

            // Assert
            response.Should().BeFalse();
        }

        #endregion IsNullOrDefault

        #region ValueOrDefault

        [Fact]
        public void test_value_or_default_on_a_nullable_type_with_null_value_returns_the_expected_default_value()
        {
            // Arrange
            int? value = null;

            // Act
            var response = value.ValueOrDefault();

            // Assert
            response.Should().Be(0);
        }


        [Fact]
        public void test_value_or_default_on_a_nullable_type_with_null_value_and_specific_default_value_returns_the_expected_default_value()
        {
            // Arrange
            int? value = null;

            // Act
            var response = value.ValueOrDefault(99);

            // Assert
            response.Should().Be(99);
        }

        [Fact]
        public void test_value_or_default_on_a_nullable_type_with_a_value_returns_the_expected_value()
        {
            // Arrange
            int? value = 66;

            // Act
            var response = value.ValueOrDefault();

            // Assert
            response.Should().Be(66);
        }
        #endregion ValueOrDefault
    }
}