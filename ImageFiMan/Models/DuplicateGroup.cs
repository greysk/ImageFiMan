using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace ImageFiMan.Models
{
    public class DuplicateGroup
    {
        private readonly ILazyLoader _lazyLoader;
        private DuplicateReport _duplicateReport;
        private ICollection<DuplicateFile> _duplicateFiles;

        public DuplicateGroup() { }
        public DuplicateGroup(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public int DuplicateGroupId { get; set; }
        public int GroupNo { get; set; }

        public int DuplicateReportId { get; set; }
        public virtual DuplicateReport DuplicateReport {
            get => _lazyLoader.Load(this, ref _duplicateReport);
            set => _duplicateReport = value; 
        }

        public virtual ICollection<DuplicateFile> DuplicateFiles
        {
            get => _lazyLoader.Load(this, ref _duplicateFiles);
            set => _duplicateFiles = value;
        }

        public static void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DuplicateGroup>().HasAlternateKey(e => new { e.DuplicateReportId, e.GroupNo });
        }
    }
}
