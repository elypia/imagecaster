using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ImageCasterCore.Utilities;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageCasterApi.Controllers
{
    [ApiController]
    [Route("schema")]
    public class SchemaController : ControllerBase
    {
        private readonly ILogger<PreviewController> _logger;

        /// <summary>The schema for the Export configuration.</summary>
        public readonly string ExportSchema;
        
        /// <summary>The schema for the Check configuration.</summary>
        public readonly string CheckSchema;
        
        /// <summary>The schema for the achive configuration.</summary>
        public readonly string ArchiveSchema;
        
        /// <summary>The schema for the montage configuration.</summary>
        public readonly string MontageSchema;
        
        public SchemaController(ILogger<PreviewController> logger)
        {
            _logger = logger;

            IEnumerable<string> exifTagList = ExifUtils.GetNames().Select((tag) => $"\"{tag}\"");
            string exifTags = String.Join(", ", exifTagList);

            string[] filtersArray = Enum.GetNames(typeof(FilterType));
            
            IEnumerable<string> filtersList = new List<string>(filtersArray)
                .GetRange(1, filtersArray.Length - 1)
                .Select((filter) => $"\"{filter}\"");
            
            string filters = String.Join(", ", filtersList);
            
            ExportSchema = SchemaFromName("export")
                .Replace("\"${ALL_RESIZE_FILTERS}\"", exifTags)
                .Replace("\"${ALL_EXIF_TAGS}\"", filters);

            CheckSchema = SchemaFromName("check");
            ArchiveSchema = SchemaFromName("archive");
            MontageSchema = SchemaFromName("montage");
        }
        
        [HttpGet("export")]
        public IActionResult GetExportSchema()
        {
            _logger.LogTrace("Called for export schema; schema has length of {0}.", ExportSchema.Length);
            return Content(ExportSchema, MediaTypeNames.Application.Json);
        }
        
        [HttpGet("check")]
        public IActionResult GetCheckSchema()
        {
            return Content(CheckSchema, MediaTypeNames.Application.Json);
        }
        
        [HttpGet("archive")]
        public IActionResult GetArchiveSchema()
        {
            return Content(ArchiveSchema, MediaTypeNames.Application.Json);
        }
        
        [HttpGet("montage")]
        public IActionResult GetMontageSchema()
        {
            return Content(MontageSchema, MediaTypeNames.Application.Json);
        }

        /// <param name="name"></param>
        /// <returns></returns>
        private string SchemaFromName(string name)
        {
            return System.IO.File.ReadAllText($"Resources/imagecaster-{name}.schema.json");
        }
    }
}
