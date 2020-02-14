using System;
using ImageCasterCore.Utilities;
using ImageMagick;
using NLog;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ImageCasterCore.Configuration.Converters
{
    public class ExifTagConverter : IYamlTypeConverter
    {
        /// <summary>
        /// Instance of the NLog logger for this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public bool Accepts(Type type)
        {
            return type == typeof(ExifTag);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            Scalar scalar = parser.Consume<Scalar>();
            string value = scalar.Value;
            ExifTag tag = ExifUtils.FindByName(value);
            return tag;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }
}