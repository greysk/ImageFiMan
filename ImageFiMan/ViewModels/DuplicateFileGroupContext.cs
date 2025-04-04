﻿using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using ImageFiMan.Data;
using ImageFiMan.Models;

namespace ImageFiMan.ViewModels
{
    internal class DuplicateFileGroupContext : DbContext
    {
        public DbSet<DuplicateReport> DuplicateReports { get; set; }
        public DbSet<DuplicateGroup> DuplicateGroups { get; set; }
        public DbSet<DuplicateFile> DuplicateFiles { get; set; }
        public string DbPath { get; }

        public DuplicateFileGroupContext()
        {
            string DbDir = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ImageManFi");
            if (!Path.Exists(DbDir))
                Directory.CreateDirectory(DbDir);
            DbPath = Path.Join(DbDir, "DuplicateFile.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DuplicateGroup.ApplyConfiguration(modelBuilder);
            DuplicateFile.ApplyConfiguration(modelBuilder);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
