using System;
using System.IO;
using ImageCaster.Interfaces;
using NLog;

namespace ImageCaster.Checks
{
    public class MaskResolutionMatchesChecker : IChecker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public bool Check(FileInfo fileInfo)
        {
            return true;
        }

        public Failure FailureMessage(ResolvedFile resolvedFile)
        {
            throw new NotImplementedException();
        }
    }
}