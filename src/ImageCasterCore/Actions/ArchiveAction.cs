using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Exceptions;
using ImageCasterCore.Extensions;
using NLog;

namespace ImageCasterCore.Actions
{
    public class ArchiveAction : IAction
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>The user defined configuration to export montages of images.</summary>
        public ImageCasterConfig Config { get; }
        
        /// <param name="config"><see cref="Config"/></param>
        public ArchiveAction(ImageCasterConfig config)
        {
            this.Config = config.RequireNonNull();
        }
        
        public void Execute()
        {
            List<Archive> archives = Config.Archives;

            if (archives == null || archives.Count == 0)
            {
                throw new ConfigurationException("Archive action performed, but no archives are defined in the configuration.");
            }
            
            foreach (Archive archive in archives)
            {
                string archiveName = archive.Name;
                Logger.Debug("Creating archive named: {0}", archiveName);

                CompressionLevel level = archive.Level;
                
                string build = Path.Combine("build", "archives", archiveName + ".zip");
                FileInfo fileInfo = new FileInfo(build);
                fileInfo.Directory?.Create();
                
                using (FileStream stream = new FileStream(build, FileMode.Create, FileAccess.Write))
                {
                    using (ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Create))
                    {
                        foreach (string source in archive.Sources)
                        {
                            Logger.Debug("Adding source to zip archive: {0}", source);
                            
                            if (File.Exists(source))
                            {
                                zip.CreateEntryFromFile(source, source, level);
                            }
                            else if (Directory.Exists(source))
                            {
                                zip.CreateEntriesFromDirectory(source, level);
                            }
                            else
                            {
                                throw new FileNotFoundException("File or directory specified in configuration doesn't exist.");
                            }
                        }   
                    }
                }
            }
        }
    }
}
