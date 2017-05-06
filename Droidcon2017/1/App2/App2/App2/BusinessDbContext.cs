namespace App2
{
    // for migrations. check https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite

    public class BusinessDbContext : DbContext
    {
        private static string path;
        private static BusinessDbContext dbContext;
        //public static BusinessDbContext Create(string dbPath)
        //{
        //    if (dbContext == null)
        //    {
        //        path = dbPath;
        //        dbContext = new BusinessDbContext();
        //        dbContext.Database.EnsureCreated();
        //        dbContext.Database.Migrate();
        //        dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //    }

        //    return dbContext;
        //}

        public BusinessDbContext()
        {
            
        }

        public BusinessDbContext(string dbPath)
        {
            path = dbPath;         
        }

        //tables        

        #region Shops

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        #endregion

        #region Products

        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductSerial> ProductSerials { get; set; }

        #endregion

        #region Sales and Purchases

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

        #endregion

        #region Customers

        public DbSet<Customer> Customers { get; set; }

        #endregion

        #region Accounts

        public DbSet<AccountHead> AccountHeads { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        #endregion

        #region Settings

        public DbSet<SettingData> SettingsData { get; set; }

        public DbSet<Shop> ShopData { get; set; }

        #endregion        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            optionsBuilder.UseSqlite($"Filename={path}");
        }

    }
}