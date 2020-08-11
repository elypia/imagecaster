using System;
using System.Collections.Generic;
using ImageCasterCore.Extensions;
using ImageMagick;

namespace ImageCasterCore
{
    /// <summary>
    /// A file that was matched and resolved from the configuration.
    /// This wraps around the <see cref="Data"/> class and appends
    /// more information to it to allow us to know how we matched it
    /// and what the tokens are for finding similar files relative to the matched file.
    ///
    /// For example, we might have a file that matches panda*.png, and require
    /// a mask for that image which matches panda$1.mask.png.
    /// </summary>
    public class ResolvedData : IComparable<ResolvedData>
    {
        /// <summary>An image or other data loaded from an external source.</summary>
        public object Data { get; }
        
        /// <summary>The name of this data.</summary>
        public string Name { get; }
        
        /// <summary>The pattern used to match this file.</summary>
        public string Pattern { get; }
        
        /// <summary>
        /// An array of tokens when matching this file.
        /// These can be accessed later in the configuration through
        /// the $1/$2/$3 syntax.
        /// </summary>
        public string[] Tokens { get; }

        /// <summary>Additional properties that may be available by the collector.</summary>
        public Dictionary<string, object> Properties { get; }

        /// <summary>How to convert this resolved data to an IMagickImage object.</summary>
        public Func<object, MagickReadSettings, MagickImage> GetMagickImage { get; }

        /// <summary>Construct an object that represents the resolution of the a file.</summary>
        /// <param name="data">The data that was matched.</param>
        /// <param name="pattern">The pattern used to match it.</param>
        /// <param name="name">The name of this file if applicable, can be null if no name could be resolved.</param>
        /// <param name="tokens">Any tokens that were inferred from the pattern.</param>
        public ResolvedData(object data, string pattern, Func<object, MagickReadSettings, MagickImage> getMagickImage, string name, Dictionary<string, object> properties, params string[] tokens)
        {
            this.Data = data.RequireNonNull();
            this.Pattern = pattern.RequireNonNull();
            this.GetMagickImage = getMagickImage.RequireNonNull();
            this.Name = name.RequireNonNull();
            this.Properties = properties;
            this.Tokens = tokens;
        }

        public MagickImage ToMagickImage(MagickReadSettings settings = null)
        {
            return GetMagickImage.Invoke(Data, settings);
        }
        
        /// <inheritdoc cref="IComparable"/>
        /// <summary>Sort the images alphabetically.</summary>
        /// <param name="other">The other resolved file to compare to.</param>
        /// <returns>A number representing if other is greater than or less than this.</returns>
        public int CompareTo(ResolvedData other)
        {
            return String.Compare(this.Data.ToString(), other.Data.ToString(), StringComparison.Ordinal);
        }
        
        public override string ToString()
        {
            string data = Data.ToString().Truncate(128);
            string pattern = Pattern.Truncate(32);
            
            string tokens = String.Join(", ", Tokens);
            return $"{data} resolved from {pattern} with tokens: {tokens}";
        }
    }
}
