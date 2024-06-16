using ImageFiMan.Models;
using ImageFiMan.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Photos = (PhotoCollection)(Application.Current.Resources["Photos"] as ObjectDataProvider)?.Data;
            Photos.DuplicateFiles = new List<DuplicateFile>();

            using (var context = new DuplicateFileGroupContext())
            {
                context.Database.EnsureCreated();
                var groups = context.DuplicateGroups.Include(a => a.DuplicateFiles).ToList();

                trvFileGroups.ItemsSource = groups;

            }
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"TreeViewItem_Selected {sender.ToString}");
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
            var pvWindow = new PhotoViewer { SelectedPhoto = (Photo)PhotosListBox.SelectedItem };
            pvWindow.Show();
        }
    }
}