using System.IO;
using System.IO.Compression;

namespace ImageCasterCore.Extensions
{
    /// <summary>Extension methods for the ZipArchive object.</summary>
    public static class ZipArchiveExtensions
    {
        /// <summary>Add entries from a directory.</summary>
        /// <param name="o">The zip archive to add all the entries to.</param>
        /// <param name="source">The directory to add the ZipArchive to.</param>
        /// <param name="level">The compression level to use when archiving files.</param>
        public static void CreateEntriesFromDirectory(this ZipArchive o, string source, CompressionLevel level = CompressionLevel.Optimal)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(source);

            if (!dirInfo.Exists)
            {
                throw new DirectoryNotFoundException("Source directory specified does not exist.");
            }
            
            FileInfo[] files = dirInfo.GetFiles("*", SearchOption.AllDirectories);
            
            foreach (FileInfo file in files)
            {
                o.CreateEntryFromFile(file.FullName, Path.GetRelativePath(".", file.FullName));
            }
        }
    }
}