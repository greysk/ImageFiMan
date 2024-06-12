using System;
using System.Windows.Media.Imaging;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace ImageFiMan.ViewModels
{
    public class ExifMetadata
    {
        private readonly IEnumerable<Directory> _metadataDirectories;

        public ExifMetadata(string imagePath)
        {
            _metadataDirectories = ImageMetadataReader.ReadMetadata(imagePath);
        }
        public uint? Width
        {
            get
            {
                var subIfDirectory = _metadataDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var value = subIfDirectory?.GetDescription(ExifDirectoryBase.TagExifImageWidth);
                if (value == null)
                    return null;
                return Convert.ToUInt32(value);
            }
        }

        public uint? Height
        {
            get
            {
                var subIfDirectory = _metadataDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var value = subIfDirectory?.GetDescription(ExifDirectoryBase.TagExifImageHeight);
                if (value == null)
                    return null;
                return Convert.ToUInt32(value);
            }
        }

        public DateTime? DateImageTaken
        {
            get
            {
                var subIfDirectory = _metadataDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var value = subIfDirectory?.GetDescription(ExifDirectoryBase.TagDateTime);
                if (value == null)
                    return null;
                return Convert.ToDateTime(value);
            }
        }

        private decimal ParseUnsignedRational(ulong exifValue) => (exifValue & 0xFFFFFFFFL) / (decimal)((exifValue & 0xFFFFFFFF00000000L) >> 32);

    }
}
