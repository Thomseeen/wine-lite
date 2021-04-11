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

    public class WineModel {
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
        public virtual VendorModel Producer { get; set; }

        public virtual ICollection<VendorModel> Vendors { get; private set; } = new ObservableCollection<VendorModel>();
        public virtual ICollection<TastingModel> Tastings { get; private set; } = new ObservableCollection<TastingModel>();
        public virtual ICollection<BookingModel> Bookings { get; private set; } = new ObservableCollection<BookingModel>();
        #endregion

        #region Dynamic Data
        [NotMapped]
        public int VendorsCnt => Vendors.Count;
        [NotMapped]
        public int TastingsCnt => Tastings.Count;
        [NotMapped]
        public int BookingsCnt => Bookings.Count;
        [NotMapped]
        public int BottlesBought => Bookings.Where(booking => booking.Quantity > 0).Select(booking => booking.Quantity).Sum();
        [NotMapped]
        public int BottlesCnt => Bookings.Select(tasting => tasting.Quantity).Sum();
        [NotMapped]
        public float AvgPrice => Bookings.Select(booking => booking.Price).Average();
        [NotMapped]
        public float AvgRating => Tastings.Select(tasting => tasting.OverallRating).Average();
        #endregion

        #region Comparable
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var comp = (WineModel)obj;
            return WineId == comp.WineId;
        }

        public override int GetHashCode() {
            return WineId;
        }
        #endregion
    }
}
