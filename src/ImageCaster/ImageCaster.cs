using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.DragonFruit;
using System.CommandLine.Invocation;
using System.Reflection;
using System.Threading.Tasks;
using ImageCaster.Commands;
using ImageCaster.Configuration;
using NLog;
using YamlDotNet.Serialization;

namespace ImageCaster
{
    public class ImageCaster
    {
        /// <summary>
        /// Instance of the NLog logger for this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static async Task<int> Main(string[] args)
        {
            Logger.Info("Initializing ImageCaster");
            RootCommand command = new RootCommand("Perform aggregate tasks against a collection of images.");
            
            Command check = new Command("check", "Validate that the project structure and standards are maintained.");
            MethodInfo checkMethod = typeof(CheckCommand).GetMethod("Check");
            check.ConfigureFromMethod(checkMethod);
            command.AddCommand(check);
            
            Command build = new Command("build", "Export the output images from the source.");
            MethodInfo buildMethod = typeof(BuildCommand).GetMethod("Build");
            build.ConfigureFromMethod(buildMethod);
            command.AddCommand(build);

            Command montage = new Command("montage", "Export a single image comprised of all matching output images.");
            command.AddCommand(montage);

            Logger.Info("Finished initializing ImageCaster");
            
            return await command.InvokeAsync(args);
        }
    }
}
