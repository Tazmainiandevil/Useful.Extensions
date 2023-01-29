﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class ListExtensionsTests
    {
        #region Add Many

        [Fact]
        public void add_many_with_null_list_should_throw_exception()
        {
            // Arrange
            // Act
            Action act = () => ((List<string>)null).AddMany();

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("The list cannot be null (Parameter 'src')");
        }

        [Fact]
        public void add_many_with_valid_list_but_no_params_has_same_list()
        {
            // Arrange
            var items = new List<string>();

            // Act
            items.AddMany();

            // Assert
            items.Should().BeEmpty();
        }

        [Fact]
        public void add_many_with_valid_list_and_single_list_has_list_with_additional_item()
        {
            // Arrange
            var expected = new List<string> { "Hello", "World", "Bye" };

            var items = new List<string> { "Hello", "World" };

            // Act
            items.AddMany("Bye");

            // Assert
            items.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void add_many_with_valid_list_and_single_list_has_list_with_additional_items()
        {
            // Arrange
            var expected = new List<string> { "Hello", "World", "Bye", "Later" };

            var items = new List<string> { "Hello", "World" };

            // Act
            items.AddMany("Bye", "Later");

            // Assert
            items.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void add_many_with_valid_list_and_array_of_items_has_list_with_additional_items()
        {
            // Arrange
            var expected = new List<string> { "Hello", "World", "Bye", "Later" };

            var items = new List<string> { "Hello", "World" };
            var additionalItems = new List<string> { "Bye", "Later" };

            // Act
            items.AddMany(additionalItems.ToArray());

            // Assert
            items.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void add_many_with_list_of_complex_types_with_additional_items_has_expected_list()
        {
            // Arrange
            var expected = new List<Item>
            {
                new Item { Text = "First Item", Number = 1 },
                new Item { Text = "Second Item", Number = 2 },
                new Item { Text = "Third Item", Number = 3 }
            };

            var items = new List<Item> { new Item { Text = "First Item", Number = 1 } };

            // Act
            items.AddMany(new Item { Text = "Second Item", Number = 2 }, new Item { Text = "Third Item", Number = 3 });

            // Assert
            items.Should().BeEquivalentTo(expected);
        }

        #endregion Add Many

        #region Combine

        [Fact]
        public void combine_with_null_list_throws_exception()
        {
            // Arrange
            // Act
            Action act = () => ((List<string>)null).Combine();

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("The list cannot be null (Parameter 'src')");
        }

        [Fact]
        public void combine_with_no_additional_lists_has_current_list()
        {
            // Arrange
            var list = new List<string>();

            // Act
            list.Combine();

            // Assert
            list.Should().BeEmpty();
        }

        [Fact]
        public void combine_with_additional_list_has_list_with_new_values()
        {
            // Arrange
            var expected = new List<string> { "Hello", "Bye", "World" };

            var list = new List<string> { "Hello" };
            var additionalList = new List<string> { "Bye", "World" };

            // Act
            list.Combine(additionalList);

            // Assert
            list.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void combine_with_additional_lists_has_list_with_all_additional_values()
        {
            // Arrange
            var expected = new List<string> { "Hello", "Bye", "World", "Later" };

            var list = new List<string> { "Hello" };
            var additionalList = new List<string> { "Bye", "World" };
            var anotherList = new List<string> { "Later" };

            // Act
            list.Combine(additionalList, anotherList);

            // Assert
            list.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void combine_with_additional_complex_type_lists_has_list_with_all_additional_values()
        {
            // Arrange
            var expected = new List<Item>
            {
                new Item { Text = "First Item", Number = 1 },
                new Item { Text = "Second Item", Number = 2 },
                new Item { Text = "Third Item", Number = 3 },
                new Item { Text = "Fourth Item", Number = 4 },
                new Item { Text = "Fifth Item", Number = 5 }
            };

            var items = new List<Item> { new Item { Text = "First Item", Number = 1 } };
            var additionalItems = new List<Item> { new Item { Text = "Second Item", Number = 2 }, new Item { Text = "Third Item", Number = 3 } };
            var moreItems = new List<Item> { new Item { Text = "Fourth Item", Number = 4 }, new Item { Text = "Fifth Item", Number = 5 } };

            // Act
            items.Combine(additionalItems, moreItems);

            // Assert
            items.Should().BeEquivalentTo(expected);
        }

        #endregion Combine

        #region Test Class

        private sealed class Item
        {
            public string Text { get; set; }
            public int Number { get; set; }
        }

        #endregion Test Class
    }
}