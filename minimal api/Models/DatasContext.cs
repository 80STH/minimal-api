using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class DatasContext : DbContext
    {
        public DatasContext(DbContextOptions<DatasContext> options)
            : base(options)
        {
        }

        public DbSet<Datas> Datas { get; set; } = null!;
        public DbSet<Scan> Scan { get; set; } = null!;
        public DbSet<Files> Files { get; set; } = null!;
        public DbSet<Error> Errors { get; set; } = null!;



    }
}
