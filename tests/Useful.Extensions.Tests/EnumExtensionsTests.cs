using FluentAssertions;
using System;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class EnumExtensionsTests
    {
        [Flags]
        public enum TestEnum : short
        {
            None = 0,
            Item1 = 1,
            Item2 = 2,
            Item3 = 4,
            Item4 = 8,
            Item5 = 16,
            Item6 = 32
        }

        #region Contains

        [Fact]
        public void set_default_value_on_enum_flag_then_value_is_not_set()
        {
            // Arrange
            var testValue = TestEnum.None;

            // Act
            var result = testValue.Contains(TestEnum.Item1);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(TestEnum.Item1)]
        [InlineData(TestEnum.Item2)]
        [InlineData(TestEnum.Item3)]
        [InlineData(TestEnum.Item4)]
        [InlineData(TestEnum.Item5)]
        [InlineData(TestEnum.Item6)]
        public void set_value_on_enum_flag_then_value_is_set(Enum value)
        {
            // Arrange
            var testValue = value;

            // Act
            var result = testValue.Contains(value);

            // Assert
            result.Should().BeTrue();
        }

        #endregion Contains

        #region Any

        [Fact]
        public void set_value_on_enum_when_an_one_of_the_items_is_set_then_returns_true()
        {
            // Arrange
            TestEnum testValue = TestEnum.Item1 | TestEnum.Item5 | TestEnum.Item2;

            // Act
            var result = testValue.Any(TestEnum.Item1, TestEnum.Item3, TestEnum.Item4);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void set_value_on_enum_when_none_of_the_items_is_set_then_returns_false()
        {
            // Arrange
            var testValue = TestEnum.Item6 | TestEnum.Item5 | TestEnum.Item2;

            // Act
            var result = testValue.Any(TestEnum.Item1, TestEnum.Item3, TestEnum.Item4);

            // Assert
            result.Should().BeFalse();
        }

        #endregion Any

        #region All

        [Fact]
        public void set_value_on_enum_when_the_items_are_set_then_all_items_are_set_returns_true()
        {
            // Arrange
            var testValue = TestEnum.Item6 | TestEnum.Item5 | TestEnum.Item2;

            // Act
            var result = testValue.All(TestEnum.Item6, TestEnum.Item2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void set_value_on_enum_when_the_items_are_set_except_one_then_all_items_are_set_returns_false()
        {
            // Arrange
            var testValue = TestEnum.Item6 | TestEnum.Item5 | TestEnum.Item2;

            // Act
            var result = testValue.All(TestEnum.Item6, TestEnum.Item1);

            // Assert
            result.Should().BeFalse();
        }

        #endregion All

        #region Set

        [Theory]
        [InlineData(TestEnum.Item2)]
        [InlineData(TestEnum.Item6)]
        public void set_values_for_enum_when_checked_return_true(TestEnum itemAdded)
        {
            // Arrange
            var value = TestEnum.None;

            // Act
            var result = value.Set<TestEnum>(TestEnum.Item2, TestEnum.Item6);

            // Assert
            result.HasFlag(itemAdded).Should().BeTrue();
        }

        #endregion Set

        #region UnSet

        [Theory]
        [InlineData(TestEnum.Item2)]
        [InlineData(TestEnum.Item6)]
        public void set_values_for_enum_when_one_is_removed_and_checked_return_false(TestEnum itemRemoved)
        {
            // Arrange
            var value = TestEnum.None;
            value = value.Set<TestEnum>(TestEnum.Item2, TestEnum.Item6);

            // Act
            var result = value.UnSet<TestEnum>(itemRemoved);
            // Assert
            result.HasFlag(itemRemoved).Should().BeFalse();
        }

        #endregion UnSet
    }
}