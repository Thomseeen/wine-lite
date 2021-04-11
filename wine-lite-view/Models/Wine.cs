using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace wine_lite_view.Models {
    public enum WineType {
        Red,
        White,
        Rose
    }

    public class Wine {
        [Key]
        public int WineId { get; set; }

        #region Data
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Vine { get; set; }
        [Required]
        public WineType WineType { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string Taste { get; set; }
        #endregion

        #region Mappings
        public Producer Producer { get; set; }

        public virtual ICollection<Vendor> Vendors { get; set; }
        public virtual ICollection<Tasting> Tastings { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        #endregion

        #region Dynamic Data
        [NotMapped]
        public int VendorsCnt => Vendors?.Count ?? 0;
        [NotMapped]
        public int TastingsCnt => Tastings?.Count ?? 0;
        [NotMapped]
        public int BookingsCnt => Bookings?.Count ?? 0;
        [NotMapped]
        public int BottlesBought => Bookings?.Where(booking => booking.Quantity > 0)?.Select(booking => booking.Quantity).DefaultIfEmpty().Sum() ?? 0;
        [NotMapped]
        public int BottlesCnt => Bookings?.Select(tasting => tasting.Quantity).DefaultIfEmpty().Sum() ?? 0;
        [NotMapped]
        public float AvgPrice => Bookings?.Where(booking => booking.Quantity > 0)?.Select(booking => booking.Price).DefaultIfEmpty().Average() ?? 0;
        [NotMapped]
        public float AvgRating => Tastings?.Select(tasting => tasting.OverallRating).DefaultIfEmpty().Average() ?? 0;
        #endregion

        #region Comparable
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var comp = (Wine)obj;
            return WineId == comp.WineId;
        }

        public override int GetHashCode() {
            return WineId;
        }
        #endregion
    }
}
