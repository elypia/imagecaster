using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Diagnostics;
using NLog;

namespace ImageCasterCli.Middleware
{
    /// <summary>
    /// Add global arguments to the commandline for configuring the logger.
    /// </summary>
    public static class TimerMiddleware
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static CommandLineBuilder UseTimer(this CommandLineBuilder commandLineBuilder)
        {
            commandLineBuilder.AddOption(new Option(new[] {"--timer", "-T"}, "Enable the timer, and display how much time was taken")
            {
                Argument = new Argument<bool>("timer", () => false)
            });
            
            commandLineBuilder.UseMiddleware(async (context, next) =>
            {
                CommandResult rootCommandResult = context.ParseResult.RootCommandResult;

                bool timer = rootCommandResult.ValueForOption<bool>("-T");

                if (timer)
                {
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    
                    AppDomain.CurrentDomain.ProcessExit += (s, e) =>
                    {
                        Console.WriteLine("Finished running after: {0}", stopWatch.Elapsed);
                    };
                }
            
                await next(context);
            }, MiddlewareOrder.Configuration);

            return commandLineBuilder;
        }
    }
}