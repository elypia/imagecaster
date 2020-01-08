using System.Collections.Generic;
using System.IO;
using ImageCaster.Configuration;

namespace ImageCaster.Interfaces
{
    /// <summary>
    /// The IChecker interface allows us to implement Checkers
    /// for each type of <see cref="Check"/>.
    /// </summary>
    public interface IChecker
    {
        /// <summary>Perform the respective check for a <see cref="Check"/>.</summary>
        /// <param name="fileInfo">The file to check against.</param>
        /// <returns>If the file adheres to the checks requirements, returns false if it fails.</returns>
        bool Check(FileInfo fileInfo);

        Failure FailureMessage(ResolvedFile resolvedFile);
    }
}