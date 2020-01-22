using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using NLog;

namespace ImageCaster.Middleware
{
    /// <summary>
    /// A middleware for System.CommandLine to have a --license or -L option
    /// that shows the softwares license.
    /// </summary>
    public static class LicenseMiddleware
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static CommandLineBuilder UseLicense(this CommandLineBuilder commandLineBuilder)
        {
            commandLineBuilder.AddOption(new Option(new[] {"--license", "-L"}, "Show the license of the software")
            {
                Argument = new Argument<bool>("license", false)
            });
            
            commandLineBuilder.UseMiddleware(async (context, next) =>
            {
                Logger.Trace("Executing License Middleware.");
                CommandResult rootCommandResult = context.ParseResult.RootCommandResult;
                bool license = rootCommandResult.ValueForOption<bool>("-L");

                if (license)
                {
                    Logger.Info("GNU GPL 3+ License");
                }
                else
                {
                    await next(context);
                }
            });

            return commandLineBuilder;
        }
    }
}
