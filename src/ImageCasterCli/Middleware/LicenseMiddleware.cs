using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using NLog;

namespace ImageCasterCli.Middleware
{
    /// <summary>
    /// A middleware for System.CommandLine to have an option that shows the softwares license.
    /// </summary>
    public static class LicenseMiddleware
    {
        private const string License = @"https://gitlab.com/Elypia/imagecaster
Copyright 2020-2020 Elypia CIC and Contributors

Licensed under the Apache License, Version 2.0 (the ""License"");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an ""AS IS"" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.";
        
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static CommandLineBuilder UseLicense(this CommandLineBuilder commandLineBuilder)
        {
            commandLineBuilder.AddOption(new Option(new[] {"--license", "-L"}, "Show the license of the software")
            {
                Argument = new Argument<bool>("license", () => false)
            });
            
            commandLineBuilder.UseMiddleware(async (context, next) =>
            {
                Logger.Trace("Executing License Middleware.");
                CommandResult rootCommandResult = context.ParseResult.RootCommandResult;
                bool license = rootCommandResult.ValueForOption<bool>("-L");

                if (license)
                {
                    Logger.Info(License);
                }
                else
                {
                    await next(context);
                }
            }, MiddlewareOrder.Configuration);

            return commandLineBuilder;
        }
    }
}
