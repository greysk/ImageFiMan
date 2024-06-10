using System;
using System.Windows.Media.Imaging;

namespace ImageFiMan.ViewModels
{
    public class Photo
    {
        private readonly Uri _source;

        public Photo(string path)
        {
            Source = path;
            _source = new Uri(path);
            Image = BitmapFrame.Create(_source);
            Metadata = new ExifMetadata(_source);
        }

        public string Source { get; }
        public BitmapFrame Image { get; set; }
        public ExifMetadata Metadata { get; }

        public override string ToString() => _source.ToString() ?? string.Empty;
    }
}
