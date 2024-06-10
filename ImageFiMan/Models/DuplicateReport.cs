using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ImageFiMan.Models
{
    public class DuplicateReport
    {
        private ICollection<DuplicateGroup> _duplicateGroups;
        private ILazyLoader _lazyLoader;

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
