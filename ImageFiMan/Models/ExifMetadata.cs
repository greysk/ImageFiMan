using MetadataExtractor;

namespace ImageFiMan.Models
{
    public class ExifMetadata
    {
        private readonly string _source;
        private List<Tag> _imageWidth = [];
        private List<Tag> _imageHeight = [];
        private List<Tag> _datetimes = [];
        private readonly IReadOnlyList<Directory> _metadataDirectories = [];

        public ExifMetadata(string source)
        {
            _source = source;
            _metadataDirectories = ImageMetadataReader.ReadMetadata(source);
            foreach (var directory in _metadataDirectories)
            {
                foreach (var tag in directory.Tags)
                {
                    if (tag.Name.Contains("Width"))
                        _imageWidth.Add(tag);
                    else if (tag.Name.Contains("Height"))
                        _imageHeight.Add(tag);
                    else if (tag.Name.Contains("Date"))
                        _datetimes.Add(tag);
                }
            }
        }
        public string Source { get { return _source; }  }

        public uint? Width
        {
            get
            {
                if (_imageWidth.Count == 0)
                    return null;
                return Convert.ToUInt32(_imageWidth[0].Description);
            }
        }

        public uint? Height
        {
            get
            {
                if ( _imageHeight.Count == 0)
                    return null;
                return Convert.ToUInt32(_imageHeight[0].Description);
            }
        }

        public string? DateImageTaken
        {
            get
            {
                if ( _datetimes.Count == 0)
                    return null;
                return _datetimes[0].Description;
            }
        }
    }
}
