using System;
using System.Collections.Generic;
using System.IO;
using ImageCasterCore.Extensions;

namespace ImageCasterCore
{
    /// <summary>
    /// A file that was matched and resolved from the configuration.
    /// This wraps around the <see cref="FileInfo"/> class and appends
    /// more information to it to allow us to know how we matched it
    /// and what the tokens are for finding similar files relative to the matched file.
    ///
    /// For example, we might have a file that matches panda*.png, and require
    /// a mask for that image which matches panda$1.mask.png.
    /// </summary>
    public class ResolvedFile : IComparable<ResolvedFile>
    {
        /// <summary>A file resolved from the configuration.</summary>
        public FileInfo FileInfo { get; }
        
        /// <summary>The pattern used to match this file.</summary>
        public string Pattern { get; }
        
        /// <summary>
        /// An array of tokens when matching this file.
        /// These can be accessed later in the configuration through
        /// the $1/$2/$3 syntax.
        /// </summary>
        public string[] Tokens { get; }

        /// <summary>Overload that takes a list of tokens instead of varargs.</summary>
        /// <param name="fileInfo">The file that was matched.</param>
        /// <param name="pattern">The pattern used to match it.</param>
        /// <param name="tokens">Any tokens that were inferred from the pattern.</param>
        public ResolvedFile(FileInfo fileInfo, string pattern, List<string> tokens) : this(fileInfo, pattern, tokens.ToArray())
        {
            // Does nothing
        }
        
        /// <summary>Construct an object that represents the resolution of the a file.</summary>
        /// <param name="fileInfo">The file that was matched.</param>
        /// <param name="pattern">The pattern used to match it.</param>
        /// <param name="tokens">Any tokens that were inferred from the pattern.</param>
        public ResolvedFile(FileInfo fileInfo, string pattern, params string[] tokens)
        {
            this.FileInfo = fileInfo.RequireNonNull();
            this.Pattern = pattern.RequireNonNull();
            this.Tokens = tokens.RequireNonNull();
        }

        /// <summary>
        /// Sort the images alphabetically.
        /// </summary>
        /// <param name="other">The other resolved file to compare to.</param>
        /// <returns>A number representing if other is greater than or less than this.</returns>
        public int CompareTo(ResolvedFile other)
        {
            return String.Compare(this.FileInfo.Name, other.FileInfo.Name, StringComparison.Ordinal);
        }
        
        public override string ToString()
        {
            string tokens = String.Join(", ", Tokens);
            return $"{FileInfo} resolved from {Pattern} with tokens: {tokens}";
        }
    }
}
