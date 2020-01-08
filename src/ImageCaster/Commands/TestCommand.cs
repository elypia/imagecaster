using NLog;

namespace ImageCaster.Commands
{
    public static class TestCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Test()
        {
            Logger.Info("Executed check command through command handler.");
        }
    }
}
