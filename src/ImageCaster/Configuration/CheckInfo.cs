using System.Collections.Generic;
using System.Data;
using NLog;

namespace ImageCaster.Configuration
{
    /// <summary>Wrapper around Check enum to define additional data.</summary>
    public class CheckInfo
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static readonly CheckInfo FileExists = new CheckInfo(Check.FileExists, false, "file-exists");

        public static readonly CheckInfo MaskResolutionMatches = new CheckInfo(Check.MaskResolutionMatches, true, "mask-resolution-matches");

        public static readonly List<CheckInfo> All = new List<CheckInfo>{
            FileExists, MaskResolutionMatches
        };

        /// <summary>The Check (enum) value this is additional information for.</summary>
        public Check Check { get; }
        
        /// <summary>If multiple of this check can be defined in the configuration.</summary>
        public bool Multi { get; }
        
        /// <summary>The configuration name of the check.</summary>
        public string Alias { get; }

        /// <summary>
        /// Create an instance of CheckInfo.
        /// Checks can only be created internally.
        /// </summary>
        /// <param name="check">The check this defined information for.</param>
        /// <param name="multi">If this check can be defined multiple times.</param>
        /// <param name="alias">The configuration name of this check.</param>
        internal CheckInfo(Check check, bool multi, string alias)
        {
            this.Check = check;
            this.Multi = multi;
            this.Alias = alias;
        }

        /// <summary>Static method to find CheckInfo from the check alias.</summary>
        /// <param name="value">The alias of the check method to find.</param>
        /// <returns>The CheckInfo for the specified check.</returns>
        /// <exception cref="DataException">If the check doesn't exist.</exception>
        public static CheckInfo Find(string value)
        {
            foreach (CheckInfo info in All)
            {
                if (info.Alias == value.ToLower())
                    return info;
            }

            throw new DataException();
        }
    }
}