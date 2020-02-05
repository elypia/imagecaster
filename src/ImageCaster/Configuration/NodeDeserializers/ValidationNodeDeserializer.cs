using System;
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration.NodeDeserializers
{
    public class ValidationNodeDeseralizer : INodeDeserializer
    {
        private INodeDeserializer Deserializer { get; }

        public ValidationNodeDeseralizer(INodeDeserializer deserializer)
        {
            this.Deserializer = deserializer;
        }

        public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
        {
            if (Deserializer.Deserialize(parser, expectedType, nestedObjectDeserializer, out value))
            {
                ValidationContext context = new ValidationContext(value, null, null);
                Validator.ValidateObject(value, context, true);
                return true;
            }

            return false;
        }
    }
}
