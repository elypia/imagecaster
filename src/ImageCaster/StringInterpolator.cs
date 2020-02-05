using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ImageCaster.Configuration;
using ImageCaster.Extensions;
using ImageMagick;

namespace ImageCaster
{
    /// <summary>
    /// Interpolator to expand variables used in strings.
    /// This is helpful, for example when configuring the <see cref="TagConfig"/>.
    ///
    /// This will take key/value pairs from the <see cref="FileInfo"/>, <see cref="IMagickImage"/>
    /// and process environment variables.
    /// </summary>
    public class StringInterpolator
    {
        /// <summary>A static list of all process environment variables.</summary>
        private static readonly IDictionary EnvironmentVariables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);

        /// <summary>The date format to display any information in.</summary>
        private const string DateFormat = "yyyy:MM:dd hh:mm:ss";

        /// <summary>A list of variables this interpolator can write.</summary>
        public Dictionary<string, object> Variables { get; }
        
        /// <summary>
        /// A dictionary of variables this interpolator can write, mapped
        /// to functions that produce the value dynamically.
        /// </summary>
        public Dictionary<string, Func<object>> DynamicVariables { get; }

        public StringInterpolator(FileInfo fileInfo, IMagickImage magickImage = null)
        {
            Variables = new Dictionary<string, object>();
            DynamicVariables = new Dictionary<string, Func<object>>();
            
            string filename = fileInfo.Name;
            
            Variables.Add("IMAGECASTER", "ImageCaster " + typeof(ImageCaster).Assembly.GetName().Version.ToString(3));
            
            Variables.Add("FILE_FULL_NAME", filename);
            Variables.Add("FILE_NAME", Path.GetFileNameWithoutExtension(filename));
            Variables.Add("PARENT_DIR", fileInfo.DirectoryName);
            Variables.Add("FILE_FORMAT", fileInfo.Extension);
            Variables.Add("FILE_PATH", fileInfo.FullName);
            Variables.Add("CREATION_TIME", fileInfo.CreationTimeUtc.ToString(DateFormat));

            if (magickImage != null)
            {
                Variables.Add("COMMENT", magickImage.Comment);
                Variables.Add("HEIGHT", magickImage.Height);
                Variables.Add("WIDTH", magickImage.Width);
                Variables.Add("DELAY", magickImage.AnimationDelay);
                Variables.Add("LOOPS", magickImage.AnimationIterations);
            }
            
            foreach (DictionaryEntry entry in EnvironmentVariables)
            {
                Variables.Add(entry.Key.ToString(), entry.Value);
            }
            
            DynamicVariables.Add("NOW", () => DateTime.UtcNow.ToString(DateFormat));
        }

        /// <summary>
        /// Replace any use of variables, defined by the ${...} format,
        /// with the associated variables that were collected when this
        /// object was instantiated.
        /// </summary>
        /// <param name="source">The string to replace variables in.</param>
        /// <returns>The string with all known variables found.</returns>
        public string Interpolate(string source)
        {
            string result = source;

            Dictionary<string, object> variables = new Dictionary<string, object>(Variables);
            
            foreach ((string key, Func<object> value) in DynamicVariables)
            {
                variables.Add(key, value.Invoke());
            }
            
            foreach ((string key, object value) in variables)
            {
                if (value != null)
                {
                    result = result.Replace("${" + key + "}", value.ToString());
                }
            }

            return result;
        }
    }
}
