using Microsoft.EntityFrameworkCore;

namespace wine_lite_view.Models {
    public class WineLiteContext : DbContext {
        #region Private Fields
        private string _dbPath;
        #endregion

        #region Properties
        public DbSet<Wine> Wines { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Tasting> Tastings { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        #endregion

        #region Constructors
        public WineLiteContext(string dbpath, bool forcerebuild = false) {
            _dbPath = dbpath;

            if (forcerebuild) {
                Database.EnsureDeleted();
            }
            Database.EnsureCreated();
        }
        #endregion

        #region DbContext Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dbPath}");
        #endregion
    }
}
