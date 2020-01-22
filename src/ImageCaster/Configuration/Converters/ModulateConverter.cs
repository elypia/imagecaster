using System;
using System.Collections.Generic;
using ImageCaster.Configuration;
using ImageMagick;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ImageCaster.Converters
{
    public class ModulateConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(Modulate);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            parser.Consume<MappingStart>();

            List<string> setValues = new List<string>();
            Modulate modulate = new Modulate();
            
            while (parser.TryConsume(out Scalar scalar))
            {
                string key = scalar.Value;
                setValues.Add(key);
                string value = parser.Consume<Scalar>().Value;
                SetValue(ref modulate, key, value);
            }

            if (!setValues.Contains("prefix"))
                modulate.Prefix = modulate.Name.Substring(0, 1);

            if (!setValues.Contains("brightness"))
                modulate.Brightness = new Percentage(100);
            
            if (!setValues.Contains("saturation"))
                modulate.Saturation = new Percentage(100);
            
            if (!setValues.Contains("hue"))
                modulate.Hue = new Percentage(100);
            
            parser.Consume<MappingEnd>();
            return modulate;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }

        private void SetValue(ref Modulate modulate, string name, string value)
        {
            switch (name)
            {
                case "name":
                    modulate.Name = value;
                    break;
                case "prefix":
                    modulate.Prefix = value;
                    break;
                case "brightness":
                    modulate.Brightness = new Percentage(Double.Parse(value));
                    break;
                case "saturation":
                    modulate.Saturation = new Percentage(Double.Parse(value));
                    break;
                case "hue":
                    modulate.Hue = new Percentage(Double.Parse(value));
                    break;
            }
        }
    }
}