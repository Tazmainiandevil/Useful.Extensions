using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void test_value_or_default_returns_the_expected_result_with_default_set()
        {
            // Arrange
            var values = new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } };

            // Act
            var result = values.ValueOrDefault("some text", 99);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public void test_value_or_default_returns_the_expected_result()
        {
            // Arrange
            var values = new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } };

            // Act
            var result = values.ValueOrDefault("some text");

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public void test_if_value_not_found_then_value_or_default_returns_the_default_value()
        {
            // Arrange
            var values = new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } };

            // Act
            var result = values.ValueOrDefault("something", 99);

            // Assert
            result.Should().Be(99);
        }

        [Fact]
        public void test_a_null_dictionary_for_value_or_default_returns_the_default_value()
        {
            // Arrange
            // Act
            var result = ((Dictionary<string, int>)null).ValueOrDefault("something", 99);

            // Assert
            result.Should().Be(99);
        }

        [Fact]
        public void test_try_get_value_or_default_returns_the_expected_result()
        {
            // Arrange
            var values = new Dictionary<int, int> { { 1, 100 }, { 2, 102 } };

            // Act
            var result = values.TryGetValueOrDefault(1, 99);

            // Assert
            result.Should().Be(100);
        }

        [Fact]
        public void test_if_value_not_found_then_try_get_value_or_default_returns_the_default_value()
        {
            // Arrange
            var values = new Dictionary<int, int> { { 1, 100 }, { 2, 102 } };

            // Act
            var result = values.TryGetValueOrDefault(9, 99);

            // Assert
            result.Should().Be(99);
        }

        [Fact]
        public void test_a_null_dictionary_for_try_get_value_or_default_returns_the_default_value()
        {
            // Arrange
            // Act
            var result = ((Dictionary<int, int>)null).TryGetValueOrDefault(9, 99);

            // Assert
            result.Should().Be(99);
        }
    }
}
