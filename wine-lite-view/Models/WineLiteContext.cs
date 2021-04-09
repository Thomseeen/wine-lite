using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wine_lite_view.Models {
    public class WineLiteContext : DbContext {
        private const string DEFAULT_DB_NAME = "wines.wldb";
        private readonly static string DEFAULT_DB_PATH = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        private static bool _created = false;

        public DbSet<WineModel> Wines { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<VendorModel> Vendors { get; set; }
        public DbSet<TastingModel> Tastings { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }

        public WineLiteContext() {
            if (!_created) {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DEFAULT_DB_PATH}\\{DEFAULT_DB_NAME}");
    }
}
