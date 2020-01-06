using System;
using ImageCaster.Configuration;
using NLog;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ImageCaster.Converters
{
    public class CheckInfoConverter : IYamlTypeConverter
    {
        /// <summary>
        /// Instance of the NLog logger for this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public bool Accepts(Type type)
        {
            return type == typeof(CheckInfo);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            Scalar scalar = parser.Consume<Scalar>();
            string value = scalar.Value;
            Logger.Debug("Found configuration value for check: {0}", value);
            CheckInfo check = CheckInfo.Find(value);
            return check;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }
}