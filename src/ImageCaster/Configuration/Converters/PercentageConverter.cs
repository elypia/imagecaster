using System;
using System.IO;
using ImageMagick;
using NLog;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ImageCaster.Converters
{
    public class PercentageConverter : IYamlTypeConverter
    {
        /// <summary>
        /// Instance of the NLog logger for this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public bool Accepts(Type type)
        {
            return type == typeof(Percentage);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            Scalar scalar = parser.Consume<Scalar>();
            string value = scalar.Value;
            Logger.Trace("Found configuration value for percentage: {0}", value);
            Double.TryParse(value, out double valueNumber);
            Percentage percentage = new Percentage(valueNumber);
            return percentage;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }
}