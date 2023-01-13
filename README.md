# Useful.Extensions

A group of useful extensions in C#, supporting .NET Standard 2.0, .NET 6.0 and .NET 7.0

<image src="https://ci.appveyor.com/api/projects/status/github/Tazmainiandevil/Useful.Extensions?branch=master&svg=true">
<a href="https://badge.fury.io/nu/Useful.Extensions"><img src="https://badge.fury.io/nu/Useful.Extensions.svg" alt="NuGet version" height="18"></a>

I found myself creating useful extensions over and over as I moved along my career path and decided that they actually needed a home to be reusable and grow. They are not trade secrets or proprietary code they are just little bits of code that are useful.

## Methods

### Array Extensions

_SafeGetElement_ - Gets an element from an array, without throwing an exception if the index is out of bounds.

#### Using just the index parameter (array location)

e.g.

```csharp
var intArray = new[] { 2, 4, 8, 1, 7 };
var itemValue = intArray.SafeGetElement(2);
```

itemValue will be 8.

#### Using the array location and a default value to return if the element is not found

e.g.

```csharp
var intArray = new[] { 2, 4, 8, 1, 7 };
var itemValue = intArray.SafeGetElement(20, 1);
```

itemValue will be 1, as the array does not contain 20 elements.

#### Using the array location, default value and a function to apply to the element value

e.g.

```csharp
var intArray = new[] { 2, 4, 8, 1, 7 };
var itemValue = intArray.SafeGetElement(1, 1, (s) => s * 2);
```

`itemValue` will be 8, as the element at location 1, is 4, which is then multiplied by 2.

The test project has more examples using strings and classes.

### Character Extensions

_EqualTo_ - A simple comparison of characters including an option to compare by the case (default is to ignore case)

e.g.

```csharp
'c'.EqualTo('C');
```

or

```csharp
'c'.EqualTo('C', StringComparison.Ordinal);
```

### String Extensions

_ContainsValue (same as HasValue)_ - Looks for a specified value inside a string including an option to compare by the case (default is to ignore case)

e.g.

```csharp
"XYZ".ContainsValue("x");
```

or

```csharp
"XYZ".ContainsValue("x", StringComparison.Ordinal);
```

_HasValue_ - Looks for a specified value inside a string including an option to compare by the case (default is to ignore case)

e.g.

```csharp
"XYZ".HasValue("x");
```

or

```csharp
"XYZ".HasValue("x", StringComparison.Ordinal);
```

_EqualsIgnoreCase_ - A wrapper around `Equals` to always be a case-insensitive check. The check is also safe and returns false if the string is null or empty.

e.g.

```csharp
"XYZ".EqualsIgnoreCase("xyz");
```

```csharp
string value = null;
value.EqualsIgnoreCase(null);
```

both examples return true

_SubstringOrEmpty_ - A safe version of substring that returns an empty string if the substring is completely out of range or the value found if the length is out of range

e.g.

```csharp
"Hello World".SubstringOrEmpty(12, 10);
```

returns ""

```csharp
"Hello World".SubstringOrEmpty(9, 10);
```

returns "ld"

_SubstringAfterValue_ - A substring that returns the remaining string after a given string or character ignoring the case by default

e.g.

```csharp
"Hello World".SubstringAfterValue("Hello ");
```

returns "World"

```csharp
"Hello World".SubstringAfterValue('W');
```

returns "orld"

Case Sensitive substring

```csharp
"Hello world World".SubstringAfterValue('W', StringComparison.Ordinal);
```

returns "orld"

```csharp
"Hello world World".SubstringAfterValue("world", StringComparison.Ordinal);
```

returns " World"

_SubstringAfterLastValue_ - A substring that returns the remaining string after the last occurrence of a given character or string ignoring case by default

e.g.

```csharp
"Hello World hello@world.com".SubstringAfterLastValue("Hello");
```

returns "@world.com"

```csharp
"Hello World hello@world.com".SubstringAfterLastValue('W');
```

returns "orld.com"

Case Sensitive substring

```csharp
"Hello world World".SubstringAfterLastValue('W', StringComparison.Ordinal);
```

returns "orld"

```csharp
"Hello world World".SubstringAfterLastValue("world", StringComparison.Ordinal);
```

returns " World"

_SubstringBeforeValue_ - A substring that returns the string before a given string or character ignoring the case by default

e.g.

```csharp
"Hello World".SubstringBeforeValue(" World");
```

returns "Hello"

```csharp
"Hello World".SubstringBeforeValue('W');
```

returns "Hello "

Case Sensitive substring

```csharp
"Hello world World".SubstringBeforeValue(" World", StringComparison.Ordinal);
```

returns "Hello world"

```csharp
"Hello world World".SubstringBeforeValue('W', StringComparison.Ordinal);
```

returns "Hello world "

_SubstringBeforeLastValue_ - A substring that returns the string before the last occurrence of a given string or character ignoring the case by default

e.g.

```csharp
"Hello World World".SubstringBeforeLastValue(" world");
```

returns "Hello World"

```csharp
"Hello World World".SubstringBeforeLastValue('w');
```

returns ""Hello World "

Case Sensitive substring

```csharp
"Hello World world".SubstringBeforeLastValue(" World", StringComparison.Ordinal);
```

returns "Hello"

```csharp
"Hello World world".SubstringBeforeLastValue('W', StringComparison.Ordinal);
```

returns ""Hello "

_SafeTrim_ - A safe version of trim that does not throw an exception if the string is null

e.g.

```csharp
(null as string).SafeTrim();
```

returns null

_SafeStartsWith_ - A safe version of starts with that does not throw an exception if the string is null

e.g.

```csharp
(null as string).SafeStartsWith("Find");
```

returns false

_SafeEndsWith_ - A safe version of ends with that does not throw an exception if the string is null

e.g.

```csharp
(null as string).SafeEndsWith("Find");
```

returns false

_IsBase64_ - A check to see if a string has been base64 encoded
e.g.

```csharp
var value = Convert.ToBase64String(Encoding.UTF8.GetBytes("some value"), Base64FormattingOptions.None);
value.IsBase64();
```

returns true

or

```csharp
"some value".IsBase64();
```

returns false

_IsAllNumbers_ - Determine if the whole string value contains only number characters

e.g.

```csharp
"1235456".IsAllNumbers();
```

returns true

e.g.

```csharp
"1235456ABc".IsAllNumbers();
```

returns false

_IsAllAlpha_ - Determine if the whole string value contains only alpha characters

e.g.

```csharp
"ABCDE".IsAllAlpha();
```

returns true

e.g.

```csharp
"ABCDE123$".IsAllAlpha();
```

returns false

_IsAllAlphaOrNumbers_ -  Determine if the whole string value contains only alpha or number characters

e.g.

```csharp
"1A235456c".IsAllAlphaOrNumbers();
```

returns true

e.g.

```csharp
"1A235456c%%".IsAllAlphaOrNumbers();
```

returns false

### Dictionary Extensions

_ValueOrDefault_ - Get the value from a dictionary or return the default for the value type (the default value can be specified if needed)

e.g.

```csharp
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.ValueOrDefault("value", 99);
```

returns 99

```csharp
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.ValueOrDefault("some text");
```

returns 1

_TryGetValueOrDefault_ - Same as _ValueOrDefault_ only uses the dictionary method TryGetValue which depending on dictionary size can be more performant

e.g.

```csharp
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.TryGetValueOrDefault("value", 99);
```

returns 99

```csharp
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.TryGetValueOrDefault("some text");
```

returns 1

### Enumerable Extensions

_Partition_ - Splits an IEnumerable collection into multiple collections based on size

e.g.

```csharp
var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };`
var result = items.Partition(4);
```

returns an `IEnumerable<IEnumerable<int>>` that contains 2 lists one with 0, 1, 2, 3 one with 4, 5, 6

_Page_ - Get a part or page of data from an IEnumerable

e.g.

```csharp
var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };`
items.Page(0, 2);
```

returns a list with 0 and 1 in

_IsNullOrEmpty_ - Performs a check on a collection for null or empty

e.g.

```csharp
List<int> list = null;
if(list.IsNullOrEmpty())
{
  // Perform logic here
}
```

or

```csharp
var list = new List<string> { "somevalue" };
if(!list.IsNullOrEmpty())
{
  // Perform logic here
}
```

_IsValueInList_ - Checks for a value that exists in the list

e.g.

```csharp
var list = new[] { "One", "Two", "Three", "Four" };
if(list.IsValueInList("five"))
{
  // Perform logic here
}
```

or

```csharp
var list = new[] { 1, 2, 3, 4 };
if(list.IsValueInList(3))
{
  // Perform logic here
}
```

### List Extensions

_Combine_ - Combine multiple lists together

e.g.

```csharp
var list = new List<string> { "Hello" };
var additionalList = new List<string> { "World" };
var anotherList = new List<string> { "Bye" };

list.Combine(additionalList, anotherList);
```

Results in a list containing "Hello", "World", "Bye"

_Add Many_ - Add multiple items to a list

e.g.

```csharp
var items = new List<string> { "Hello", "World" };

items.AddMany("Another", "Day");
```

Results in a list containing "Hello", "World", "Another", "Day"

### Nullable Extensions

_ToStringOrEmpty_ - String representation of a nullable value or an empty string if the nullable has no value

_IsEqual_ - Checks if the nullable value is equal to another value

e.g.

```csharp
int? value = null;
value.IsEqual(14);
```

returns false

_IsNullOrDefault_ - Check if the value is null or the default value

e.g.

```csharp
int? value = 0;
value.IsNullOrDefault();
```

returns true as the default for an int is 0

```csharp
bool? value = true;
value.IsNullOrDefault();
```

returns false

_ValueOrDefault_ - Retrieves the value of the nullable or the default of the type if there is not a value or specified default value

e.g.

```csharp
long? value = 123456;
value.ValueOrDefault();
```

returns 123456

```csharp
int? value = null;
value.ValueOrDefault(-1);
```

returns -1 as that was the default specified

### Enum Flag Extensions

```Csharp
[Flags]
public enum TestEnum : short
{
    None = 0,
    Item1 = 1,
    Item2 = 2,
    Item3 = 4,
    Item4 = 8,
    Item5 = 16,
    Item6 = 32
}
```

_Contains_ - Returns a boolean to denote if the specified flag has been set

```Csharp
var testValue = TestEnum.Item1;

var result = testValue.Contains(TestEnum.Item1);
```

result would be true

_HasAnyOf_ - Returns a boolean to denote if any of the specified flags have been set

```Csharp
var testValue = TestEnum.Item1 | TestEnum.Item5;

var result = testValue.HasAnyOf(TestEnum.Item5);
```

result would be true

_HasAllOf_ - Returns a boolean to denote if all of the specified flags have been set

```csharp
var testValue = TestEnum.Item1 | TestEnum.Item5 | TestEnum.Item2;

var result = testValue.HasAllOf(TestEnum.Item5, TestEnum.Item2);
```

result would be true

_Set_ - Set flags on a given enum

```csharp
 var value = TestEnum.None;
 var result = value.Set(TestEnum.Item2, TestEnum.Item6);

 result.All(TestEnum.Item2, TestEnum.Item6)
```

result would be true

_UnSet_ - UnSet flags on a give enum

```csharp
 var value = TestEnum.None;
 var result = value.UnSet(TestEnum.Item2);

 result.Contains(TestEnum.Item2);
```

return would be false

### Date Time Extensions

_ShouldBeWithinRangeOf_ - Determines that a DateTime is close to an expected date.  Has an optional parameter of an int.  This represents the variation we can accept in seconds.

e.g.

```csharp
var dateToCheck = new DateTime(2017, 01, 01, 12, 30, 00);
var expectedDateToCheck = new DateTime(2017, 01, 01, 12, 30, 05);

dateToCheck.ShouldBeWithinRangeOf(expectedDateToCheck);
```

returns true, as the date and time we are checking are within five seconds of the expected date and time.

```csharp
var dateToCheck = new DateTime(2017, 01, 01, 12, 30, 00);
var expectedDateToCheck = new DateTime(2017, 01, 01, 12, 30, 11);

dateToCheck.ShouldBeWithinRangeOf(expectedDateToCheck);
```

returns false, as the date and time we are checking are over the default ten seconds of the expected date and time.

```csharp
var dateToCheck = new DateTime(2017, 01, 01, 12, 30, 00);
var expectedDateToCheck = new DateTime(2017, 01, 01, 12, 30, 19);

dateToCheck.ShouldBeWithinRangeOf(expectedDateToCheck, 20);
```

returns true, as the date and time we are checking are within nineteen seconds of the expected date and time.  This is using the optional parameter where we can set the seconds of variation.

_Between_ - Determines if the DateTime is between a specified start and end time.  Has an optional parameter of a bool.  This defaults to false and doesn't include the specific start time or end time.

e.g.

```csharp
var dateToCheck = new DateTime(2017, 01, 01, 12, 30, 00);
var startOfRange = new DateTime(2016, 12, 15, 12, 30, 00);
var endOfRange = new DateTime(2017, 01, 02, 23, 59, 59);

dateToCheck.Between(startOfRange, endOfRange);
```

returns true, as the date and time being checked are within the ranges supplied.

```csharp
var dateToCheck = new DateTime(2017, 01, 02, 12, 30, 00);
var startOfRange = new DateTime(2016, 12, 15, 12, 30, 00);
var endOfRange = new DateTime(2017, 01, 02, 12, 30, 00);

dateToCheck.Between(startOfRange, endOfRange, true);
```

returns true, as the date and time being checked are within the ranges supplied, and as the inclusive parameter is true, we are going right up to the end range limit.

_GetAge_ - Get the age from a given date representing the Date of Birth and now or a given date

e.g.

```csharp
var dob = new DateTime(2003, 07, 04);

var ageToday = dob.GetAge();

```

ageToday (ran on 05/01/2020) would return 16. If supplying the date then it would return the same using the syntax below

e.g.

```csharp
var dob = new DateTime(2003, 07, 04);

var ageToday = dob.GetAge(new DateTime(2020, 01, 05));

```

### System Time Helpers

Allow date/time now entries to be testable. This was inspired by Oren Eini https://ayende.com/blog/3408/dealing-with-time-in-tests.

e.g.

```csharp
var now = SystemTime.Now();

var utc = SystemTime.UtcNow();
```

To set up a time in the unit tests so that when System.Now() or System.UtcNow() is called it will be a specified date/time

```csharp
SystemTime.Now = () => new DateTime(2000, 1, 1, 10, 10, 47);

SystemTime.UtcNow = () => new DateTime(2000, 1, 1, 9, 10, 47);
```

### Object Helpers

_Clone_ - An implementation of a deep clone using serialization

The next set are helper functions rather than extensions

_GetValueFromAnonymousType_ - Get values from `dynamic` objects by property name

e.g.

```csharp
var anon = new { Text = "Hello" };`
ObjectExtensions.GetValueFromAnonymousType<string>(anon, "Text");
```

returns "Hello"

_GetValueFromAnonymousTypeOrDefault_ - As with GetValueFromAnonymousType but returns a default value for the type if not found
e.g.

```csharp
var anon = new { Text = "Hello" };`
ObjectExtensions.GetValueFromAnonymousTypeOrDefault<string>(anon, "Value");
```

returns null

_IsPropertyInAnonymousType_ - Checks to see if there is a property of a given name inside a `dynamic` object
e.g.

```csharp
var anon = new { Text = "Hello" };`
ObjectExtensions.IsPropertyInAnonymousType(anon, "Value");
```

returns false
