using System.Collections.Generic;
using ImageMagick;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    public class ExifConfig
    {
        /// <summary>If ImageCaster should overwrite values if they already exist.</summary>
        public WriteMode WriteMode { get; set; }
        
        /// <summary>
        /// If ImageCaster should use it's internal default values.
        /// If set to true, you can still change values by overriding them
        /// in your configuration, or remove them entirely by setting them to null.
        /// </summary>
        public bool Defaults { get; set; }
        
        /// <summary>A list of Exif metadata to put on exported images.</summary>
        public List<TagConfig> Tags { get; set; }
    }
}