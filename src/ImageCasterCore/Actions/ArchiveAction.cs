using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
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
        
        public int Execute()
        {
            List<Archive> archives = Config.Archives;
            
            foreach (Archive archive in archives)
            {
                string archiveName = archive.Name;
                Logger.Debug("Building archive named: {0}", archiveName);
                
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
                                zip.CreateEntryFromFile(source, source);
                            }
                            else if (Directory.Exists(source))
                            {
                                zip.CreateEntriesFromDirectory(source);
                            }
                            else
                            {
                                Logger.Fatal("File or directory specified in configuration doesn't exist.");
                            }
                        }   
                    }
                }
            }
            
            return 0;
        }
    }
}
