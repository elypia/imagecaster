using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using ImageCasterApi.Json;
using ImageCasterCore.Utilities;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageCasterApi.Controllers
{
    /// <summary>
    /// This controllers returns information that could help a frontend
    /// understand what data ImageCaster expectes, or fulfil any
    /// data contracts that might help make forms stricter.
    /// </summary>
    [ApiController]
    [Route("schema")]
    public class SchemaController : ControllerBase
    {
        /// <summary>The relative location from the executable that the resources live.</summary>
        private static readonly string RelativeSchemaLocation = Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "imagecaster.schema.json";

        /// <summary>The directory the executable resides in.</summary>
        private static readonly string ExecutableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        /// <summary>An absolute path to the schema resource.</summary>
        private static readonly string SchemaLocation = ExecutableDirectory + RelativeSchemaLocation;

        /// <summary>An array of all available fonts on the system.</summary>
        private static readonly FontFamily[] FontFamilies = FontFamily.Families;
        
        private readonly ILogger<PreviewController> _logger;

        /// <summary>A list of all exif tags available.</summary>
        private readonly List<string> _exifTags;

        /// <summary>A list of all IPTC tags available.</summary>
        private readonly List<string> _iptcTags;
        
        /// <summary>A list of all resize filters available.</summary>
        private readonly List<string> _resizeFilters;

        private readonly List<string> _fontFamilies;
        
        /// <summary>The schema for the ImageCaster configuration.</summary>
        private readonly JsonSchema _schema;

        public SchemaController(ILogger<PreviewController> logger)
        {
            _logger = logger;

            _exifTags = ExifUtils.GetNames().ToList();
            _exifTags.Sort();
            
            string[] iptcTags = Enum.GetNames(typeof(IptcTag));
            _iptcTags = new List<string>(iptcTags);
            _iptcTags.Remove("Unknown");
            _iptcTags.Sort();
            
            string[] filtersArray = Enum.GetNames(typeof(FilterType));
            _resizeFilters = new List<string>(filtersArray).ToList();
            _resizeFilters.Remove("Undefined");
            _resizeFilters.Sort();
            _resizeFilters.Insert(0, "Default");

            _fontFamilies = FontFamilies.Select((family) => family.Name).Distinct().ToList();
            _fontFamilies.Sort();
            _fontFamilies.Insert(0, "Default");
            
            _logger.LogInformation("Loading JSON Schema from: {0}", SchemaLocation);
            string schemaTemplate = System.IO.File.ReadAllText(SchemaLocation);

            _schema = JsonSerializer.Deserialize<JsonSchema>(schemaTemplate);
            
            Dictionary<string, JsonSchemaProperty> buildProperties = _schema.Properties["build"].Properties;
            buildProperties["resize"].Properties["filter"].Enum = _resizeFilters;

            JsonSchemaProperty metadataProperties = buildProperties["metadata"];
            metadataProperties.Properties["exif"].Items.Properties["tag"].Enum = _exifTags;
            metadataProperties.Properties["iptc"].Items.Properties["tag"].Enum = _iptcTags;

            Dictionary<string, JsonSchemaProperty> montageProperties = _schema.Properties["montages"].Items.Properties;
            montageProperties["font-family"].Enum = _fontFamilies;
        }

        /// <summary>
        /// Receive the JSON Schema for the ImageCaster configuration, or
        /// a partial schema from part including one or more of the properties.
        /// </summary>
        /// <param name="properties">The properties to obtain.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpGet("json-schema")]
        public ActionResult<JsonSchema> GetSchema([FromQuery] string[] properties)
        {
            JsonSchema response;
            
            if (properties.Length == 0)
            {
                response = _schema;
            }
            else
            {
                response = new JsonSchema()
                {
                    Schema = _schema.Schema,
                    Id = _schema.Id,
                    Title = _schema.Title,
                    Description = _schema.Description,
                    Type = _schema.Type,
                    Properties = new Dictionary<string, JsonSchemaProperty>()
                };

                Dictionary<string, JsonSchemaProperty> jsonProperties = _schema.Properties;

                foreach (string property in properties)
                {
                    if (!jsonProperties.ContainsKey(property))
                    {
                        return StatusCode(404, "Property not found.");
                    }
                    
                    response.Properties.Add(property, jsonProperties[property]);
                }
            }

            return response;
        }

        /// <returns>All exif tags values available in ImageCaster.</returns>
        [HttpGet("exif-tags")]
        public ActionResult<List<string>> ExifTags()
        {
            return _exifTags;
        }

        /// <returns>All IPTC tags in ImageCaster.</returns>
        [HttpGet("iptc-tags")]
        public ActionResult<List<string>> IptcTags()
        {
            return _iptcTags;
        }
        
        /// <returns>All resize filters available in ImageCaster.</returns>
        [HttpGet("resize-filters")]
        public ActionResult<List<string>> ResizeFilters()
        {
            return _resizeFilters;
        }

        /// <returns>All fonts available on the server.</returns>
        [HttpGet("fonts")]
        public ActionResult<List<string>> ServerFonts()
        {
            return _fontFamilies;
        }
    }
}
