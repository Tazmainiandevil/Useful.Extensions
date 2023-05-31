using FluentAssertions;
using System;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class EnumFlagExtensionsTests
    {
        [Flags]
        public enum TestEnumShort : short
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
            var testValue = TestEnumShort.None;

            // Act
            var result = testValue.Contains(TestEnumShort.Item1);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(TestEnumShort.Item1)]
        [InlineData(TestEnumShort.Item2)]
        [InlineData(TestEnumShort.Item3)]
        [InlineData(TestEnumShort.Item4)]
        [InlineData(TestEnumShort.Item5)]
        [InlineData(TestEnumShort.Item6)]
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
            var testValue = TestEnumShort.Item1 | TestEnumShort.Item5 | TestEnumShort.Item2;

            // Act
            var result = testValue.HasAnyOf(TestEnumShort.Item1, TestEnumShort.Item3, TestEnumShort.Item4);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void set_value_on_enum_when_none_of_the_items_is_set_then_returns_false()
        {
            // Arrange
            var testValue = TestEnumShort.Item6 | TestEnumShort.Item5 | TestEnumShort.Item2;

            // Act
            var result = testValue.HasAnyOf(TestEnumShort.Item1, TestEnumShort.Item3, TestEnumShort.Item4);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void set_value_on_enum_when_entries_is_null_then_returns_false()
        {
            // Arrange
            var testValue = TestEnumShort.Item6 | TestEnumShort.Item5 | TestEnumShort.Item2;

            // Act
            var result = testValue.HasAnyOf(null);

            // Assert
            result.Should().BeFalse();
        }

        #endregion Any

        #region All

        [Fact]
        public void set_value_on_enum_when_the_items_are_set_then_all_items_are_set_returns_true()
        {
            // Arrange
            var testValue = TestEnumShort.Item6 | TestEnumShort.Item5 | TestEnumShort.Item2;

            // Act
            var result = testValue.HasAllOf(TestEnumShort.Item6, TestEnumShort.Item2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void set_value_on_enum_when_the_items_are_set_except_one_then_all_items_are_set_returns_false()
        {
            // Arrange
            var testValue = TestEnumShort.Item6 | TestEnumShort.Item5 | TestEnumShort.Item2;

            // Act
            var result = testValue.HasAllOf(TestEnumShort.Item6, TestEnumShort.Item1);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void set_value_on_enum_all_when_entries_is_null_then_returns_false()
        {
            // Arrange
            var testValue = TestEnumShort.Item6 | TestEnumShort.Item5 | TestEnumShort.Item2;

            // Act
            var result = testValue.HasAllOf(null);

            // Assert
            result.Should().BeFalse();
        }

        #endregion All

        #region Set

        [Theory]
        [InlineData(TestEnumShort.Item2)]
        [InlineData(TestEnumShort.Item6)]
        public void set_values_for_enum_when_checked_return_true(TestEnumShort itemAdded)
        {
            // Arrange
            var value = TestEnumShort.None;

            // Act
            var result = value.Set(TestEnumShort.Item2, TestEnumShort.Item6);

            // Assert
            result.HasFlag(itemAdded).Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new TestEnumShort[] { })]
        public void given_set_when_entries_is_null_or_empty_then_returns_the_current_result(TestEnumShort[] entries)
        {
            // Arrange
            var value = TestEnumShort.None;

            // Act
            var result = value.Set(entries);

            // Assert
            result.Should().Be(0);
        }

        #endregion Set

        #region UnSet

        [Theory]
        [InlineData(TestEnumShort.Item2)]
        [InlineData(TestEnumShort.Item6)]
        public void set_values_for_enum_when_one_is_removed_and_checked_return_false(TestEnumShort itemRemoved)
        {
            // Arrange
            var value = TestEnumShort.None;
            value = value.Set(TestEnumShort.Item2, TestEnumShort.Item6);

            // Act
            var result = value.UnSet(itemRemoved);
            // Assert
            result.HasFlag(itemRemoved).Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new TestEnumShort[] { })]
        public void given_unset_when_entries_is_null_or_empty_then_returns_the_current_result(TestEnumShort[] entries)
        {
            // Arrange
            var value = TestEnumShort.Item1 | TestEnumShort.Item2;

            // Act
            var result = value.UnSet(entries);

            // Assert
            result.Should().Be((TestEnumShort)3);
        }

        #endregion UnSet
    }
}