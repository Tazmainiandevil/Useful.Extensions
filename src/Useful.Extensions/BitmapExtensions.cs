#if NET452 || NET46
using System;
using System.Drawing;
using System.IO;

namespace Useful.Extensions
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Convert a base64 image string to a bitmap object
        /// </summary>
        /// <param name="src">The string containing the base64 representation of an image</param>
        /// <returns>A bitmap or throws an exception if not a valid string</returns>
        public static Bitmap Base64ToBitmap(this string src)
        {
            if (string.IsNullOrWhiteSpace(src) || !src.IsBase64())
            {
                throw new ArgumentException("Invalid base 64 string");
            }

            Bitmap result;

            var byteBuffer = Convert.FromBase64String(src);
            using (var memoryStream = new MemoryStream(byteBuffer))
            {
                memoryStream.Position = 0;
                result = (Bitmap)Image.FromStream(memoryStream);
            }

            return result;
        }
    }
}
#endif