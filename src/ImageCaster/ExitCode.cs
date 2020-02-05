using System;

namespace ImageCaster
{
    /// <summary>
    /// The application may exit with a non-zero exit code if either
    /// the application considers it's run unsuccesful, for example if
    /// check failures are found, or if an error occurs at runtime,
    /// for example the user does not have write permission.
    /// </summary>
    public enum ExitCode
    {
        /// <summary>Normal end of console application.</summary>
        Normal = 0,
        
        /// <summary>An invalid log level was passed.</summary>
        InvalidLogLevel = 10,
        
        /// <summary>No configuration file found or specified.</summary>
        NoConfig = 20,
        
        /// <summary>A required field in the configuration was missing.</summary>
        MissingConfigField = 21,
        
        /// <summary>The configuration is malformed when fields are compared to other fields.</summary>
        MalformedConfigFields = 22,
        
        /// <summary>Failures occured with checks.</summary>
        CheckFailures = 30,
        
        /// <summary>Error occured during checks.</summary>
        CheckError = 31,
        
        /// <summary>Error occured when exporting images.</summary>
        BuildError = 40,

        /// <summary>Unable to read files from disk.</summary>
        CantRead = 50,
        
        /// <summary>Unable to write files to disk.</summary>
        CantWrite = 51,
        
        /// <summary>Something went seriously wrong, some kind of internal error, or unexpected state.</summary>
        InternalError = 127
    }
}