using System.Collections.Generic;
using System.IO;

namespace ImageCaster.Api
{
    /// <summary>
    /// Interface to determine how files are collected.
    /// Should take a string relative to the directory the
    /// application is being run and collect files from there.
    /// </summary>
    public interface ICollector
    {
        /// <summary>
        /// Collect all files that match the pattern provided.
        /// </summary>
        /// <param name="pattern">
        /// A string that represents all desired files
        /// relative to where the application was run.
        /// </param>
        /// <returns>A collection of files matching the pattern.</returns>
        List<ResolvedFile> Collect(string pattern);

        /// <summary>
        /// Find another file, relative to a resolved file.
        /// For example if a file was matched using the pattern
        /// panda*.png, and matched the file pandaAww.png,
        /// it was create a <see cref="ResolvedFile"/> representing this file
        /// with Aww as a deferenceable token which can later be accessed
        /// deeper the configuration with $1, for example panda$1.mask.png
        /// which might be where the masks are located.
        /// </summary>
        /// <param name="resolvedFile">The file that was resolved in <see cref="Collect"/></param>
        /// <param name="pattern">A pattern matching the file required."/></param>
        /// <returns>The file matching the pattern, or null if no such file exists.</returns>
        FileInfo Resolve(ResolvedFile resolvedFile, string pattern);
    }
}
