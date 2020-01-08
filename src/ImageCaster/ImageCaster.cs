using System.CommandLine;
using System.CommandLine.DragonFruit;
using System.CommandLine.Invocation;
using System.Reflection;
using System.Threading.Tasks;
using ImageCaster.Collectors;
using ImageCaster.Commands;
using ImageCaster.Configuration;
using ImageCaster.Interfaces;
using NLog;

namespace ImageCaster
{
    public class ImageCaster
    {
        /// <summary>Instance of the NLog logger for this class.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static async Task<int> Main(string[] args)
        {
            Logger.Info("Initializing ImageCaster");

            ICollector collector = new RegexCollector();
            ImageCasterConfig config = ImageCasterConfig.LoadFromFile();
            
            RootCommand command = new RootCommand("Perform aggregate tasks against a collection of images.");

            TestCommand test = new TestCommand(collector, config);
            Command testCommand = new Command("test", "Validate that the project structure and standards are maintained.");
            MethodInfo checkMethod = typeof(TestCommand).GetMethod("Test");
            testCommand.ConfigureFromMethod(checkMethod, test);
            command.AddCommand(testCommand);

            BuildCommand build = new BuildCommand(collector, config);
            Command buildCommand = new Command("build", "Export the output images from the source.");
            MethodInfo buildMethod = typeof(BuildCommand).GetMethod("Build");
            buildCommand.ConfigureFromMethod(buildMethod, build);
            command.AddCommand(buildCommand);

            Command montage = new Command("montage", "Export a single image comprised of all matching output images.");
            command.AddCommand(montage);

            Logger.Info("Finished initializing ImageCaster");
            
            return await command.InvokeAsync(args);
        }
    }
}
