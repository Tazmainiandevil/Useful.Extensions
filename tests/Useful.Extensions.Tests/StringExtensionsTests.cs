using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Useful.Extensions.Tests.TestClasses;
using Xunit;

namespace Useful.Extensions.Tests
{
    /// <summary>
    /// string extension tests
    /// </summary>
    public class StringExtensionTests
    {
        #region Contains Value Tests

        [Theory]
        [InlineData("RGS", "rGs")]
        [InlineData("XYZ", "x")]
        [InlineData("This test string is quite a long one", "This teST strIng is QuitE a lOng onE")]
        public void test_string_contains_value_returns_true_ignoring_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.ContainsValue(comparisonString);

            // Assert
            valid.Should().BeTrue();
        }

        [Theory]
        [InlineData("RGS", "RGS")]
        [InlineData("XYZ", "X")]
        [InlineData("This test string is quite a long one", "long")]
        [InlineData("This test string is quite a long one", "This test string is quite a long one")]
        public void test_string_contains_value_returns_true_depending_on_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.ContainsValue(comparisonString, StringComparison.Ordinal);

            // Assert
            valid.Should().BeTrue();
        }

        [Theory]
        [InlineData("RGS", "F")]
        [InlineData("XYZ", "g")]
        [InlineData("This test string is quite a long one", "This tEst stRing IS quiTe a Long one2")]
        public void test_string_contains_value_contains_returns_false_ignoring_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.ContainsValue(comparisonString);

            // Assert
            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData("RGS", "rGs")]
        [InlineData("XYZ", "x")]
        [InlineData("This test string is quite a long one", "This teST strIng is QuitE a lOng onE")]
        public void test_string_contains_value_returns_false_depending_on_case(string testString, string comparisonString)
        {
            // Arrange
            // Act
            var valid = testString.ContainsValue(comparisonString, StringComparison.Ordinal);

            // Assert
            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_string_contains_value_returns_false_if_source_is_null_or_empty(string someText)
        {
            // Arrange
            // Act
            var valid = someText.ContainsValue("someText");

            // Assert
            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_string_contains_value_returns_false_if_find_is_null_or_empty(string findText)
        {
            // Arrange
            var someText = "someText";

            // Act
            var valid = someText.ContainsValue(findText);

            // Assert
            valid.Should().BeFalse();
        }

        #endregion Contains Value Tests

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
        public void test_string_has_value_returns_false_if_source_is_null_or_empty(string someText)
        {
            // Arrange
            // Act
            var valid = someText.HasValue("someText");

            // Assert
            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_string_has_value_returns_false_if_find_is_null_or_empty(string findText)
        {
            // Arrange
            var someText = "someText";

            // Act
            var valid = someText.HasValue(findText);

            // Assert
            valid.Should().BeFalse();
        }

        #endregion Has Value Tests

        #region Equals Ignore Case

        public static IEnumerable<object[]> StringCaseTestData
        {
            get
            {
                yield return new object[] { "someText", "SOMETEXT" };
                yield return new object[] { "someText", "someText" };
                yield return new object[] { "someText", "SometexT" };
                yield return new object[] { "someText", "sOmetexT" };
            }
        }

        [Theory]
        [MemberData(nameof(StringCaseTestData))]
        public void test_string_is_equal_regardless_of_case(string value, string compare)
        {
            // Arrange
            // Act
            // Assert
            value.EqualsIgnoreCase(compare).Should().BeTrue();
        }

        [Fact]
        public void test_string_is_null_then_equals_ignore_case_returns_false()
        {
            // Arrange
            // Act
            // Assert
            (null as string).EqualsIgnoreCase(null).Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void test_string_is_empty_and_compare_is_empty_then_equals_ignore_case_returns_true(string value)
        {
            // Arrange
            var compare = value;

            // Act
            // Assert
            value.EqualsIgnoreCase(compare).Should().BeTrue();
        }

        [Fact]
        public void test_string_is_null_and_compare_is_not_null_or_empty_then_equals_ignore_case_returns_false()
        {
            // Arrange
            var compare = "SomeString";

            // Assert
            (null as string).EqualsIgnoreCase(compare).Should().BeFalse();
        }

        #endregion Equals Ignore Case

        #region Safe StartsWith

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_safe_starts_with_when_value_is_null_or_empty_returns_false(string value)
        {
            // Arrange
            // Act
            var result = value.SafeStartsWith("Hello");

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_safe_starts_with_when_case_type_is_specified_and_value_is_null_or_empty_returns_false(string value)
        {
            // Arrange
            // Act
            var result = value.SafeStartsWith("Hello", StringComparison.Ordinal);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_safe_starts_with_when_culture_and_ignore_case_is_specified_and_value_is_null_or_empty_returns_false(string value)
        {
            // Arrange
            // Act
            var result = value.SafeStartsWith("Hello", true, CultureInfo.CurrentCulture);

            // Assert
            result.Should().BeFalse();
        }

        #endregion Safe StartsWith

        #region Safe EndsWith

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_safe_ends_with_when_value_is_null_or_empty_returns_false(string value)
        {
            // Arrange
            // Act
            var result = value.SafeEndsWith("Hello");

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_safe_ends_with_when_case_type_is_specified_and_value_is_null_or_empty_returns_false(string value)
        {
            // Arrange
            // Act
            var result = value.SafeEndsWith("Hello", StringComparison.Ordinal);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_safe_ends_with_when_culture_and_ignore_case_is_specified_and_value_is_null_or_empty_returns_false(string value)
        {
            // Arrange
            // Act
            var result = value.SafeEndsWith("Hello", true, CultureInfo.CurrentCulture);

            // Assert
            result.Should().BeFalse();
        }

        #endregion Safe EndsWith

        #region SubstringOrEmpty

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void test_try_substring_returns_empty_string_when_null_empty(string value)
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

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_substring_after_value_when_character_to_find_with_null_or_empty_value_then_returns_an_empty_string(string value)
        {
            // Arrange
            // Act
            var result = value.SubstringAfterValue('@');

            // Assert
            result.Should().Be(string.Empty);
        }

        [Fact]
        public void test_substring_after_value_when_character_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string value to find From you";

            // Act
            var result = text.SubstringAfterValue('F', StringComparison.Ordinal);

            // Assert
            result.Should().Be("rom you");
        }

        [Fact]
        public void test_substring_after_value_when_string_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string value to find From you";

            // Act
            var result = text.SubstringAfterValue("From", StringComparison.Ordinal);

            // Assert
            result.Should().Be(" you");
        }

        #endregion SubstringAfterValue

        #region SubstringAfterLastValue

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_substring_after_last_value_with_null_or_empty_returns_empty_string(string value)
        {
            // Arrange
            // Act
            var result = value.SubstringAfterLastValue("something");

            // Assert
            result.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("an", " email someone@here.com")]
        [InlineData("com", "")]
        [InlineData("", "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData(null, "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData("another", "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData("r", "e.com")]
        [InlineData("s", "omeone@here.com")]
        [InlineData("@", "here.com")]
        public void test_substring_after_last_value_with_string_find_returns_the_expected_string(string find, string expected)
        {
            // Arrange
            var text = "There is some text that includes an '@' and an email someone@here.com";

            // Act
            var result = text.SubstringAfterLastValue(find);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData('u', "des an '@' and an email someone@here.com")]
        [InlineData('U', "des an '@' and an email someone@here.com")]
        [InlineData('z', "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData('Z', "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData('l', " someone@here.com")]
        [InlineData('L', " someone@here.com")]
        [InlineData('s', "omeone@here.com")]
        [InlineData('S', "omeone@here.com")]
        [InlineData('n', "e@here.com")]
        [InlineData('N', "e@here.com")]
        public void test_substring_after_last_value_with_character_find_returns_the_expected_string(char find, string expected)
        {
            // Arrange
            var text = "There is some text that includes an '@' and an email someone@here.com";

            // Act
            var result = text.SubstringAfterLastValue(find);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void test_substring_after_last_value_when_character_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string value to Find From you";

            // Act
            var result = text.SubstringAfterLastValue('F', StringComparison.Ordinal);

            // Assert
            result.Should().Be("rom you");
        }

        [Fact]
        public void test_substring_after_last_value_when_string_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string From value to find From you";

            // Act
            var result = text.SubstringAfterLastValue("From", StringComparison.Ordinal);

            // Assert
            result.Should().Be(" you");
        }

        #endregion SubstringAfterLastValue

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

        [Fact]
        public void test_substring_before_value_when_character_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string value to Find From you";

            // Act
            var result = text.SubstringBeforeValue('F', StringComparison.Ordinal);

            // Assert
            result.Should().Be("some string value to ");
        }

        [Fact]
        public void test_substring_before_value_when_string_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string From value to find From you";

            // Act
            var result = text.SubstringBeforeValue("From", StringComparison.Ordinal);

            // Assert
            result.Should().Be("some string ");
        }

        #endregion SubstringBeforeValue

        #region SubstringBeforeLastValue

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void test_substring_before_last_value_with_null_or_empty_returns_empty_string(string value)
        {
            // Arrange
            // Act
            var result = value.SubstringBeforeLastValue("something");

            // Assert
            result.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("", "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData(null, "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData("another", "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData("@", "There is some text that includes an '@' and an email someone")]
        [InlineData("includes", "There is some text that ")]
        [InlineData("n", "There is some text that includes an '@' and an email someo")]
        [InlineData("u", "There is some text that incl")]
        public void test_substring_before_last_value_with_string_find_returns_the_expected_string(string find, string expected)
        {
            // Arrange
            var text = "There is some text that includes an '@' and an email someone@here.com";

            // Act
            var result = text.SubstringBeforeLastValue(find);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData('x', "There is some te")]
        [InlineData('X', "There is some te")]
        [InlineData('z', "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData('Z', "There is some text that includes an '@' and an email someone@here.com")]
        [InlineData('l', "There is some text that includes an '@' and an emai")]
        [InlineData('L', "There is some text that includes an '@' and an emai")]
        [InlineData('@', "There is some text that includes an '@' and an email someone")]
        public void test_substring_before_last_value_with_character_find_returns_the_expected_string(char find, string expected)
        {
            // Arrange
            var text = "There is some text that includes an '@' and an email someone@here.com";

            // Act
            var result = text.SubstringBeforeLastValue(find);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void test_substring_before_last_value_when_character_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string value to Find From you";

            // Act
            var result = text.SubstringBeforeLastValue('F', StringComparison.Ordinal);

            // Assert
            result.Should().Be("some string value to Find ");
        }

        [Fact]
        public void test_substring_before_last_value_when_string_to_find_is_case_sensitive_then_the_correct_string_is_returned()
        {
            // Arrange
            var text = "some string From value to find From you";

            // Act
            var result = text.SubstringBeforeLastValue("From", StringComparison.Ordinal);

            // Assert
            result.Should().Be("some string From value to find ");
        }

        #endregion SubstringBeforeLastValue

        #region Is base 64 string

        public static IEnumerable<object[]> InvalidBase64TestData
        {
            get
            {
                yield return new object[] { null };
                yield return new object[] { "" };
                yield return new object[] { "    " };
                yield return new object[] { "some string" };
            }
        }

        [Theory]
        [MemberData(nameof(InvalidBase64TestData))]
        public void test_is_base_64_returns_false_for_invalid_entries(string value)
        {
            // Arrange
            // Act
            var result = value.IsBase64();

            // Assert
            result.Should().BeFalse();
        }

        public static IEnumerable<object[]> ValidBase64TestData
        {
            get
            {
                yield return new object[] { Convert.ToBase64String(Encoding.UTF8.GetBytes("some value"), Base64FormattingOptions.None) };
                yield return new object[] { "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" };
            }
        }

        [Theory]
        [MemberData(nameof(ValidBase64TestData))]
        public void test_is_base_64_with_a_valid_base64_string_returns_true(string value)
        {
            // Arrange
            // Act
            var result = value.IsBase64();

            // Assert
            result.Should().BeTrue();
        }

        #endregion Is base 64 string

        #region IsAllNumber

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void given_is_numeric_when_null_or_empty_then_returns_false(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllNumber();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("123a")]
        [InlineData("A123")]
        [InlineData("12a3")]
        [InlineData("%^&£;///123")]
        public void given_is_numeric_when_value_contains_any_non_numeric_character_then_returns_false(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllNumber();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("12356")]
        [InlineData("1")]
        [InlineData("123456789012345789111111")]
        [InlineData("394732090123028340374932879123789132749374392471923729371")]
        public void given_is_numeric_when_string_contains_all_digits_then_returns_true(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllNumber();

            // Assert
            result.Should().BeTrue();
        }

        #endregion IsAllNumber

        #region IsAllAlpha

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void given_is_alpha_when_null_or_empty_then_returns_false(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllAlpha();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("1ldjfls")]
        [InlineData("A123dfdsf")]
        [InlineData("12a3")]
        [InlineData("%^&£;///Akjdsaljajiw")]
        public void given_is_alpha_when_value_contains_any_non_alpha_character_then_returns_false(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllAlpha();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("abcdef")]
        [InlineData("ABCDEF")]
        [InlineData("a")]
        [InlineData("A")]
        [InlineData("dslfjsdljdsljfdlfjldsfjlhdiahdoidosjdlajodslakdjasdad")]
        [InlineData("akjsdkhdfkhkdjsfksdjhfkewieosdisdksdhkjsdhfksiasddhdjfhcnpasplaspldjskdhahdfa")]
        [InlineData("KASJFHKSFHKNDDNKSANDCLJLSAKDNSDKDNASKJNZSDNK")]
        [InlineData("lsdfhklLDFJGLFDJGLlfgjdlLJDFGLJDlfdgljdLFDJDGLjglfdgjl")]
        public void given_is_alpha_when_string_contains_all_alpha_then_returns_true(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllAlpha();

            // Assert
            result.Should().BeTrue();
        }

        #endregion IsAllAlpha

        #region IsAllAlphaOrNumbers

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void given_is_alpha_or_number_when_null_or_empty_then_returns_false(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllAlphaOrNumbers();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("1$")]
        [InlineData("A£")]
        [InlineData("a^")]
        [InlineData("%^&£;///Akjdsaljajiw123")]
        public void given_is_alpha_or_number_when_value_contains_any_non_alpha_character_then_returns_false(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllAlphaOrNumbers();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("abcdef")]
        [InlineData("ABCDEF")]
        [InlineData("ABCDEF72728")]
        [InlineData("a")]
        [InlineData("A1")]
        [InlineData("dslfjsdljdsljfdlfjldsfjlhdiahdoidosjdlajodslakdjasdad")]
        [InlineData("akjsdkhdfkhkdjsfksdjhfkewieosdisdksdhkjsdhfksiasddhdjfhcnpasplaspldjskdhahdfa")]
        [InlineData("KASJFHKSFHKNDDNKSANDCLJLSAKDNSDKDNASKJNZSDNK")]
        [InlineData("lsdfhklLDFJGLFDJGLlfgjdlLJDFGLJDlfdgljdLFDJDGLjglfdgjl")]
        public void given_is_alpha_or_number_when_string_contains_all_alpha_and_or_numeric_then_returns_true(string input)
        {
            // Arrange
            // Act
            var result = input.IsAllAlphaOrNumbers();

            // Assert
            result.Should().BeTrue();
        }

        #endregion IsAllAlphaOrNumbers

        #region String.Join

        [Fact]
        public void test_join_with_char_separator_and_single_item_in_collection_returns_string()
        {
            // Arrange.
            const string expected = "Banana";
            string[] input = { expected };

            // Act.
            var actual = input.Join(',');

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_char_separator_and_multiple_items_in_collection_returns_string()
        {
            // Arrange.
            const string expected = "Banana,apple,orange";
            string[] input = { "Banana", "apple", "orange" };

            // Act.
            var actual = input.Join(',');

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_string_separator_and_single_item_in_collection_returns_string()
        {
            // Arrange.
            const string expected = "Banana";
            string[] input = { expected };

            // Act.
            var actual = input.Join(", ");

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_string_separator_and_multiple_items_in_collection_returns_string()
        {
            // Arrange.
            const string expected = "Banana, apple, orange";
            string[] input = { "Banana", "apple", "orange" };

            // Act.
            var actual = input.Join(", ");

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_char_separator_and_single_item_in_list_returns_string()
        {
            // Arrange.
            const string expected = "Banana";
            List<string> input = new() { expected };

            // Act.
            var actual = input.Join(',');

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_char_separator_and_multiple_items_in_list_returns_string()
        {
            // Arrange.
            const string expected = "Banana,apple,orange,pineapple,grape,plum";
            List<string> input = new() { "Banana", "apple", "orange", "pineapple", "grape", "plum" };

            // Act.
            var actual = input.Join(',');

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_string_separator_and_single_item_in_list_returns_string()
        {
            // Arrange.
            const string expected = "Banana";
            List<string> input = new() { expected };

            // Act.
            var actual = input.Join(", ");

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_string_separator_and_multiple_items_in_list_returns_string()
        {
            // Arrange.
            const string expected = "Banana, apple, orange, pineapple, grape, plum";
            List<string> input = new() { "Banana", "apple", "orange", "pineapple", "grape", "plum" };

            // Act.
            var actual = input.Join(", ");

            // Assert.
            actual.Should().Be(expected);
        }
        

        [Fact]
        public void test_join_with_char_separator_and_single_class_in_list_returns_string()
        {
            // Arrange.
            const string expected = "1: Pooh - 55";
            List<SafeGetElementTestClass> input = new()
                { new SafeGetElementTestClass { Identity = 1, Name = "Pooh", Age = 55 } };

            // Act.
            var actual = input.Join(',');

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_char_separator_and_multiple_classes_in_list_returns_string()
        {
            // Arrange.
            const string expected = "1: Pooh - 55,2: Piglet - 40,3: Tigger - 45,4: Eeyore - 65";
            List<SafeGetElementTestClass> input = new()
            {
                new SafeGetElementTestClass { Identity = 1, Name = "Pooh", Age = 55 },
                new SafeGetElementTestClass { Identity = 2, Name = "Piglet", Age = 40 },
                new SafeGetElementTestClass { Identity = 3, Name = "Tigger", Age = 45 },
                new SafeGetElementTestClass { Identity = 4, Name = "Eeyore", Age = 65 }
            };

            // Act.
            var actual = input.Join(',');

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_string_separator_and_single_class_in_list_returns_string()
        {
            // Arrange.
            const string expected = "1: Pooh - 55";
            List<SafeGetElementTestClass> input = new()
                { new SafeGetElementTestClass { Identity = 1, Name = "Pooh", Age = 55 } };

            // Act.
            var actual = input.Join(", ");

            // Assert.
            actual.Should().Be(expected);
        }

        [Fact]
        public void test_join_with_string_separator_and_multiple_classes_in_list_returns_string()
        {
            // Arrange.
            const string expected = "1: Pooh - 55, 2: Piglet - 40, 3: Tigger - 45, 4: Eeyore - 65";
            List<SafeGetElementTestClass> input = new()
            {
                new SafeGetElementTestClass { Identity = 1, Name = "Pooh", Age = 55 },
                new SafeGetElementTestClass { Identity = 2, Name = "Piglet", Age = 40 },
                new SafeGetElementTestClass { Identity = 3, Name = "Tigger", Age = 45 },
                new SafeGetElementTestClass { Identity = 4, Name = "Eeyore", Age = 65 }
            };

            // Act.
            var actual = input.Join(", ");

            // Assert.
            actual.Should().Be(expected);
        }

        #endregion String.Join
    }
}