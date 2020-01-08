using System.IO;
using ImageCaster.Configuration;
using NLog;

namespace ImageCaster
{
    public class Failure
    {
        /// <summary>Instance of the NLog logger for this class.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The check that failed.</summary>
        public Check Check { get; }
        
        /// <summary>The file that failed a check.</summary>
        public FileInfo FileInfo { get; }
        
        /// <summary>The message to display as a reason for the check failing.</summary>
        public string Message { get; }
        
        public Failure(Check check, FileInfo fileInfo, string message)
        {
            this.Check = check;
            this.FileInfo = fileInfo;
            this.Message = message;
        }

        public override string ToString()
        {
            return $"{Check} check failed for {FileInfo}: {Message}";
        }
    }
}
