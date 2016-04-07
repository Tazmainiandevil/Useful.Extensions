using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Useful.Extensions.Tests
{
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
            var items = new List<int> {0, 1, 2, 3, 4, 5, 6};
            var expectedCount = (int)Math.Ceiling((double)items.Count/partitionSize);

            // Act
            var result = items.Partition(partitionSize).ToList();

            // Assert
            result.Count().Should().Be(expectedCount);
        }

        public static IEnumerable<object[]> PartitionItemsTestData
        {
            get
            {
                yield return new object[] { 1, new [] { new int[] { 0 }, new int[] { 1 }, new int[] { 2 }, new int[] { 3 } , new int[] { 4 }, new int[] { 5 }, new int[] { 6 } } };
                yield return new object[] { 2, new [] { new int[] { 0, 1 }, new int[] { 2, 3 }, new int[] { 4, 5 }, new int[] { 6 } } };
                yield return new object[] { 3, new [] { new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, } } };                
            }
        }

        [Theory]
        [MemberData("PartitionItemsTestData")]
        public void test_partition_returns_the_expected_items(int partitionSize, IEnumerable<int[]> expectedItems)
        {
            // Arrange
            var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
            var expectedCount = (int)Math.Ceiling((double)items.Count / partitionSize);

            // Act
            var result = items.Partition(partitionSize).ToList();

            // Assert
            result.ShouldBeEquivalentTo(expectedItems);
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
            result.Count().Should().Be(1);
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

        #endregion Partition

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
            result.Count().Should().Be(expectedCount);
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
            result.Count().Should().Be(1);
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
        #endregion


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
        [MemberData("PageItemsTestData")]
        public void test_page_returns_the_expected_items(IEnumerable<int> expected, int start, int length)
        {
            // Arrange
            var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

            // Act
            var result = items.Page(start, length);
            
            // Assert
            result.ShouldAllBeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> PageEnumerableTestData
        {
            get
            {
                yield return new object[] { 0, 0, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { 0, -1, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { -1, 0, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { -1, -1, new List<int> { 0, 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { 0, 0, new int[] { 0, 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { 0, -1, new int[] { 0, 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { -1, 0, new int[] { 0, 1, 2, 3, 4, 5, 6 } };
                yield return new object[] { -1, -1, new int[] { 0, 1, 2, 3, 4, 5, 6 } };
            }
        }

        [Theory]
        [MemberData("PageEnumerableTestData")]
        public void test_page_with_invalid_parameters_returns_an_empty_set(int start, int length, IEnumerable<int> items)
        {
            // Arrange
            // Act
            var result = items.Page(start, length);

            // Assert
            result.ShouldAllBeEquivalentTo(new int[] { });
        }
        #endregion

        #region Page Queryable extension

        [Theory]
        [MemberData("PageItemsTestData")]
        public void test_queryable_page_returns_expected_items(IEnumerable<int> expected, int start, int length)
        {
            // Arrange
            var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

            // Act
            var result = items.AsQueryable().Page(start, length);

            // Assert
            result.ShouldAllBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData("PageEnumerableTestData")]
        public void test_queryable_page_with_invalid_parameters_returns_an_empty_set(int start, int length, IEnumerable<int> items)
        {
            // Arrange            
            // Act
            var result = items.AsQueryable().Page(start, length);

            // Assert
            result.ShouldAllBeEquivalentTo(new int[] { });
        }
        #endregion
    }
}
