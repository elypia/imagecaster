using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.IO.Compression;
using ImageCaster.Api;
using ImageCaster.Configuration;
using ImageCaster.Extensions;
using NLog;

namespace ImageCaster.Commands
{
    public class ArchiveCommand : ICliCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>The user defined configuration to export montages of images.</summary>
        public ImageCasterConfig Config { get; }
        
        /// <param name="config"><see cref="Config"/></param>
        public ArchiveCommand(ImageCasterConfig config)
        {
            this.Config = config.RequireNonNull();
        }
        
        public Command Configure()
        {
            return new Command("archive", "Archive collections of images or files into compressed archives")
            {
                Handler = CommandHandler.Create(Execute)
            };
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
                                return (int)ExitCode.MalformedConfigFields;
                            }
                        }   
                    }
                }
            }
            
            return (int)ExitCode.Normal;
        }
    }
}
