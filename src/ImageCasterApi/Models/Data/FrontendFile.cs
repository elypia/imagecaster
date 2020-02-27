using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ImageCasterApi.Models.Data
{
    /// <summary>
    /// A model representing the data received from the from a frontend
    /// when converting to a string and sent over.
    /// </summary>
    public class FrontendFile
    {
        /// <summary>The MIME type of the file if present, else null.</summary>
        public ContentType ContentType { get; set; }

        /// <summary>The bytes representing the actual data.</summary>
        [Required]
        public byte[] Data { get; set; }

        public FrontendFile()
        {
            // Do nothing
        }

        public FrontendFile(byte[] data)
        {
            Data = data;
        }
        
        public FrontendFile(ContentType contentType, byte[] data)
        {
            ContentType = contentType;
            Data = data;
        }
    }
}
