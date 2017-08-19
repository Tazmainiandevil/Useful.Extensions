using System;
using System.Dynamic;
using FluentAssertions;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
#region Get Value From Anon Tests

        [Fact]
        public void test_get_value_from_anonymous_type_with_null_object_throws_exception()
        {
            // Arrange
            ExpandoObject anonObject = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ObjectExtensions.GetValueFromAnonymousType<string>(anonObject, "Name"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void test_get_value_from_anonymous_type_with_null_or_empty_property_throws_exception(string property)
        {
            // Arrange
            var anonObject = new { Name = "Bob" };

            // Act
            // Assert            
            Assert.Throws<ArgumentNullException>(() => ObjectExtensions.GetValueFromAnonymousType<string>(anonObject, property));
        }

        [Fact]
        public void test_get_value_from_anonymous_type_for_a_string_returns_the_expected_value()
        {
            // Arrange
            var anonObject = new { Name = "Bob" };

            // Act
            var result = ObjectExtensions.GetValueFromAnonymousType<string>(anonObject, "Name");

            // Assert            
            result.Should().Be("Bob");
        }

        [Fact]
        public void test_get_value_from_anonymous_type_for_an_int_value_returns_the_expected_value()
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act
            var result = ObjectExtensions.GetValueFromAnonymousType<int>(anonObject, "Number");

            // Assert
            result.Should().Be(3);
        }


        [Fact]
        public void test_get_value_from_anonymous_type_when_the_property_does_not_exist_throws_an_exception()
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act                        
            // Assert
            Assert.Throws<ArgumentException>(() => ObjectExtensions.GetValueFromAnonymousType<int>(anonObject, "Something"));
        }


        [Fact]
        public void test_get_value_from_anonymous_type_when_the_property_exists_but_the_type_is_different_throws_an_exception()
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act                        
            // Assert
            Assert.Throws<InvalidCastException>(() => ObjectExtensions.GetValueFromAnonymousType<string>(anonObject, "Number"));
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(string))]
        [InlineData(typeof(double))]
        [InlineData(typeof(long))]
        [InlineData(typeof(DateTime))]
        public void test_get_value_from_anonymous_type_or_default_when_the_property_does_not_exist_returns_the_default<T>(T thetype)
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act
            var result = ObjectExtensions.GetValueFromAnonymousTypeOrDefault<T>(anonObject, "Something");

            // Assert
            result.Should().Be(default(T));
        }

        [Fact]
        public void test_get_value_from_anonymous_type_or_default_when_the_property_exists_but_the_type_is_different_throws_an_exception()
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act            
            // Assert
            Assert.Throws<InvalidCastException>(() => ObjectExtensions.GetValueFromAnonymousTypeOrDefault<string>(anonObject, "Number"));
        }


        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void test_get_value_from_anonymous_type_or_default_with_null_or_empty_property_returns_the_default_value(string property)
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act
            var result = ObjectExtensions.GetValueFromAnonymousTypeOrDefault<int>(anonObject, property);

            // Assert            
            result.Should().Be(default(int));
        }

#endregion Get Value From Anon Tests

#region Is Property In Anonymous Type

        [Fact]
        public void test_is_property_in_anonymous_type_with_null_object_returns_false()
        {
            // Arrange
            ExpandoObject anonObject = null;

            // Act
            var result = ObjectExtensions.IsPropertyInAnonymousType(anonObject, "Number");

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void test_is_property_in_anonymous_type_with_null_or_empty_property_returns_false(string property)
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act
            var result = ObjectExtensions.IsPropertyInAnonymousType(anonObject, property);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void test_is_property_in_anonymous_type_with_unknown_property_returns_false()
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act
            var result = ObjectExtensions.IsPropertyInAnonymousType(anonObject, "Something");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void test_is_property_in_anonymous_type_with_known_property_returns_true()
        {
            // Arrange
            var anonObject = new { Number = 3 };

            // Act
            bool result = ObjectExtensions.IsPropertyInAnonymousType(anonObject, "Number");

            // Assert
            result.Should().BeTrue();
        }
        #endregion

        #region Object Clone Tests

#if NET452 || NET46

        [Serializable]
        private class Details
        {
            public string Input { get; set; }
            public int Number { get; set; }
            public AdditionalDetails additional { get; set; }
        }

        [Serializable]
        private class AdditionalDetails
        {
            public DateTime DateOfBirth { get; set; }
        }

        [Fact]
        public void test_object_clone_throws_exception_if_not_a_serializable_object()
        {
            // Arrange
            var person = new { Input = "test1", Number = 57 };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => person.Clone());
        }

        [Fact]
        public void test_object_clone_returns_default_when_object_is_null()
        {
            // Arrange
            // Act
            var result = ((Details)null).Clone();

            // Assert            
            result.ShouldBeEquivalentTo(default(Details));
        }

        [Fact]
        public void test_object_clone_returns_an_equivalent_object()
        {
            // Arrange
            var details = new Details { Input = "test1", Number = 57, additional = new AdditionalDetails { DateOfBirth = new DateTime(1973, 5, 1) } };

            // Act
            var result = details.Clone();

            // Assert
            result.ShouldBeEquivalentTo(details);
        }


        [Fact]
        public void test_object_clone_does_not_return_the_same_reference_object()
        {
            // Arrange
            var details = new Details { Input = "test1", Number = 57, additional = new AdditionalDetails { DateOfBirth = new DateTime(1973, 5, 1) } };

            // Act
            var result = details.Clone();

            // Assert
            result.Should().NotBeSameAs(details);
        }
#endif
        #endregion Object Clone Tests
    }
}
