using System;

namespace ImageCaster.Extensions
{
    /// <summary>
    /// Add utility extension methods to all types.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>Require a non-null argument, or throw an exception.</summary>
        /// <param name="o">The value to null-check.</param>
        /// <param name="message">The message to print in the exception if it's null.</param>
        /// <typeparam name="T">The type of object we're checking.</typeparam>
        /// <returns>The value that was passed in if it's not null.</returns>
        /// <exception cref="ArgumentNullException">If the value being checked is null.</exception>
        public static T RequireNonNull<T>(this T o, string message = "must not be null")
        {
            if (o == null)
            {
                throw new ArgumentNullException(message);
            }

            return o;
        }
    }
}
