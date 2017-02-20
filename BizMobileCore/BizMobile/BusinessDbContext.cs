using Microsoft.EntityFrameworkCore;

namespace BizMobile
{
    public class BusinessDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        private string _databasePath;

        public BusinessDbContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        private static BusinessDbContext dbContext;
        public static BusinessDbContext Create(string databasePath)
        {
            if (dbContext == null)
            {
                dbContext = new BusinessDbContext(databasePath);
                dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
                dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            
            return dbContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}