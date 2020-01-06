using NLog;

namespace ImageCaster.Commands
{
    public static class CheckCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Check()
        {
            Logger.Info("Executed check command through command handler.");
        }
    }
}
