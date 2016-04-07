using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Useful.Extensions.Tests
{
    /// <summary>
    /// string extension tests
    /// </summary>
    public class StringExtensionTests
    {
        #region Has Value Tests

        [Theory]
        [InlineData("RGS", "rGs")]
        [InlineData("XYZ", "x")]
        [InlineData("This test string is quite a long one", "This teST strIng is QuitE a lOng onE")]
        public void test_string_has_value_returns_true_ignoring_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.HasValue(comparisonString);

            // Assert
            valid.Should().BeTrue();
        }

        [Theory]
        [InlineData("RGS", "RGS")]
        [InlineData("XYZ", "X")]
        [InlineData("This test string is quite a long one", "long")]
        [InlineData("This test string is quite a long one", "This test string is quite a long one")]
        public void test_string_has_value_returns_true_depending_on_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.HasValue(comparisonString, StringComparison.Ordinal);

            // Assert
            valid.Should().BeTrue();
        }

        [Theory]
        [InlineData("RGS", "F")]
        [InlineData("XYZ", "g")]
        [InlineData("This test string is quite a long one", "This tEst stRing IS quiTe a Long one2")]
        public void test_string_has_value_contains_returns_false_ignoring_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.HasValue(comparisonString);

            // Assert
            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData("RGS", "rGs")]
        [InlineData("XYZ", "x")]
        [InlineData("This test string is quite a long one", "This teST strIng is QuitE a lOng onE")]
        public void test_string_has_value_returns_false_depending_on_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.HasValue(comparisonString, StringComparison.Ordinal);

            // Assert
            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_string_has_value_returns_false_if_source_is_null_or_empty(string sometext)
        {
            // Arrange
            // Act
            var valid = sometext.HasValue("sometext");

            // Assert
            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_string_has_value_returns_false_if_find_is_null_or_empty(string findtext)
        {
            // Arrange
            var sometext = "sometext";

            // Act
            var valid = sometext.HasValue(findtext);

            // Assert
            valid.Should().BeFalse();
        }

        #endregion Has Value Tests

        #region EqualTo

        public static IEnumerable<object[]> StringCaseTestData
        {
            get
            {
                yield return new object[] { "sometext", "SOMETEXT" };
                yield return new object[] { "sometext", "sometext" };
                yield return new object[] { "sometext", "SometexT" };
                yield return new object[] { "sometext", "sOmetexT" };
            }
        }

        [Theory]
        [MemberData("StringCaseTestData")]
        public void test_string_is_equal_regardless_of_case(string value, string compare)
        {
            // Arrange
            // Act
            // Assert
            value.EqualsIgnoreCase(compare).Should().BeTrue();
        }

        #endregion EqualTo

        #region SubstringOrEmpty

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void test_try_substring_retuns_empty_string_when_null_empty(string value)
        {
            // Arrange
            // Act
            // Assert
            value.SubstringOrEmpty(1, 10).Should().Be(string.Empty);
        }

        [Theory]
        [InlineData(6, 10)]
        [InlineData(-1, -5)]
        [InlineData(0, -1)]
        public void test_try_substring_returns_empty_string_when_index_and_length_are_out_of_bounds(int start, int length)
        {
            // Arrange
            var value = "Test";

            // Act
            // Assert
            value.SubstringOrEmpty(start, length).Should().Be(string.Empty);
        }

        [Theory]
        [InlineData(0, "Some text to create a test")]
        [InlineData(1, "ome text to create a test")]
        [InlineData(6, "ext to create a test")]
        [InlineData(10, "to create a test")]
        public void test_if_only_the_start_given_return_the_requested_remaining_part_of_the_value(int start, string expected)
        {
            // Arrange
            var value = "Some text to create a test";

            // Act
            // Assert
            value.SubstringOrEmpty(start).Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 1, "S")]
        [InlineData(6, 5, "ext t")]
        [InlineData(10, 9, "to create")]
        [InlineData(0, 26, "Some text to create a test")]
        public void test_if_the_start_and_length_is_given_return_the_requested_part_of_the_value(int start, int length, string expected)
        {
            // Arrange
            var value = "Some text to create a test";

            // Act
            // Assert
            value.SubstringOrEmpty(start, length).Should().Be(expected);
        }

        #endregion SubstringOrEmpty

        #region Safe Trim

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("  ", "")]
        [InlineData("Some text    ", "Some text")]
        [InlineData("   Some text    ", "Some text")]
        public void test_if_trim_correctly_trims_or_returns_if_null_or_empty(string value, string expected)
        {
            // Arrange
            // Act
            // Assert
            value.SafeTrim().Should().Be(expected);
        }

        #endregion Safe Trim

        #region SubstringAfterValue

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_substring_after_value_with_null_or_empty_returns_empty_string(string value)
        {
            // Arrange
            // Act
            var result = value.SubstringAfterValue("something");

            // Assert
            result.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("string", " value to find from")]
        [InlineData("from", "")]
        [InlineData("", "some string value to find from")]
        [InlineData(null, "some string value to find from")]
        [InlineData("another", "some string value to find from")]
        [InlineData("m", "e string value to find from")]
        [InlineData("s", "ome string value to find from")]
        public void test_substring_after_value_with_string_find_returns_the_expected_string(string find, string expected)
        {
            // Arrange
            var text = "some string value to find from";

            // Act
            var result = text.SubstringAfterValue(find);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData('v', "alue to find from you")]
        [InlineData('V', "alue to find from you")]
        [InlineData('z', "some string value to find from you")]
        [InlineData('Z', "some string value to find from you")]
        [InlineData('l', "ue to find from you")]
        [InlineData('L', "ue to find from you")]
        [InlineData('s', "ome string value to find from you")]
        [InlineData('S', "ome string value to find from you")]
        [InlineData('y', "ou")]
        [InlineData('Y', "ou")]
        public void test_substring_after_value_with_character_find_returns_the_expected_string(char find, string expected)
        {
            // Arrange
            var text = "some string value to find from you";

            // Act
            var result = text.SubstringAfterValue(find);

            // Assert
            result.Should().Be(expected);
        }

        #endregion SubstringAfterValue

        #region SubstringBeforeValue

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_substring_before_value_with_null_or_empty_returns_empty_string(string value)
        {
            // Arrange
            // Act
            var result = value.SubstringBeforeValue("something");

            // Assert
            result.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("string", "some ")]
        [InlineData("from", "some string value to find ")]
        [InlineData("", "some string value to find from")]
        [InlineData(null, "some string value to find from")]
        [InlineData("another", "some string value to find from")]
        [InlineData("m", "so")]
        [InlineData("s", "")]
        public void test_substring_before_value_with_string_find_returns_the_expected_string(string find, string expected)
        {
            // Arrange
            var text = "some string value to find from";

            // Act
            var result = text.SubstringBeforeValue(find);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData('v', "some string ")]
        [InlineData('V', "some string ")]
        [InlineData('z', "some string value to find from you")]
        [InlineData('Z', "some string value to find from you")]
        [InlineData('l', "some string va")]
        [InlineData('L', "some string va")]
        [InlineData('s', "")]
        [InlineData('S', "")]
        [InlineData('y', "some string value to find from ")]
        [InlineData('Y', "some string value to find from ")]
        public void test_substring_before_value_with_character_find_returns_the_expected_string(char find, string expected)
        {
            // Arrange
            var text = "some string value to find from you";

            // Act
            var result = text.SubstringBeforeValue(find);

            // Assert
            result.Should().Be(expected);
        }

        #endregion SubstringBeforeValue
    }
}