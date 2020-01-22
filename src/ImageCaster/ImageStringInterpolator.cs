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
    public class ImageStringInterpolator
    {
        /// <summary>A static list of all process environment variables.</summary>
        private static readonly IDictionary EnvironmentVariables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);

        /// <summary>The date format to display any information in.</summary>
        private static readonly string DateFormat = "YYYY:MM:DD hh:mm:ss";
        
        /// <summary>The source file that output is generated from.</summary>
        public FileInfo FileInfo { get; }
        
        /// <summary>The source image that output it generated from.</summary>
        public IMagickImage MagickImage { get; }
        
        /// <summary>A list of variables this interpolator can write.</summary>
        public Dictionary<string, object> Variables { get; }

        public ImageStringInterpolator(FileInfo fileInfo, IMagickImage magickImage = null)
        {
            FileInfo = fileInfo.RequireNonNull();
            MagickImage = magickImage;

            string filename = fileInfo.Name;

            Variables = new Dictionary<string, object>();

            Variables.Add("FILENAME", filename);
            Variables.Add("NAME", Path.GetFileNameWithoutExtension(filename));
            Variables.Add("DIR", fileInfo.DirectoryName);
            Variables.Add("FORMAT", fileInfo.Extension);
            Variables.Add("PATH", fileInfo.FullName);
            Variables.Add("CREATION_TIME", fileInfo.CreationTimeUtc.ToString(DateFormat));
            Variables.Add("NOW", DateTime.UtcNow.ToString(DateFormat));

            if (magickImage != null)
            {
                Variables.Add("COMMENT", magickImage.Comment);
                Variables.Add("HEIGHT", magickImage.Height);
                Variables.Add("WIDTH", magickImage.Width);
                Variables.Add("DELAY", magickImage.AnimationDelay);
                Variables.Add("LOOPS", magickImage.AnimationIterations);
            }

            foreach (KeyValuePair<string, object> entry in EnvironmentVariables)
            {
                Variables.Add(entry.Key, entry.Value);
            }
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

            foreach (KeyValuePair<string, object> entry in Variables)
            {
                result = result.Replace(entry.Key, entry.Value.ToString());
            }

            return result;
        }
    }
}