namespace ImageCaster.Configuration
{
    /// <summary>
    /// The write mode determines how ImageCaster will write new metadata
    /// to the images as they export.
    /// </summary>
    public enum WriteMode
    {
        /// <summary>Remove all existing tags before writing.</summary>
        Truncate,
        
        /// <summary>Overwrite any existing tags is they already exist.</summary>
        Overwrite,
        
        /// <summary>Don't write tags if they already exist.</summary>
        Annul,
        
        /// <summary>Throw an error if tag already exists occurs.</summary>
        Error
    }
}
