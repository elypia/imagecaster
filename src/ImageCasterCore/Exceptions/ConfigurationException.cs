using System;

namespace ImageCasterCore.Exceptions
{
    /// <summary>
    /// Exception for when the configuration is invalid and prevents
    /// ImageCaster from continuing or is most likely user error.
    /// </summary>
    [Serializable]
    public class ConfigurationException : Exception
    {
        public ConfigurationException() : base()
        {
            
        }

        public ConfigurationException(string message) : base(message)
        {
            
        }

        public ConfigurationException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}
