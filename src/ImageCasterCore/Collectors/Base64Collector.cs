using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using ImageCasterCore.Api;
using ImageMagick;
using NLog;

namespace ImageCasterCore.Collectors
{
    public class Base64Collector : ICollector
    {
        /// <summary>If there is a content-type, it'll be prefixed with this string.</summary>
        public const string DataPrefix = "data:";
        
        /// <summary>If ther is a content-type, this seperated it from the data.</summary>
        public const string Base64Seperator = "base64,";

        /// <summary>Automatically use this for unnamed files.</summary>
        private const string DefaultFileFormat = ".png";
        
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>Function to convert the data to an IMagickImage instance.</summary>
        private static readonly Func<object, MagickReadSettings, IMagickImage> ToMagickImage = (o, settings) => MagickImage.FromBase64((string)o);
        
        public List<ResolvedData> Collect(string data)
        {
            string name;
            string base64;
            
            if (data.StartsWith(DataPrefix))
            {
                string[] split = data.Split(Base64Seperator);
                string contentType = split[0].Substring(DataPrefix.Length);
                ContentType type = new ContentType(contentType);
                name = type.Name;
                base64 = split[1];
            }
            else
            {
                Logger.Warn("Base64 source with no name specified, generating a name from checksum.");
                Logger.Warn("It's recommended you prefix a content-type and name like: {0}image/png;name=myimage{1};{2}...", DataPrefix, DefaultFileFormat, Base64Seperator);
                name = GenerateName(data);
                base64 = data;
            }

            string[] tokens =
            {
                name,
                Path.GetFileNameWithoutExtension(name),
                Path.GetExtension(name)
            };
            
            List<ResolvedData> resolvedDatas = new List<ResolvedData>
            {
                new ResolvedData(base64, data, ToMagickImage, name, null, tokens)
            };
            
            return resolvedDatas;
        }

        /// <summary>
        /// Generate a file name for an the specified string.
        /// This is useful when a name is required, but nothing
        /// is available to indicate the name of the data.
        ///
        /// By using a checksum, we ensure the name generated
        /// is persistent with the same data.
        /// </summary>
        /// <param name="data">The data to generate a name for from a checksum.</param>
        /// <returns>A name for the data, a checksum appended with the <see cref="DefaultFileFormat"/>.</returns>
        public string GenerateName(string data)
        {
            using (MD5 sha = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] hash = sha.ComputeHash(bytes);
                string checksum = BitConverter.ToString(hash);
                string result = checksum.Replace("-", "");
                return result + DefaultFileFormat;
            }
        }
    }
}
