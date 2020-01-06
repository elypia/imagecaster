using System.Collections.Generic;
using System.Data;
using System.Linq;
using NLog;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Wrapper of Unit enumeration to provide more information.
    /// </summary>
    public class UnitInfo
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>UnitInfo wrapper to provide additional information for Unit.Pixel.</summary>
        public static readonly UnitInfo Pixel = new UnitInfo(Unit.Pixel, "px", "pixel");
        public static readonly UnitInfo Percentage = new UnitInfo(Unit.Percentage, "%", "percent", "percentage");

        public static readonly List<UnitInfo> All = new List<UnitInfo>{
            Pixel, Percentage
        };

        /// <summary>The display unit this is wrapping around.</summary>
        public Unit Unit { get; }
        
        /// <summary>
        /// Names for this unit which can be references in the configuration.
        /// At least one name must be provided, the first alias is the primary name.
        /// </summary>
        public string[] Aliases { get; }

        internal UnitInfo(Unit unit, params string[] aliases)
        {
            this.Unit = unit;
            this.Aliases = aliases;
        }

        /// <summary>Find an unit with a unit alias matching the value provided.</summary>
        /// <param name="value">A name or respective alias of one of a known units.</param>
        /// <returns>The the UnitInfo matching the alias.</returns>
        /// <exception cref="DataException">If no unit matches the name or alias provided.</exception>
        public static UnitInfo Find(string value)
        {
            foreach (UnitInfo info in All)
            {
                if (info.Aliases.Contains(value.ToLower()))
                    return info;
            }

            throw new DataException();
        }
    }
}