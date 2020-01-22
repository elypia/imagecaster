using System;
using System.Collections.Generic;
using ImageCaster.Configuration;
using ImageMagick;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ImageCaster.Converters
{
    public class ExifConfigConverter : IYamlTypeConverter
    {
        private static readonly List<TagConfig> DefaultExifTags = new List<TagConfig>
        {
            new TagConfig(ExifTag.Software.ToString()         , "${IMAGECASTER}"   ),
            new TagConfig(ExifTag.ExifVersion.ToString()      , "2.31"             ),
            new TagConfig(ExifTag.ImageUniqueID.ToString()    , "${NAME}"          ),
            new TagConfig(ExifTag.DocumentName.ToString()     , "${NAME}"          ),
            new TagConfig(ExifTag.DateTime.ToString()         , "${NOW}"           ),
            new TagConfig(ExifTag.DateTimeDigitized.ToString(), "${CREATION_TIME}" ),
            new TagConfig(ExifTag.DateTimeOriginal.ToString() , "${CREATION_TIME}" )
        }; 
        
        public bool Accepts(Type type)
        {
            return type == typeof(ExifConfig);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
