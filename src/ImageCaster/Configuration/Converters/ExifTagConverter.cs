using System;
using ImageMagick;
using NLog;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration.Converters
{
    public class ExifTagConverter : IYamlTypeConverter
    {
        /// <summary>
        /// Instance of the NLog logger for this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public bool Accepts(Type type)
        {
            return type == typeof(ExifTag);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            Scalar scalar = parser.Consume<Scalar>();
            string value = scalar.Value;
            ExifTag tag = FindByTag(value);
            return tag;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Switch case made from taking all available EXIF tags in Magick.NET
        /// and making a switch mapping input to them.
        /// </summary>
        /// <param name="tag">The Exif tag as a string literal.</param>
        /// <returns>The Exif tag specified by the user if found.</returns>
        /// <exception cref="ArgumentException">If the Exif tag specified doesn't exist.</exception>
        private ExifTag FindByTag(string tag)
        {
            if (String.IsNullOrWhiteSpace(tag))
            {
                return null;
            }
            
            switch (tag.ToLower())
            {
                case "faxprofile": return ExifTag.FaxProfile;
                case "modenumber": return ExifTag.ModeNumber;
                case "gpsaltituderef": return ExifTag.GPSAltitudeRef;
                case "clippath": return ExifTag.ClipPath;
                case "versionyear": return ExifTag.VersionYear;
                case "xmp": return ExifTag.XMP;
                case "cfapattern2": return ExifTag.CFAPattern2;
                case "tiffepstandardid": return ExifTag.TIFFEPStandardID;
                case "xptitle": return ExifTag.XPTitle;
                case "xpcomment": return ExifTag.XPComment;
                case "xpauthor": return ExifTag.XPAuthor;
                case "xpkeywords": return ExifTag.XPKeywords;
                case "xpsubject": return ExifTag.XPSubject;
                case "gpsversionid": return ExifTag.GPSVersionID;
                case "pixelscale": return ExifTag.PixelScale;
                case "intergraphmatrix": return ExifTag.IntergraphMatrix;
                case "modeltiepoint": return ExifTag.ModelTiePoint;
                case "modeltransform": return ExifTag.ModelTransform;
                case "subfiletype": return ExifTag.SubfileType;
                case "subifdoffset": return ExifTag.SubIFDOffset;
                case "gpsifdoffset": return ExifTag.GPSIFDOffset;
                case "t4options": return ExifTag.T4Options;
                case "t6options": return ExifTag.T6Options;
                case "xclippathunits": return ExifTag.XClipPathUnits;
                case "yclippathunits": return ExifTag.YClipPathUnits;
                case "profiletype": return ExifTag.ProfileType;
                case "codingmethods": return ExifTag.CodingMethods;
                case "t82ptions": return ExifTag.T82ptions;
                case "jpeginterchangeformat": return ExifTag.JPEGInterchangeFormat;
                case "jpeginterchangeformatlength": return ExifTag.JPEGInterchangeFormatLength;
                case "mdfiletag": return ExifTag.MDFileTag;
                case "standardoutputsensitivity": return ExifTag.StandardOutputSensitivity;
                case "recommendedexposureindex": return ExifTag.RecommendedExposureIndex;
                case "isospeed": return ExifTag.ISOSpeed;
                case "isospeedlatitudeyyy": return ExifTag.ISOSpeedLatitudeyyy;
                case "isospeedlatitudezzz": return ExifTag.ISOSpeedLatitudezzz;
                case "faxrecvparams": return ExifTag.FaxRecvParams;
                case "faxrecvtime": return ExifTag.FaxRecvTime;
                case "imagenumber": return ExifTag.ImageNumber;
                case "freeoffsets": return ExifTag.FreeOffsets;
                case "freebytecounts": return ExifTag.FreeByteCounts;
                case "colorresponseunit": return ExifTag.ColorResponseUnit;
                case "tileoffsets": return ExifTag.TileOffsets;
                case "sminsamplevalue": return ExifTag.SMinSampleValue;
                case "smaxsamplevalue": return ExifTag.SMaxSampleValue;
                case "jpegqtables": return ExifTag.JPEGQTables;
                case "jpegdctables": return ExifTag.JPEGDCTables;
                case "jpegactables": return ExifTag.JPEGACTables;
                case "striprowcounts": return ExifTag.StripRowCounts;
                case "intergraphregisters": return ExifTag.IntergraphRegisters;
                case "timezoneoffset": return ExifTag.TimeZoneOffset;
                case "imagewidth": return ExifTag.ImageWidth;
                case "imagelength": return ExifTag.ImageLength;
                case "tilewidth": return ExifTag.TileWidth;
                case "tilelength": return ExifTag.TileLength;
                case "badfaxlines": return ExifTag.BadFaxLines;
                case "consecutivebadfaxlines": return ExifTag.ConsecutiveBadFaxLines;
                case "pixelxdimension": return ExifTag.PixelXDimension;
                case "pixelydimension": return ExifTag.PixelYDimension;
                case "stripoffsets": return ExifTag.StripOffsets;
                case "tilebytecounts": return ExifTag.TileByteCounts;
                case "imagelayer": return ExifTag.ImageLayer;
                case "xposition": return ExifTag.XPosition;
                case "yposition": return ExifTag.YPosition;
                case "xresolution": return ExifTag.XResolution;
                case "yresolution": return ExifTag.YResolution;
                case "batterylevel": return ExifTag.BatteryLevel;
                case "exposuretime": return ExifTag.ExposureTime;
                case "fnumber": return ExifTag.FNumber;
                case "mdscalepixel": return ExifTag.MDScalePixel;
                case "compressedbitsperpixel": return ExifTag.CompressedBitsPerPixel;
                case "aperturevalue": return ExifTag.ApertureValue;
                case "maxaperturevalue": return ExifTag.MaxApertureValue;
                case "subjectdistance": return ExifTag.SubjectDistance;
                case "focallength": return ExifTag.FocalLength;
                case "flashenergy2": return ExifTag.FlashEnergy2;
                case "focalplanexresolution2": return ExifTag.FocalPlaneXResolution2;
                case "focalplaneyresolution2": return ExifTag.FocalPlaneYResolution2;
                case "exposureindex2": return ExifTag.ExposureIndex2;
                case "humidity": return ExifTag.Humidity;
                case "pressure": return ExifTag.Pressure;
                case "acceleration": return ExifTag.Acceleration;
                case "flashenergy": return ExifTag.FlashEnergy;
                case "focalplanexresolution": return ExifTag.FocalPlaneXResolution;
                case "focalplaneyresolution": return ExifTag.FocalPlaneYResolution;
                case "exposureindex": return ExifTag.ExposureIndex;
                case "digitalzoomratio": return ExifTag.DigitalZoomRatio;
                case "gpsaltitude": return ExifTag.GPSAltitude;
                case "gpsdop": return ExifTag.GPSDOP;
                case "gpsspeed": return ExifTag.GPSSpeed;
                case "gpstrack": return ExifTag.GPSTrack;
                case "gpsimgdirection": return ExifTag.GPSImgDirection;
                case "gpsdestbearing": return ExifTag.GPSDestBearing;
                case "gpsdestdistance": return ExifTag.GPSDestDistance;
                case "whitepoint": return ExifTag.WhitePoint;
                case "primarychromaticities": return ExifTag.PrimaryChromaticities;
                case "ycbcrcoefficients": return ExifTag.YCbCrCoefficients;
                case "referenceblackwhite": return ExifTag.ReferenceBlackWhite;
                case "gpslatitude": return ExifTag.GPSLatitude;
                case "gpslongitude": return ExifTag.GPSLongitude;
                case "gpstimestamp": return ExifTag.GPSTimestamp;
                case "gpsdestlatitude": return ExifTag.GPSDestLatitude;
                case "gpsdestlongitude": return ExifTag.GPSDestLongitude;
                case "lensinfo": return ExifTag.LensInfo;
                case "oldsubfiletype": return ExifTag.OldSubfileType;
                case "compression": return ExifTag.Compression;
                case "photometricinterpretation": return ExifTag.PhotometricInterpretation;
                case "thresholding": return ExifTag.Thresholding;
                case "cellwidth": return ExifTag.CellWidth;
                case "celllength": return ExifTag.CellLength;
                case "fillorder": return ExifTag.FillOrder;
                case "orientation": return ExifTag.Orientation;
                case "samplesperpixel": return ExifTag.SamplesPerPixel;
                case "planarconfiguration": return ExifTag.PlanarConfiguration;
                case "grayresponseunit": return ExifTag.GrayResponseUnit;
                case "resolutionunit": return ExifTag.ResolutionUnit;
                case "cleanfaxdata": return ExifTag.CleanFaxData;
                case "inkset": return ExifTag.InkSet;
                case "numberofinks": return ExifTag.NumberOfInks;
                case "dotrange": return ExifTag.DotRange;
                case "indexed": return ExifTag.Indexed;
                case "opiproxy": return ExifTag.OPIProxy;
                case "jpegproc": return ExifTag.JPEGProc;
                case "jpegrestartinterval": return ExifTag.JPEGRestartInterval;
                case "ycbcrpositioning": return ExifTag.YCbCrPositioning;
                case "rating": return ExifTag.Rating;
                case "ratingpercent": return ExifTag.RatingPercent;
                case "exposureprogram": return ExifTag.ExposureProgram;
                case "interlace": return ExifTag.Interlace;
                case "selftimermode": return ExifTag.SelfTimerMode;
                case "sensitivitytype": return ExifTag.SensitivityType;
                case "meteringmode": return ExifTag.MeteringMode;
                case "lightsource": return ExifTag.LightSource;
                case "focalplaneresolutionunit2": return ExifTag.FocalPlaneResolutionUnit2;
                case "sensingmethod2": return ExifTag.SensingMethod2;
                case "flash": return ExifTag.Flash;
                case "colorspace": return ExifTag.ColorSpace;
                case "focalplaneresolutionunit": return ExifTag.FocalPlaneResolutionUnit;
                case "sensingmethod": return ExifTag.SensingMethod;
                case "customrendered": return ExifTag.CustomRendered;
                case "exposuremode": return ExifTag.ExposureMode;
                case "whitebalance": return ExifTag.WhiteBalance;
                case "focallengthin35mmfilm": return ExifTag.FocalLengthIn35mmFilm;
                case "scenecapturetype": return ExifTag.SceneCaptureType;
                case "gaincontrol": return ExifTag.GainControl;
                case "contrast": return ExifTag.Contrast;
                case "saturation": return ExifTag.Saturation;
                case "sharpness": return ExifTag.Sharpness;
                case "subjectdistancerange": return ExifTag.SubjectDistanceRange;
                case "gpsdifferential": return ExifTag.GPSDifferential;
                case "bitspersample": return ExifTag.BitsPerSample;
                case "minsamplevalue": return ExifTag.MinSampleValue;
                case "maxsamplevalue": return ExifTag.MaxSampleValue;
                case "grayresponsecurve": return ExifTag.GrayResponseCurve;
                case "colormap": return ExifTag.ColorMap;
                case "extrasamples": return ExifTag.ExtraSamples;
                case "pagenumber": return ExifTag.PageNumber;
                case "transferfunction": return ExifTag.TransferFunction;
                case "predictor": return ExifTag.Predictor;
                case "halftonehints": return ExifTag.HalftoneHints;
                case "sampleformat": return ExifTag.SampleFormat;
                case "transferrange": return ExifTag.TransferRange;
                case "defaultimagecolor": return ExifTag.DefaultImageColor;
                case "jpeglosslesspredictors": return ExifTag.JPEGLosslessPredictors;
                case "jpegpointtransforms": return ExifTag.JPEGPointTransforms;
                case "ycbcrsubsampling": return ExifTag.YCbCrSubsampling;
                case "cfarepeatpatterndim": return ExifTag.CFARepeatPatternDim;
                case "intergraphpacketdata": return ExifTag.IntergraphPacketData;
                case "isospeedratings": return ExifTag.ISOSpeedRatings;
                case "subjectarea": return ExifTag.SubjectArea;
                case "subjectlocation": return ExifTag.SubjectLocation;
                case "shutterspeedvalue": return ExifTag.ShutterSpeedValue;
                case "brightnessvalue": return ExifTag.BrightnessValue;
                case "exposurebiasvalue": return ExifTag.ExposureBiasValue;
                case "ambienttemperature": return ExifTag.AmbientTemperature;
                case "waterdepth": return ExifTag.WaterDepth;
                case "cameraelevationangle": return ExifTag.CameraElevationAngle;
                case "decode": return ExifTag.Decode;
                case "imagedescription": return ExifTag.ImageDescription;
                case "make": return ExifTag.Make;
                case "model": return ExifTag.Model;
                case "software": return ExifTag.Software;
                case "datetime": return ExifTag.DateTime;
                case "artist": return ExifTag.Artist;
                case "hostcomputer": return ExifTag.HostComputer;
                case "copyright": return ExifTag.Copyright;
                case "documentname": return ExifTag.DocumentName;
                case "pagename": return ExifTag.PageName;
                case "inknames": return ExifTag.InkNames;
                case "targetprinter": return ExifTag.TargetPrinter;
                case "imageid": return ExifTag.ImageID;
                case "mdlabname": return ExifTag.MDLabName;
                case "mdsampleinfo": return ExifTag.MDSampleInfo;
                case "mdprepdate": return ExifTag.MDPrepDate;
                case "mdpreptime": return ExifTag.MDPrepTime;
                case "mdfileunits": return ExifTag.MDFileUnits;
                case "seminfo": return ExifTag.SEMInfo;
                case "spectralsensitivity": return ExifTag.SpectralSensitivity;
                case "datetimeoriginal": return ExifTag.DateTimeOriginal;
                case "datetimedigitized": return ExifTag.DateTimeDigitized;
                case "subsectime": return ExifTag.SubsecTime;
                case "subsectimeoriginal": return ExifTag.SubsecTimeOriginal;
                case "subsectimedigitized": return ExifTag.SubsecTimeDigitized;
                case "relatedsoundfile": return ExifTag.RelatedSoundFile;
                case "faxsubaddress": return ExifTag.FaxSubaddress;
                case "offsettime": return ExifTag.OffsetTime;
                case "offsettimeoriginal": return ExifTag.OffsetTimeOriginal;
                case "offsettimedigitized": return ExifTag.OffsetTimeDigitized;
                case "securityclassification": return ExifTag.SecurityClassification;
                case "imagehistory": return ExifTag.ImageHistory;
                case "imageuniqueid": return ExifTag.ImageUniqueID;
                case "ownername": return ExifTag.OwnerName;
                case "serialnumber": return ExifTag.SerialNumber;
                case "lensmake": return ExifTag.LensMake;
                case "lensmodel": return ExifTag.LensModel;
                case "lensserialnumber": return ExifTag.LensSerialNumber;
                case "gdalmetadata": return ExifTag.GDALMetadata;
                case "gdalnodata": return ExifTag.GDALNoData;
                case "gpslatituderef": return ExifTag.GPSLatitudeRef;
                case "gpslongituderef": return ExifTag.GPSLongitudeRef;
                case "gpssatellites": return ExifTag.GPSSatellites;
                case "gpsstatus": return ExifTag.GPSStatus;
                case "gpsmeasuremode": return ExifTag.GPSMeasureMode;
                case "gpsspeedref": return ExifTag.GPSSpeedRef;
                case "gpstrackref": return ExifTag.GPSTrackRef;
                case "gpsimgdirectionref": return ExifTag.GPSImgDirectionRef;
                case "gpsmapdatum": return ExifTag.GPSMapDatum;
                case "gpsdestlatituderef": return ExifTag.GPSDestLatitudeRef;
                case "gpsdestlongituderef": return ExifTag.GPSDestLongitudeRef;
                case "gpsdestbearingref": return ExifTag.GPSDestBearingRef;
                case "gpsdestdistanceref": return ExifTag.GPSDestDistanceRef;
                case "gpsdatestamp": return ExifTag.GPSDateStamp;
                case "jpegtables": return ExifTag.JPEGTables;
                case "oecf": return ExifTag.OECF;
                case "exifversion": return ExifTag.ExifVersion;
                case "componentsconfiguration": return ExifTag.ComponentsConfiguration;
                case "makernote": return ExifTag.MakerNote;
                case "usercomment": return ExifTag.UserComment;
                case "flashpixversion": return ExifTag.FlashpixVersion;
                case "spatialfrequencyresponse": return ExifTag.SpatialFrequencyResponse;
                case "spatialfrequencyresponse2": return ExifTag.SpatialFrequencyResponse2;
                case "noise": return ExifTag.Noise;
                case "cfapattern": return ExifTag.CFAPattern;
                case "devicesettingdescription": return ExifTag.DeviceSettingDescription;
                case "imagesourcedata": return ExifTag.ImageSourceData;
                case "gpsprocessingmethod": return ExifTag.GPSProcessingMethod;
                case "gpsareainformation": return ExifTag.GPSAreaInformation;
                case "filesource": return ExifTag.FileSource;
                case "scenetype": return ExifTag.SceneType;
                default: throw new ArgumentException("Exif tag specified does not exist: " + tag);
            }
        }
    }
}