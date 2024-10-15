using ImageFiMan.Models;
using ImageFiMan.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace ImageFiMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public PhotoCollection Photos;

        public MainWindow()
        {
            InitializeComponent();
            Photos = (PhotoCollection)(Application.Current.Resources["Photos"] as ObjectDataProvider).Data;
            Photos.DuplicateFiles = new List<DuplicateFile>();

            using (var context = new DuplicateFileGroupContext())
            {
                context.Database.Migrate();
                //context.Database.EnsureCreated();
                var groups = context.DuplicateGroups.Include(a => a.DuplicateFiles).ToList();
                

                trvFileGroups.ItemsSource = groups;
            }
        }

        private void OnTrvSelected(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Debug.WriteLine("OnTrvSelected");
            if (e.NewValue is DuplicateGroup)
            {
                Photos.DuplicateFiles = ((DuplicateGroup)(e.NewValue)).DuplicateFiles.ToList();
            }
            else if (e.NewValue is DuplicateFile)
            {
                Photos.DuplicateFiles = [((DuplicateFile)e.NewValue)];
            }
        }

        private void OnPhotoClick(object sender, RoutedEventArgs e)
        {
            var photoWindow = new PhotoViewer { 
                SelectedPhoto = (Photo)PhotosListBox.SelectedItem };
            photoWindow.Show();
        }
    }
}