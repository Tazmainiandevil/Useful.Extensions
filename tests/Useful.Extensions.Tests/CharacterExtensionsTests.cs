namespace Useful.Extensions.Tests;

/// <summary>
/// character extension tests
/// </summary>
public class CharacterExtensionTests
{
    public static IEnumerable<object[]> CharacterInputTestData
    {
        get
        {
            yield return new object[] { 'R', 'R' };
            yield return new object[] { 'E', 'e' };
            yield return new object[] { 'f', 'F' };
            yield return new object[] { 'g', 'g' };
        }
    }

    [Theory]
    [MemberData(nameof(CharacterInputTestData))]
    public void test_two_characters_are_equal_to_each_other_ignoring_case(char value, char compare)
    {
        // Arrange
        // Act
        // Assert
        Assert.True(value.EqualTo(compare));
    }

    [Fact]
    public void test_two_characters_are_not_equal_to_each_other_and_the_same_case()
    {
        // Arrange
        var first = 'h';
        var compare = 'H';

        // Act
        // Assert
        Assert.False(first.EqualTo(compare, StringComparison.Ordinal));
    }

    [Fact]
    public void test_two_characters_are_equal_to_each_other_and_the_same_case()
    {
        // Arrange
        var first = 'h';
        var compare = 'h';

        // Act
        // Assert
        Assert.True(first.EqualTo(compare, StringComparison.Ordinal));
    }
}