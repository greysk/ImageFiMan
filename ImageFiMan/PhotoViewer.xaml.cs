// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageFiMan.Models;

namespace ImageFiMan
{
    /// <summary>
    ///     Interaction logic for PhotoViewer.xaml
    /// </summary>
    public partial class PhotoViewer : Window
    {
        private BitmapFrame _orgImage;
        private bool _isBlackAndWhite = false;
        public PhotoViewer()
        {
            InitializeComponent();
        }

        public Photo SelectedPhoto { get; set; }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            _orgImage = SelectedPhoto.Image;
            ViewedPhoto.Source = SelectedPhoto.Image;
            ViewedCaption.Content = SelectedPhoto.Source;
            ViewedMetatags.ItemsSource = SelectedPhoto.MetaTags;
        }

        private void Rotate(object sender, RoutedEventArgs e)
        {
            BitmapSource img = SelectedPhoto.Image;

            var cache = new CachedBitmap(img, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            SelectedPhoto.Image = BitmapFrame.Create(new TransformedBitmap(cache, new RotateTransform(90.0)));

            ViewedPhoto.Source = SelectedPhoto.Image;
        }

        private void BlackAndWhite(object sender, RoutedEventArgs e)
        {
            if (!_isBlackAndWhite)
            {
                BitmapSource img = SelectedPhoto.Image;
                SelectedPhoto.Image =
                    BitmapFrame.Create(new FormatConvertedBitmap(img, PixelFormats.Gray8, BitmapPalettes.Gray256, 1.0));
                _isBlackAndWhite = true;
            }
            else
            {
                SelectedPhoto.Image = _orgImage;
                _isBlackAndWhite = false;
            }
            ViewedPhoto.Source = SelectedPhoto.Image;
        }
    }
}
