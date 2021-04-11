using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace wine_lite_view.Models {
    public class VendorModel {
        [Key]
        public int VendorId { get; set; }

        #region Data
        [Required]
        public bool IsProducer { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Mappings
        public virtual ICollection<BookingModel> Bookings { get; private set; } = new ObservableCollection<BookingModel>();
        #endregion

        #region Dynamic Data
        [NotMapped]
        public float OverallSpendings => Bookings.Select(booking => booking.OverallPrice).Sum();
        [NotMapped]
        public int BottlesBought => Bookings.Select(booking => booking.Quantity).Sum();
        [NotMapped]
        public int UniqueWines => Bookings.Select(booking => booking.Wine).Distinct().Count();
        #endregion

        #region Comparable
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var comp = (VendorModel)obj;
            return VendorId == comp.VendorId;
        }

        public override int GetHashCode() {
            return VendorId;
        }
        #endregion
    }
}
