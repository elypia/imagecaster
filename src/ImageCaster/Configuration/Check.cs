namespace ImageCaster.Configuration
{
    /// <summary>
    /// Valid checks that can be performed to verify the project structure
    /// is maintained and consistent.
    /// </summary>
    public enum Check
    {
        /// <summary>
        /// Check that if the source file exists, if a respective target file may exist.
        /// </summary>
        FileExists,
        
        /// <summary>
        /// Check that the resolution of the mask matches the resolution of the input.
        /// </summary>
        MaskResolutionMatches,
        
        /// <summary>
        /// Ensure that all images found matching the specified naming convention.
        /// </summary>
        NamingConvention
    }
}
