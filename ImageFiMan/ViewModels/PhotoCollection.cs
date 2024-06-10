using ImageFiMan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageFiMan.ViewModels
{
    public class PhotoCollection : ObservableCollection<Photo>
    {
        private List<DuplicateFile> _duplicateFiles;

        public PhotoCollection() { }

        public PhotoCollection(List<DuplicateFile> duplicateFiles)
        {
            _duplicateFiles = duplicateFiles;
            Update();
        }
        public PhotoCollection(List<DuplicateGroup> duplicateGroups)
        {
            _duplicateFiles = new List<DuplicateFile>();
            foreach (DuplicateGroup duplicateGroup in duplicateGroups)
            {
                foreach (DuplicateFile duplicateFile in duplicateGroup.DuplicateFiles)
                {
                    _duplicateFiles.Add(duplicateFile);
                }
            }
            Update();
        }

        public List<DuplicateFile> DuplicateFiles
        {
            set
            {
                _duplicateFiles = value;
                Update();
            }
            get { return _duplicateFiles; }
        }

        private void Update()
        {
            Clear();
            foreach (var f in _duplicateFiles)
            {
                if (new FileInfo(f.Filepath).Exists)
                {
                    Add(new Photo(f.Filepath));
                }
                else
                {
                    MessageBox.Show("File not found");
                }
            }
        }
    }
}
