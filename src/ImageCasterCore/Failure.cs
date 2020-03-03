using ImageCasterCore.Extensions;
using NLog;

namespace ImageCasterCore
{
    public class Failure
    {
        /// <summary>Instance of the NLog logger for this class.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The file that failed a check.</summary>
        public ResolvedData ResolvedData { get; }
        
        /// <summary>The message to display as a reason for the check failing.</summary>
        public string Message { get; }
        
        public Failure(ResolvedData resolvedData, string message)
        {
            this.ResolvedData = resolvedData.RequireNonNull();
            this.Message = message.RequireNonNull();
        }

        public override string ToString()
        {
            return $"Check failed for {ResolvedData.Name}: {Message}";
        }
    }
}
