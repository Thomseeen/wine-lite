using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace wine_lite_view.Models {
    public class WineLiteContext : DbContext {
        #region Private Fields
        private string _dbPath;
        #endregion

        #region Properties
        public DbSet<WineModel> Wines { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<VendorModel> Vendors { get; set; }
        public DbSet<TastingModel> Tastings { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
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
