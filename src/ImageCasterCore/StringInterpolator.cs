using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ImageCasterCore.Configuration;
using ImageMagick;

namespace ImageCasterCore
{
    /// <summary>
    /// Interpolator to expand variables used in strings.
    /// This is helpful, for example when configuring the <see cref="TagConfig{T}"/>.
    ///
    /// This will take key/value pairs from the <see cref="FileInfo"/>, <see cref="IMagickImage"/>
    /// and process environment variables.
    /// </summary>
    public class StringInterpolator
    {
        
        private const string DateFormat = "yyyy:MM:dd";
     
        /// <summary>The default format for specifying the time.</summary>
        private const string TimeFormat = "hh:mm:ss";
        
        /// <summary>The date format to display any information in.</summary>
        private const string DateTimeFormat = DateFormat + " " + TimeFormat;

        /// <summary>The `Name Major.Minor.Patch version string of ImageCaster.</summary>
        private static readonly string Application = "ImageCaster " + typeof(StringInterpolator).Assembly.GetName().Version.ToString(3);
        
        /// <summary>A static list of all process environment variables.</summary>
        private static readonly IDictionary EnvironmentVariables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
        
        /// <summary>A list of variables this interpolator can write.</summary>
        private Dictionary<string, object> Variables { get; }

        public StringInterpolator()
        {
            Variables = new Dictionary<string, object>();
            
            foreach (DictionaryEntry entry in EnvironmentVariables)
            {
                Variables.Add(entry.Key.ToString(), entry.Value);
            }
            
            Variables.Add("IMAGECASTER", Application);
            
            DateTime date = DateTime.UtcNow;
            
            Variables.Add("NOW", date.ToString(DateTimeFormat));
            Variables.Add("NOW_TIME", date.ToString(TimeFormat));
            Variables.Add("NOW_DATE", date.ToString(DateFormat));
        }
        
        public StringInterpolator(StringInterpolator interpolator, ResolvedData resolvedData = null, IMagickImage magickImage = null)
        {
            if (interpolator == null)
            {
                interpolator = new StringInterpolator();
            }
            
            Variables = new Dictionary<string, object>(interpolator.Variables);

            if (resolvedData != null)
            {
                string filename = resolvedData.Name;

                Variables.Add("DATA_FULL_NAME", filename);
                Variables.Add("DATA_NAME", Path.GetFileNameWithoutExtension(filename));
                Variables.Add("DATA_EXT", Path.GetExtension(filename));
            }
 
            if (magickImage != null)
            {
                Variables.Add("COMMENT", magickImage.Comment);
                Variables.Add("HEIGHT", magickImage.Height);
                Variables.Add("WIDTH", magickImage.Width);
                Variables.Add("DELAY", magickImage.AnimationDelay);
                Variables.Add("LOOPS", magickImage.AnimationIterations);
            }
        }

        /// <summary>
        /// Add all key/value pairs in the dictionary to the interpolator,
        /// replacing values if required.
        /// </summary>
        /// <param name="dictionary">The key/value pairs to add.</param>
        public void Add(Dictionary<string, object> dictionary)
        {
            foreach ((string key, object value) in dictionary)
            {
                Variables.Add(key, value);
            }
        }

        /// <summary>Add the specified key and value pair to the interpolator.</summary>
        /// <param name="key">The key to use to refer to this value.</param>
        /// <param name="value">The value to interpolate when referenced.</param>
        public void Add(string key, object value)
        {
            Variables.Add(key, value);
        }

        /// <param name="resolvedData">The resolved data to add properties from.</param>
        /// <param name="magickImage">The magick image to add properties from.</param>
        /// <returns>
        /// A new interpolator branching from the original one with
        /// the former variable pool, plus the ones added from the parameters.
        /// </returns>
        public StringInterpolator Branch(ResolvedData resolvedData, IMagickImage magickImage)
        {
            return new StringInterpolator(this, resolvedData, magickImage);
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
            foreach ((string key, object value) in Variables)
            {
                if (value != null)
                {
                    source = source.Replace("${" + key + "}", value.ToString());
                }
            }

            return source;
        }
    }
}
