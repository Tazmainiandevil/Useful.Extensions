#if !NETCOREAPP1_1
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FluentAssertions;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class BitmapExtensionTests
    {
        #region base64 to bitmap

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
        public void test_non_base64_string_base64_to_bitmap_throws_exception(string value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => value.Base64ToBitmap());
        }

        [Fact]
        public void test_base64_to_bitmap_with_a_valid_base64_string_returns_a_bitmap_object()
        {
            // Arrange
            var value = "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";

            // Act
            var result = value.Base64ToBitmap();

            // Assert
            result.Should().BeOfType<Bitmap>();
        }

        [Fact]
        public void test_base64_to_bitmap_with_a_valid_base64_string_but_not_a_bitmap_string_throws_an_exception()
        {
            // Arrange
            var value = Convert.ToBase64String(Encoding.UTF8.GetBytes("some value"), Base64FormattingOptions.None);

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => value.Base64ToBitmap());
        }

        #endregion base64 to bitmap
    }
}
#endif