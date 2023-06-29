namespace Useful.Extensions.Tests;

public class EnumExtensionsTests
{
    public enum TestEnumDescription
    {
        [Description("none")]
        None = 0,
        [Description("one")]
        Item1 = 1,
        [Description("two")]
        Item2 = 2
    }

    public enum TestEnumNoDescription
    {
        None = 0,
        Item1 = 1,
        Item2 = 2
    }


    [Theory]
    [InlineData(TestEnumDescription.None, "none")]
    [InlineData(TestEnumDescription.Item1, "one")]
    [InlineData(TestEnumDescription.Item2, "two")]
    public void test_enum_with_description_is_returned(TestEnumDescription item, string expected)
    {
        // Arrange
        // Act
        var result = item.GetDescription();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(TestEnumNoDescription.None)]
    [InlineData(TestEnumNoDescription.Item1)]
    [InlineData(TestEnumNoDescription.Item2)]
    public void test_enum_with_no_description_returns_empty_string(TestEnumNoDescription item)
    {
        // Arrange
        // Act
        var result = item.GetDescription();

        // Assert
        result.Should().BeEmpty();
    }
}

