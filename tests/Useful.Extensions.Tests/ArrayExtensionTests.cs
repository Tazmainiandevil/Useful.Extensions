namespace Useful.Extensions.Tests;

public class ArrayExtensionTests
{
    #region String Version

    [Theory]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 0, "hello")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 1, "goodbye")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 3, "!345ewr:)")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "" }, 3, "")]
    public void Test_safe_get_element_with_valid_parameter_of_string_and_trim_function_returns_expected(string[] stringArray, int arrayLocation, string expected)
    {
        // Arrange.
        // Act.
        var actual = stringArray.SafeGetElement(arrayLocation, string.Empty, (s) => s.Trim());

        // Assert.
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 0, "hello", "yay")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 1, "goodbye", "yay")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 3, "!345ewr:)", "yay")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "" }, 3, "", "yay")]
    public void Test_safe_get_element_with_valid_parameter_of_string_and_trim_function_and_default_element_returns_expected(string[] stringArray, int arrayLocation, string expected, string defaultElement)
    {
        // Arrange.
        // Act.
        var actual = stringArray.SafeGetElement(arrayLocation, defaultElement, (s) => s.Trim());

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_string_and_complicated_function_returns_expected()
    {
        // Arrange.
        var stringArray = new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" };
        const string expected = "3264 yep";

        // Act.
        var actual = stringArray.SafeGetElement(2, string.Empty, (s) => s.Replace("*Mob*", string.Empty).Trim());

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_string_and_long_function_returns_expected()
    {
        // Arrange.
        var stringArray = new[] { "  hello  ", "  goodbye  ", " Pooh , Hello ", "!345ewr:)" };
        const string expected = "Hello Pooh";

        // Act.
        var actual = stringArray.SafeGetElement(2, string.Empty, (s) =>
        {
            var results = new StringBuilder();
            var elements = s.Split(',');

            results.Append(elements[1].Trim());
            results.Append(" ");
            results.Append(elements[0].Trim());

            return results.ToString();
        });

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_string_and_calling_external_function_returns_expected()
    {
        // Arrange.
        var stringArray = new[] { "  hello  ", "  goodbye  ", " Pooh , Hello ", "!345ewr:)" };
        const string expected = "Hello Pooh";

        // Act.
        var actual = stringArray.SafeGetElement(2, string.Empty, ReverseStringElements);

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_string_and_null_function_returns_expected()
    {
        // Arrange.
        var stringArray = new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" };
        const string expected = "  hello  ";

        // Act.
        var actual = stringArray.SafeGetElement(0);

        // Assert.
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 4)]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 5)]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 100)]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, -1)]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, -100)]
    public void Test_safe_get_element_with_invalid_parameter_of_string_and_function_returns_empty_string(string[] stringArray, int arrayLocation)
    {
        // Arrange.
        // Act.
        var actual = stringArray.SafeGetElement(arrayLocation, string.Empty, (s) => s.Trim());

        // Assert.
        actual.Should().Be(string.Empty);
    }

    [Theory]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 4, "yay")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 5, "yay")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, 100, "yay")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, -1, "yay")]
    [InlineData(new[] { "  hello  ", "  goodbye  ", "*Mob* 3264 yep ", "!345ewr:)" }, -100, "yay")]
    public void Test_safe_get_element_with_invalid_parameter_of_string_and_function_returns_default_element(string[] stringArray, int arrayLocation, string defaultElement)
    {
        // Arrange.
        // Act.
        var actual = stringArray.SafeGetElement(arrayLocation, defaultElement, (s) => s.Trim());

        // Assert.
        actual.Should().Be(defaultElement);
    }

    #endregion String Version

    #region Int Version

    [Theory]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 0, 18)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 4, 10)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 5, 8)]
    public void Test_safe_get_element_with_valid_parameter_of_int_and_multiply_function_returns_expected(int[] intArray, int arrayLocation, int expected)
    {
        // Arrange.
        // Act.
        var actual = intArray.SafeGetElement(arrayLocation, 1, (s) => s * 2);

        // Assert.
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 0, 18, 2)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 4, 10, 2)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 7, 1, 1)]
    public void Test_safe_get_element_with_valid_parameter_of_int_and_multiply_function_and_default_element_returns_expected(int[] intArray, int arrayLocation, int expected, int defaultElement)
    {
        // Arrange.
        // Act.
        var actual = intArray.SafeGetElement(arrayLocation, defaultElement, (s) => s * 2);

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_int_and_complicated_function_returns_expected()
    {
        // Arrange.
        var intArray = new[] { 9, 8, 7, 6, 5, 4 };
        const int expected = 8;

        // Act.
        var actual = intArray.SafeGetElement(2, 1, (s) => s * 2 + 20 - 4 * 4 / 2 - 18);

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_int_and_long_function_returns_expected()
    {
        // Arrange.
        var intArray = new[] { 9, 8, 7, 6, 5, 4 };
        const int expected = 8;

        // Act.
        var actual = intArray.SafeGetElement(2, 1, (s) =>
        {
            var results = s;
            const int additionalCalculation = 4 * 4 / 2;
            results *= 2;
            results = results + 20 - additionalCalculation;
            results -= 18;

            return results;
        });

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_int_and_calling_external_function_returns_expected()
    {
        // Arrange.
        var intArray = new[] { 9, 8, 7, 6, 5, 4 };
        const int expected = 8;

        // Act.
        var actual = intArray.SafeGetElement(2, 1, ComplicatedCalculation);

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_int_and_null_function_returns_expected()
    {
        // Arrange.
        var stringArray = new[] { 2, 4, 8, 1, 7 };
        const int expected = 8;

        // Act.
        var actual = stringArray.SafeGetElement(2);

        // Assert.
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_safe_get_element_with_invalid_parameter_of_int_and_default_parameter_function_returns_default()
    {
        // Arrange.
        var stringArray = new[] { 2, 4, 8, 1, 7 };
        const int expected = 1;

        // Act.
        var actual = stringArray.SafeGetElement(9, 1);

        // Assert.
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 6)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 7)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, 100)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, -1)]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4 }, -100)]
    public void Test_safe_get_element_with_invalid_parameter_of_int_and_function_returns_default_value(int[] intArray, int arrayLocation)
    {
        // Arrange.
        // Act.
        var actual = intArray.SafeGetElement(arrayLocation, 1, (s) => s * 99);

        // Assert.
        actual.Should().Be(1);
    }

    #endregion Int Version

    #region Test Class Version

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_test_class_and_function_returns_expected_identity()
    {
        // Arrange.
        var testArray = new[]
        {
                new SafeGetElementTestClass
                {
                    Identity = 1,
                    Name = "Pooh Bear",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-73).Year, 6, 21)
                },
                new SafeGetElementTestClass
                {
                    Identity = 2,
                    Name = "Piglet",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23)
                }
            };

        var expected = new SafeGetElementTestClass
        {
            Identity = 2,
            Name = "Piglet",
            DateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23),
            Age = 54
        };

        // Act.
        var actual = testArray.SafeGetElement(1, new SafeGetElementTestClass(), (s) => s.SetAge());

        // Assert.
        actual.Identity.Should().Be(expected.Identity);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_test_class_and_function_returns_expected_name()
    {
        // Arrange.
        var testArray = new[]
        {
                new SafeGetElementTestClass
                {
                    Identity = 1,
                    Name = "Pooh Bear",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-73).Year, 6, 21)
                },
                new SafeGetElementTestClass
                {
                    Identity = 2,
                    Name = "Piglet",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23)
                }
            };

        var expected = new SafeGetElementTestClass
        {
            Identity = 2,
            Name = "Piglet",
            DateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23),
            Age = 54
        };

        // Act.
        var actual = testArray.SafeGetElement(1, new SafeGetElementTestClass(), (s) => s.SetAge());

        // Assert.
        actual.Name.Should().Be(expected.Name);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_test_class_and_function_returns_expected_age()
    {
        // Arrange.
        var testArray = new[]
        {
                new SafeGetElementTestClass
                {
                    Identity = 1,
                    Name = "Pooh Bear",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-73).Year, 6, 21)
                },
                new SafeGetElementTestClass
                {
                    Identity = 2,
                    Name = "Piglet",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23)
                }
            };

        var expectedDateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23);
        var expectedAge = expectedDateOfBirth.GetAge();

        var expected = new SafeGetElementTestClass
        {
            Identity = 2,
            Name = "Piglet",
            DateOfBirth = expectedDateOfBirth,
            Age = expectedAge
        };

        // Act.
        var actual = testArray.SafeGetElement(1, new SafeGetElementTestClass(), (s) => s.SetAge());

        // Assert.
        actual.Age.Should().Be(expected.Age);
    }

    [Fact]
    public void Test_safe_get_element_with_invalid_parameter_of_test_class_and_function_returns_expected_default_age()
    {
        // Arrange.
        var testArray = new[]
        {
                new SafeGetElementTestClass
                {
                    Identity = 1,
                    Name = "Pooh Bear",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-73).Year, 6, 21)
                },
                new SafeGetElementTestClass
                {
                    Identity = 2,
                    Name = "Piglet",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23)
                }
            };

        var expected = new SafeGetElementTestClass();

        // Act.
        var actual = testArray.SafeGetElement(6, new SafeGetElementTestClass(), (s) => s.SetAge());

        // Assert.
        actual.Age.Should().Be(expected.Age);
    }

    [Fact]
    public void Test_safe_get_element_with_valid_parameter_of_test_class_and_extended_function_returns_expected_age()
    {
        // Arrange.
        var testArray = new[]
        {
                new SafeGetElementTestClass
                {
                    Identity = 1,
                    Name = "Pooh Bear",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-73).Year, 6, 21)
                },
                new SafeGetElementTestClass
                {
                    Identity = 2,
                    Name = "Piglet",
                    DateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23)
                }
            };

        var expectedDateOfBirth = new DateTime(DateTime.Now.AddYears(-54).Year, 2, 23);
        var expectedAge = expectedDateOfBirth.GetAge();

        var expected = new SafeGetElementTestClass
        {
            Identity = 2,
            Name = "Piglet",
            DateOfBirth = expectedDateOfBirth,
            Age = expectedAge + 1000
        };

        // Act.
        var actual = testArray.SafeGetElement(1, new SafeGetElementTestClass(), (s) =>
        {
            var result = s.SetAge();
            result.Age += 1000;
            return s;
        });

        // Assert.
        actual.Age.Should().Be(expected.Age);
    }

    #endregion Test Class Version

    private string ReverseStringElements(string testString)
    {
        var results = new StringBuilder();
        var elements = testString.Split(',');

        results.Append(elements[1].Trim());
        results.Append(" ");
        results.Append(elements[0].Trim());

        return results.ToString();
    }

    private int ComplicatedCalculation(int testInt)
    {
        var results = testInt;
        const int additionalCalculation = 4 * 4 / 2;
        results *= 2;
        results = results + 20 - additionalCalculation;
        results -= 18;

        return results;
    }
}