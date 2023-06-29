namespace Useful.Extensions.Tests;

public class EnumerableExtensionTests
{
    #region Partition extension

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void test_partition_returns_the_expected_item_count(int partitionSize)
    {
        // Arrange
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
        var expectedCount = (int)Math.Ceiling((double)items.Count / partitionSize);

        // Act
        var result = items.Partition(partitionSize).ToList();

        // Assert
        result.Count.Should().Be(expectedCount);
    }

    public static IEnumerable<object[]> PartitionItemsTestData
    {
        get
        {
            yield return new object[] { 1, new[] { new[] { 0 }, new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4 }, new[] { 5 }, new[] { 6 } } };
            yield return new object[] { 2, new[] { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4, 5 }, new[] { 6 } } };
            yield return new object[] { 3, new[] { new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6 } } };
        }
    }

    [Theory]
    [MemberData(nameof(PartitionItemsTestData))]
    public void test_partition_returns_the_expected_items(int partitionSize, IEnumerable<int[]> expectedItems)
    {
        // Arrange
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        var result = items.Partition(partitionSize).ToList();

        // Assert
        result.Should().BeEquivalentTo(expectedItems);
    }

    [Fact]
    public void test_partition_returns_all_items_if_less_than_partitionSize()
    {
        // Arrange
        const int partitionSize = 8;
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        var result = items.Partition(partitionSize).ToList();

        // Assert
        result.First().Count().Should().Be(7);
    }

    [Fact]
    public void test_partition_returns_one_entry_if_items_are_less_than_partitionSize()
    {
        // Arrange
        const int partitionSize = 8;
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        var result = items.Partition(partitionSize).ToList();

        // Assert
        result.Count.Should().Be(1);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void test_an_invalid_partition_size_throws_an_exception(int invalidSize)
    {
        // Arrange
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => items.Partition(invalidSize).ToList());
    }

    #endregion Partition extension

    #region Partition Queryable extension

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void test_queryable_partition_returns_the_expected_item_count(int partitionSize)
    {
        // Arrange
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
        var expectedCount = (int)Math.Ceiling((double)items.Count / partitionSize);

        // Act
        var result = items.AsQueryable().Partition(partitionSize).ToList();

        // Assert
        result.Count.Should().Be(expectedCount);
    }

    [Fact]
    public void test_queryable_partition_returns_all_items_if_less_than_size()
    {
        // Arrange
        const int partitionSize = 8;
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        var result = items.AsQueryable().Partition(partitionSize).ToList();

        // Assert
        result.First().Count().Should().Be(7);
    }

    [Fact]
    public void test_queryable_partition_returns_one_entry_if_items_are_less_than_partitionSize()
    {
        // Arrange
        const int partitionSize = 8;
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        var result = items.AsQueryable().Partition(partitionSize).ToList();

        // Assert
        result.Count.Should().Be(1);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void test_queryable_invalid_partition_size_throws_an_exception(int invalidSize)
    {
        // Arrange
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => items.AsQueryable().Partition(invalidSize).ToList());
    }

    #endregion Partition Queryable extension

    #region Page extension

    public static IEnumerable<object[]> PageItemsTestData
    {
        get
        {
            yield return new object[] { new List<int> { 0, 1 }, 0, 2 };
            yield return new object[] { new List<int> { 3, 4, 5, 6 }, 3, 5 };
        }
    }

    [Theory]
    [MemberData(nameof(PageItemsTestData))]
    public void test_page_returns_the_expected_items(IEnumerable<int> expected, int start, int length)
    {
        // Arrange
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        var result = items.Page(start, length);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> PageEnumerableTestData
    {
        get
        {
            yield return new object[] { 0, 0, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
            yield return new object[] { 0, -1, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
            yield return new object[] { -1, 0, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
            yield return new object[] { -1, -1, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
            yield return new object[] { 0, 0, new[] { 0, 1, 2, 3, 4, 5, 6 } };
            yield return new object[] { 0, -1, new[] { 0, 1, 2, 3, 4, 5, 6 } };
            yield return new object[] { -1, 0, new[] { 0, 1, 2, 3, 4, 5, 6 } };
            yield return new object[] { -1, -1, new[] { 0, 1, 2, 3, 4, 5, 6 } };
        }
    }

    [Theory]
    [MemberData(nameof(PageEnumerableTestData))]
    public void test_page_with_invalid_parameters_returns_an_empty_set(int start, int length, IEnumerable<int> items)
    {
        // Arrange
        // Act
        var result = items.Page(start, length);

        // Assert
        result.Should().BeEquivalentTo(new int[] { });
    }

    #endregion Page extension

    #region Page Queryable extension

    [Theory]
    [MemberData(nameof(PageItemsTestData))]
    public void test_queryable_page_returns_expected_items(IEnumerable<int> expected, int start, int length)
    {
        // Arrange
        var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        // Act
        var result = items.AsQueryable().Page(start, length);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(PageEnumerableTestData))]
    public void test_queryable_page_with_invalid_parameters_returns_an_empty_set(int start, int length, IEnumerable<int> items)
    {
        // Arrange
        // Act
        var result = items.AsQueryable().Page(start, length);

        // Assert
        result.Should().BeEquivalentTo(new int[] { });
    }

    #endregion Page Queryable extension

    #region IsNullOrEmpty extension

    public static IEnumerable<object[]> IsNullOrEmptyTestData
    {
        get
        {
            yield return new object[] { Enumerable.Empty<int>() };
            yield return new object[] { null };
        }
    }

    [Theory]
    [MemberData(nameof(IsNullOrEmptyTestData))]
    public void iEnumerable_is_null_or_empty_returns_true(IEnumerable<int> list)
    {
        // Arrange
        // Act
        // Assert
        list.IsNullOrEmpty().Should().BeTrue();
    }

    [Fact]
    public void iEnumerable_is_null_or_empty_with_entries_returns_false()
    {
        // Arrange
        var list = new[] { 1, 2, 3, 4 };

        // Act
        // Assert
        list.IsNullOrEmpty().Should().BeFalse();
    }

    [Fact]
    public void iEnumerable_is_null_or_empty_not_with_entries_returns_true()
    {
        // Arrange
        var list = new[] { 1, 2, 3, 4 };

        // Act
        var result = !list.IsNullOrEmpty();

        // Assert
        result.Should().BeTrue();
    }

    public static IEnumerable<object[]> IsNullOrEmptyCollectionTestData
    {
        get
        {
            yield return new object[] { new List<int>() };
            yield return new object[] { null };
        }
    }

    [Theory]
    [MemberData(nameof(IsNullOrEmptyCollectionTestData))]
    public void collection_is_null_or_empty_returns_true(List<int> list)
    {
        // Arrange
        // Act
        // Assert
        list.IsNullOrEmpty().Should().BeTrue();
    }

    [Fact]
    public void collection_is_null_or_empty_with_entries_returns_false()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4 };

        // Act
        // Assert
        list.IsNullOrEmpty().Should().BeFalse();
    }

    [Fact]
    public void collection_is_null_or_empty_not_with_entries_returns_true()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4 };

        // Act
        var result = !list.IsNullOrEmpty();

        // Assert
        result.Should().BeTrue();
    }

    #endregion IsNullOrEmpty extension

    #region IsValueInList extension

    [Fact]
    public void given_is_value_in_list_when_list_of_strings_is_null_then_false_is_returned()
    {
        // Arrange
        // Act
        var result = ((string[])null).IsValueInList("one");

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void given_is_value_in_list_when_list_of_strings_and_search_is_null_or_empty_then_false_is_returned(string data)
    {
        var list = new[] { "One", "Two", "Three", "Four" };

        // Act
        var result = list.IsValueInList(data);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_strings_and_value_exists_then_true_is_returned()
    {
        var list = new[] { "One", "Two", "Three", "Four" };

        // Act
        var result = list.IsValueInList("one");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_strings_and_value_not_in_the_list_then_false_is_returned()
    {
        var list = new[] { "One", "Two", "Three", "Four" };

        // Act
        var result = list.IsValueInList("five");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_strings_is_null_case_sensitivity_defined_then_false_is_returned()
    {
        // Arrange
        // Act
        var result = ((string[])null).IsValueInList("one", StringComparison.Ordinal);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void given_is_value_in_list_when_list_of_strings_and_search_is_null_or_empty_and_case_defined_then_false_is_returned(string data)
    {
        var list = new[] { "One", "Two", "Three", "Four" };

        // Act
        var result = list.IsValueInList(data, StringComparison.Ordinal);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_strings_and_value_exists_and_search_by_case_then_true_is_returned()
    {
        var list = new[] { "One", "Two", "Three", "Four" };

        // Act
        var result = list.IsValueInList("One", StringComparison.Ordinal);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_strings_and_value_exists_and_search_by_case_insensitive_then_true_is_returned()
    {
        var list = new[] { "One", "Two", "Three", "Four" };

        // Act
        var result = list.IsValueInList("two", StringComparison.OrdinalIgnoreCase);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_integers_and_value_exists_then_true_is_returned()
    {
        var list = new[] { 1, 2, 3, 4 };

        // Act
        var result = list.IsValueInList(1);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_integers_and_value_not_in_the_list_then_false_is_returned()
    {
        var list = new[] { 1, 2, 3, 4 };

        // Act
        var result = list.IsValueInList(8);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_doubles_and_value_exists_then_true_is_returned()
    {
        var list = new[] { 1.1d, 2.2d, 3.3d, 4.4d };

        // Act
        var result = list.IsValueInList(3.3d);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_doubles_and_value_not_in_the_list_then_false_is_returned()
    {
        var list = new[] { 1.1d, 2.2d, 3.3d, 4.4d };

        // Act
        var result = list.IsValueInList(8.3d);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_integers_is_null_then_false_is_returned()
    {
        // Arrange
        // Act
        var result = ((int[])null).IsValueInList(1);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_objects_is_null_then_false_is_returned()
    {
        // Arrange
        // Act
        var result = ((TestInValue[])null).IsValueInList(new TestInValue());

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void given_is_value_in_list_when_list_of_objects_with_equality_and_in_the_list_then_true_is_returned()
    {
        // Arrange
        var items = new[]
        {
                new TestInValue { Id = 1, Value = "One" },
                new TestInValue { Id = 2, Value = "Two" }
            };

        // Act
        var result = items.IsValueInList(new TestInValue { Id = 2, Value = "Two" });

        // Assert
        result.Should().BeTrue();
    }

    public class TestInValue : IEquatable<TestInValue>
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public bool Equals(TestInValue other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other?.Id) && Value.Equals(other?.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals((TestInValue)obj);
        }
    }

    #endregion IsValueInList extension

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