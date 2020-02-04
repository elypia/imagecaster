using System;
using System.Collections.Generic;
using ImageMagick;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration.Converters
{
    public class ExifConfigConverter : IYamlTypeConverter
    {
        private static readonly List<TagConfig> DefaultExifTags = new List<TagConfig>
        {
            new TagConfig(ExifTag.Software         , "${IMAGECASTER}"   ),
//            new TagConfig(ExifTag.ExifVersion      , "2.31"             ),
            new TagConfig(ExifTag.ImageUniqueID    , "${FILE_NAME}"     ),
            new TagConfig(ExifTag.DocumentName     , "${FILE_NAME}"     ),
            new TagConfig(ExifTag.DateTime         , "${NOW}"           ),
            new TagConfig(ExifTag.DateTimeDigitized, "${CREATION_TIME}" ),
            new TagConfig(ExifTag.DateTimeOriginal , "${CREATION_TIME}" )
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
