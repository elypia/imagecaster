using System;
using NLog;

namespace ImageCaster
{
    class Program
    {
        /// <summary>
        /// Instance of the NLog logger for this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        static void Main(string[] args)
        {
            Logger.Trace("Hello, World!");
            Logger.Debug("Hello, World!");
            Logger.Info("Hello, World!");
            Logger.Warn("Hello, World!");
            Logger.Error("Hello, World!");
            Logger.Fatal("Hello, World!");
        }
    }
}