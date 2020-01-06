using NLog;

namespace ImageCaster.Commands
{
    public static class BuildCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Build()
        {
            Logger.Info("Executed build command through command handler.");
        }
    }
}