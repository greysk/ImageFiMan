using ImageFiMan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageFiMan.Data
{
    public static class DuplModelExtensionBuilder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            const string inFile = @"P:\source\greysk\ImageFiMan\ImageFiMan\Data\duplicate_file20240524.csv";
            string[] lines = File.ReadAllLines(Path.ChangeExtension(inFile, ".csv"));
            int fileNum = 0;

            DuplicateReport newReport = new DuplicateReport { DuplicateReportId = 2, DateGenerated = DuplicateReport.GetFileDate(inFile) };
            DuplicateGroup lastAddedGroup = new DuplicateGroup { GroupNo = 0 };

            //Add report.
            modelBuilder.Entity<DuplicateReport>().HasData(newReport);

            foreach (string line in lines)
            {
                string[] parts = line.Split('\t');

                if (parts.Length < 5)
                    throw new Exception("Not enough columns");
                if (parts[0] == "Group")  //Skip header row
                    continue;

                int groupNo = int.Parse(parts[0]);
                string sharedFolder = parts[1].Trim('"');
                string filePath = parts[2].Trim('"');
                int sizeInBytes = int.Parse(parts[3]);
                DateTime modTime = Convert.ToDateTime(parts[4].Trim('"'));

                if (lastAddedGroup == null || lastAddedGroup.GroupNo != groupNo)
                {
                    //Add new group.
                    lastAddedGroup = new DuplicateGroup { DuplicateGroupId = groupNo, GroupNo = groupNo, DuplicateReportId = newReport.DuplicateReportId};
                    modelBuilder.Entity<DuplicateGroup>().HasData(lastAddedGroup);
                }

                fileNum++;
                //Add file.
                modelBuilder.Entity<DuplicateFile>().HasData(
                    new DuplicateFile
                    {
                        DuplicateFileId = fileNum,
                        DuplicateGroupId = lastAddedGroup.DuplicateGroupId,
                        SharedFolderPath = sharedFolder,
                        Filepath = filePath,
                        ModifiedTime = modTime,
                        ByteSize = sizeInBytes
                    }
                );
            }
        }
    }
}
