namespace ImageCaster.Configuration
{
    /// <summary>
    /// Select a pattern of files.
    /// </summary>
    public class GlobConfig
    {
        /// <summary>
        /// The name of this collection of files.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The glob pattern to match all required files.
        /// </summary>
        public string Glob { get; set; }
    }
}