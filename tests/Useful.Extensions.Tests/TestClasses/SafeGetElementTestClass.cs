using System;

namespace Useful.Extensions.Tests.TestClasses
{
    /// <summary>
    /// This is a class used for the Safe Get Element tests.
    /// It is to prove that the generic function can handle different type of
    /// classes, as well as the normal types, like string and int.
    /// </summary>
    public class SafeGetElementTestClass
    {
        public int Identity { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

        public SafeGetElementTestClass SetAge()
        {
            Age = DateOfBirth.GetAge();
            return this;
        }
    }
}
