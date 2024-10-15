using MetadataExtractor;
using System.Windows.Media.Imaging;

namespace ImageFiMan.Models
{
    public class Photo
    {
        private readonly Uri _source;
        private string _filepath;
        private ICollection<Metatag> _metadataTags = [];
        private ExifMetadata _exifMetadata;

        public Photo(string path)
        {
            _source = new Uri(path);
            Image = BitmapFrame.Create(_source);
            Metadata = new ExifMetadata(path);

            foreach (var directory in ImageMetadataReader.ReadMetadata(path))
            {
                foreach (var tag in directory.Tags)
                {
                    if (tag.Description.Length > 20)
                        continue;
                    _metadataTags.Add(new Metatag(tag.Name, tag.Description));
                }
            }
        }

        public Uri Source { get { return _source; } }
        public BitmapFrame Image { get; set; }
        public ExifMetadata Metadata { get; }
        public override string ToString() => _source.ToString() ?? string.Empty;
        public ICollection<Metatag> MetaTags
        { get { return _metadataTags; } }
    }
}
