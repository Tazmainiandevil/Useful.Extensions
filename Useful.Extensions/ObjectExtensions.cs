using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Useful.Extensions
{
    /// <summary>
    /// Extensions for Object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extract the value from a anonymous object by property name
        /// </summary>
        /// <param name="src"></param>
        /// <param name="property">The name of the property to get the value from</param>
        /// <returns>The value of the given property</returns>
        public static T GetValueFromAnonymousType<T>(object src, string property)
        {
            var type = src.GetType();
            return (T)type.GetProperty(property).GetValue(src, null);
        }

        /// <summary>
        /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
        /// Binary Serialization is used to perform the copy.
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
