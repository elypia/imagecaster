using System;
using System.IO;
using ImageCaster.Extensions;
using NLog;

namespace ImageCaster
{
    public class Failure
    {
        /// <summary>Instance of the NLog logger for this class.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The file that failed a check.</summary>
        public ResolvedFile ResolvedFile { get; }
        
        /// <summary>The message to display as a reason for the check failing.</summary>
        public string Message { get; }
        
        public Failure(ResolvedFile resolvedFile, string message)
        {
            this.ResolvedFile = resolvedFile.RequireNonNull();
            this.Message = message.RequireNonNull();
        }

        public override string ToString()
        {
            return $"Check failed for {ResolvedFile.FileInfo}: {Message}";
        }
    }
}
