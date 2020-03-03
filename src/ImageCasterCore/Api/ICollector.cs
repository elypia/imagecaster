using System.Collections.Generic;

namespace ImageCasterCore.Api
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
        /// <param name="data">
        /// A string that represents the data to obtain
        /// or means to locate it.
        /// </param>
        /// <returns>A collection of files matching the pattern.</returns>
        List<ResolvedData> Collect(string data);
    }
}
