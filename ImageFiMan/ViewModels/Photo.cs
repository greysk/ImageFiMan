using MetadataExtractor;
using System;
using System.Windows.Media.Imaging;

namespace ImageFiMan.ViewModels
{
    public class Photo
    {
        private readonly Uri _source;
        //private ICollection<ExifMetadata> _metadataDirectories = [];
        private ICollection<Metatag> _metadataDirectories = [];

        public Photo(string path)
        {
            _source = new Uri(path);
            Image = BitmapFrame.Create(_source);

            foreach (var directory in ImageMetadataReader.ReadMetadata(path))
            {
                foreach (var tag in directory.Tags)
                {
                    if (tag.Description.Length > 20)
                        continue;
                    _metadataDirectories.Add(new Metatag(tag.Name, tag.Description));
                }
            }
        }

        public string? Source { get; }
        public BitmapFrame Image { get; set; }
        //public ExifMetadata Metadata { get; }
        public override string ToString() => _source.ToString() ?? string.Empty;
        public ICollection<Metatag> Metadata
        { get {  return _metadataDirectories; } }
    }
}
