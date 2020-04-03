using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Text.Json.Serialization;
using NLog;

namespace ImageCasterCore.Configuration
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
        [Required(ErrorMessage = "All archives must have a name.")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The compression level to use, this can affect speed of archiving
        /// and the final size of the archive.
        /// </summary>
        [JsonPropertyName("level")]
        public CompressionLevel Level { get; set; } = CompressionLevel.Optimal;

        /// <summary>The files to compress into the archive.</summary>
        [Required(ErrorMessage = "Must specify at least one source.")]
        [JsonPropertyName("sources")]
        public List<string> Sources { get; set; }
    }
}
