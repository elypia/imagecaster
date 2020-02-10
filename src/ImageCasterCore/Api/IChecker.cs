using System.Collections.Generic;

namespace ImageCasterCore.Api
{
    /// <summary>
    /// The IChecker interface allows us to implement Checkers
    /// for each type of <see cref="Check"/>.
    /// </summary>
    public interface IChecker
    {
        /// <summary>Perform the respective check for a <see cref="Check"/>.</summary>
        /// <returns>A list of failures that occured while checking.</returns>
        List<Failure> Check();
    }
}
