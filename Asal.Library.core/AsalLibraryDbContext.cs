
using Asal.Library.core.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Asal.Library.core
{
    public class AsalLibraryDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AsalLibraryDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AsalLibraryDBContext"));
            }
        }
        public DbSet<PackageDetail> PackageDetail { get; set; }

    }
}
