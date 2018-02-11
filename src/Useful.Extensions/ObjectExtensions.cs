#if NET452 || NET46
using System;
using System.Reflection;
using System.IO;
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
        /// <param name="src">The anonymous object</param>
        /// <param name="property">The name of the property to get the value from</param>
        /// <returns>The value of the given property</returns>
        public static T GetValueFromAnonymousType<T>(object src, string property)
        {
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }

            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentNullException(nameof(property));
            }

            var type = src.GetType();
            var objectProperty = type.GetTypeInfo().GetProperty(property);

            if (objectProperty == null)
            {
                throw new ArgumentException("Property not found in anonymous object");
            }
            return (T)objectProperty.GetValue(src, null);
        }

        /// <summary>
        /// Extract the value from an anonymouse object or return the default if not found
        /// </summary>
        /// <param name="src">The anonymous object</param>
        /// <param name="property">The name of the property to get the value from</param>
        /// <returns>The value of the given property or the default</returns>
        public static T GetValueFromAnonymousTypeOrDefault<T>(object src, string property)
        {
            try
            {
                return GetValueFromAnonymousType<T>(src, property);
            }
            catch (ArgumentException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Check if an anonymous object has a property with a given name
        /// </summary>
        /// <param name="src">The anonymous object</param>
        /// <param name="property">The name of the property to get the value from</param>
        /// <returns>A boolean denoting if the property exists</returns>
        public static bool IsPropertyInAnonymousType(object src, string property)
        {
            if (src == null || string.IsNullOrWhiteSpace(property))
            {
                return false;
            }

            var type = src.GetType();
            var objectProperty = type.GetProperty(property);
            if (objectProperty == null)
            {
                return false;
            }

            return true;
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
#endif