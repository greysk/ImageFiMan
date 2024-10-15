using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ImageFiMan.Models
{
    public class DuplicateReport
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<DuplicateGroup> _duplicateGroups;

        public DuplicateReport() { }
        public DuplicateReport(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int DuplicateReportId { get; set; }
        public DateTime DateGenerated { get; set; }
        
        public virtual ICollection<DuplicateGroup> DuplicateGroups
        {
            get => _lazyLoader.Load(this, ref _duplicateGroups);
            set => _duplicateGroups = value;
        }

        public static DateTime GetFileDate(string filepath)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filepath);
            DateTime reportDate = DateTime.ParseExact(fileName.Replace("duplicate_file", ""), "yyyymmdd", null);
            return reportDate;
        }

    }
}
