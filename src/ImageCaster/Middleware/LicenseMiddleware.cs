using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using NLog;

namespace ImageCaster.Middleware
{
    /// <summary>
    /// A middleware for System.CommandLine to have an option that shows the softwares license.
    /// </summary>
    public static class LicenseMiddleware
    {
        private const string License = @"https://gitlab.com/Elypia/imagecaster
ImageCaster - Declaratively configure and build image repositories.
Copyright (C) 2020-2020  Elypia CIC

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.";
        
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
                    Logger.Info(License);
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
