using System.Collections.Generic;
using NLog;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// A configuration to name and collate files into an archive.
    /// This can be useful for when certain collections of images need
    /// to be grouped together and distributed into certain packages.
    ///
    /// It allows for any files to be specified so you can include special
    /// files like a LICENSE or NOTICE in the artifact.
    /// </summary>
    public class Archive
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The name of this archive.</summary>
        public string Name { get; set; }
        
        /// <summary>The files to compress into the archive.</summary>
        public List<string> Sources { get; set; }
    }
}