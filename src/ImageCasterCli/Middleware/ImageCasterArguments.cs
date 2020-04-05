using System.IO;

namespace ImageCasterCli.Middleware
{
    /// <summary>
    /// Global arguments for ImageCaster CLI, these are
    /// global arguments that can be specified in CLI that
    /// may directly impact how ImageCaster processed input and ouput.
    /// </summary>
    public class ImageCasterArguments
    {
        /// <summary>
        /// Inline configuration defined in CLI, these
        /// override configuration specified in the file.
        /// </summary>
        public string Config { get; set; }

        /// <summary>The file to load the configuration file from.</summary>
        public FileInfo File { get; set; }

        /// <summary></summary>
        public DirectoryInfo Input { get; set; }

        public DirectoryInfo Output { get; set; }
    }
}