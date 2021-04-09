using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace wine_lite_view.Models {
    public class WineLiteContext : DbContext {
        #region Constants
        private const string DEFAULT_DB_NAME = "wines.wldb";
        private readonly static string DEFAULT_DB_PATH = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        #endregion

        #region Private Fields
        private static bool _created = false;
        #endregion

        #region Properties
        public DbSet<WineModel> Wines { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<VendorModel> Vendors { get; set; }
        public DbSet<TastingModel> Tastings { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        #endregion

        #region Constructors
        public WineLiteContext() {
            if (!_created) {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        #endregion

        #region DbContext Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DEFAULT_DB_PATH}\\{DEFAULT_DB_NAME}");
        #endregion
    }
}
