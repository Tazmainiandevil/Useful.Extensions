# Useful.Extensions
A group of useful extensions in C# for .NET 4.5+

<image src="https://ci.appveyor.com/api/projects/status/github/Tazmainiandevil/Useful.Extensions?branch=master&svg=true">
[![NuGet version](https://badge.fury.io/nu/Useful.Extensions.svg)](https://badge.fury.io/nu/Useful.Extensions)

I found myself creating useful extensions over and over as I moved along my career path and decided that they actually needed a home to be reusable and grow. They are not trade secrets or proprietary code they are just little bits of code that are useful.

__Character Extensions__

_EqualTo_ - A simple comparision of characters including an option to compare by case (default is to ignore case)

e.g. 
```C#
'c'.EqualTo('C')
```
or 
```C#
'c'.EqualTo('C', StringComparison.Ordinal)
```

__String Extensions__

_HasValue_ - Looks for a specified value inside a string including an option to compare by case (default is to ignore case)

e.g. 
```C#
"XYZ".HasValue("x")
``` 
or 
```C#
"XYZ".HasValue("x", StringComparison.Ordinal)
```

_EqualsIgnoreCase_ - A wrapper around `Equals` to always be a case insensitive check

e.g.
```C#
"XYZ.EqualsIgnoreCase("xyz")
```

_SubstringOrEmpty_ - A safe version of substring that returns an empty string if the substring is completely out of range or the value found if the length is out of range

e.g. 
```C#
"Hello World".SubstringOrEmpty(12, 10)
```
returns ""

```C#
"Hello World".SubstringOrEmpty(9, 10)
```
returns "ld"

_SubstringAfterValue_ - A substring that returns the remainaing string after a given string or character to find ignoring case

e.g.
```C#
"Hello World".SubstringAfterValue("Hello ")
```
returns "World"
```C#
"Hello World".SubstringAfterValue('W')
```
returns "orld"

_SubstringBeforeValue_ - A substring that returns the string before a given string or character ignoring case

e.g.
```C#
"Hello World".SubstringBeforeValue(" World")
```
returns "Hello"
```C#
"Hello World".SubstringBeforeValue('W')
```
returns "Hello "

_SafeTrim_ - A safe version of trim that does not throw an exception if the string is null

e.g. 
```C#
(null as string).SafeTrim()
```
returns null

_Base64ToBitmap_ - A simple extension that takes a base64 bitmap string and converts it back to a Bitmap

e.g.
```C#
// The full base64 string would be larger but for this example a small bit shown then ...
"/9j/4AAQSkZJRgABAQEBLAEsAAD/2wBDAAIBAQIBAQIC...".Base64ToBitmap();
```
returns a bitmap object of the image

_IsBase64_ - A check to see if a string has been base64 encoded
e.g.
```C#
var value = Convert.ToBase64String(Encoding.UTF8.GetBytes("some value"), Base64FormattingOptions.None);
value.IsBase64();
```
returns true

or 
```C#
"some value".IsBase64();
```
returns false
__Dictionary Extensions__

_ValueOrDefault_ - Get the value from a dictionary or return the default for the value type (the default value can be specified if needed)

e.g.

```C#
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.ValueOrDefault("value", 99)
```
returns 99

```C#
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.ValueOrDefault("some text")
```
returns 1

_TryGetValueOrDefault_ - Same as _ValueOrDefault_ only uses the dictionary method TryGetValue which depending on dictionary size can be more performant

e.g.

```C#
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.TryGetValueOrDefault("value", 99)
``` 
returns 99

```C#
new Dictionary<string, int> { { "some text", 1 }, { "more text", 2 } }.TryGetValueOrDefault("some text")
```
returns 1

__Enumerable Extensions__

_Partition_ - Splits an IEnumerable collection into multiple collections based on size

e.g.

```C#
var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };`
var result = items.Partition(4);
```
returns an `IEnumerable<IEnumerable<int>>` that contains 2 lists one with 0, 1, 2, 3 one with 4, 5, 6

_Page_ - Get a part or page of data from an IEnumerable 

e.g.

```C#
var items = new List<int> { 0, 1, 2, 3, 4, 5, 6 };`
items.Page(0, 2);
```
returns a list with 0 and 1 in

__Object Extensions__

_Clone_ - An implentation of a deep clone

_GetValueFromAnonymousType_ - Not an extension but a helper to get values from `dynamic` objects

e.g. 
```C#
var anon = new { Text = "Hello" };`
ObjectExtensions.GetValueFromAnonymousType<string>(anon, "Text");
``` 
returns "Hello"

__Nullable Extensions__

_ToStringOrEmpty_ - String representation of a nullable value or an empty string if the nullable has no value

_IsEqual_ - Checks if the nullable value is equal another value

e.g.
```C#
int? value = null;
value.IsEqual(14);
```
returns false

_IsNullOrDefault_ - Check if the value is null or the default value

e.g.
```C#
int? value = 0;
value.IsNullOrDefault();
```
returns true as the default for an int is 0

```C#
bool? value = true;
value.IsNullOrDefault();
```
returns false

_ValueOrDefault_ - Retreives the value of the nullable or the default of the type if there is not a value or specified default value

e.g.
```C#
long? value = 123456;
value.ValueOrDefault();
```
returns 123456
```C#
int? value = null;
value.ValueOrDefault(-1);
```
returns -1 as that was the default specified
