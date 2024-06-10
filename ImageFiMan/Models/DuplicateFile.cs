using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFiMan.Models
{
    public class DuplicateFile
    {
        private string _filepath = "";
        private string _orgfilepath = "";
        private string _sharedFolder = "";
        private DuplicateGroup _duplicateGroup;
        private ILazyLoader _lazyLoader;

        public DuplicateFile() { }
        public DuplicateFile(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader; 
        }

        public int DuplicateFileId { get; set; }
        public string SharedFolderPath {
            get { return $"{_sharedFolder}:{_orgfilepath}"; }
            set { _sharedFolder = value; }
        }
        public string Filepath {
            get { return _filepath; } 
            set { 
                _orgfilepath = value;
                _filepath = DuplicateFile.ConvertFilePath(_orgfilepath, "GraeR", "P:/.TempImageFiMan");
                Name = Path.GetFileName(_filepath);
            } 
        }
        public string Name { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public Int32? ByteSize { get; set; }

        public int DuplicateGroupId { get; set; }
        public virtual DuplicateGroup DuplicateGroup {
            get => _lazyLoader.Load(this, ref _duplicateGroup);
            set => _duplicateGroup = value; 
        }
        
        public static void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DuplicateFile>().HasAlternateKey(e => e.Filepath);
            modelBuilder.Entity<DuplicateFile>().HasIndex(e => e.Name);
        }

        public static string ConvertFilePath(string filePath, string startReplace, string replaceWith)
        {
            int postition = filePath.IndexOf(startReplace);
            StringBuilder strPathBuilder = new StringBuilder(replaceWith);
            strPathBuilder.Append(filePath.Substring(postition + startReplace.Length).Trim());
            string newpath = strPathBuilder.ToString();
            return Path.GetFullPath(newpath);
        }
    }
}
