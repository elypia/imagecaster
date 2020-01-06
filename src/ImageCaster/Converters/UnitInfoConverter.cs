using System;
using DotNet.Globbing;
using ImageCaster.Configuration;
using NLog;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ImageCaster.Converters
{
    public class UnitInfoConverter : IYamlTypeConverter
    {
        /// <summary>Instance of the NLog logger for this class.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public bool Accepts(Type type)
        {
            return type == typeof(UnitInfo);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            Scalar scalar = parser.Consume<Scalar>();
            string value = scalar.Value;
            Logger.Debug("Found configuration value for units: {0}", value);
            UnitInfo info = UnitInfo.Find(value);
            return info;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }
}