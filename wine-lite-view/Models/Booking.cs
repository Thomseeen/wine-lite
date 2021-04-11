using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wine_lite_view.Models {
    public class Booking {
        [Key]
        public int BookingId { get; set; }

        #region Data
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public float Price { get; set; }
        #endregion

        #region Mappings
        [Required]
        public Wine Wine { get; set; }
        public Vendor Vendor { get; set; }
        #endregion

        #region Dynamic Data
        [NotMapped]
        public float OverallPrice => Quantity * Price;
        [NotMapped]
        public float AvgPriceOffset => Price - Wine.AvgPrice;
        #endregion

        #region Comparable
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var comp = (Booking)obj;
            return BookingId == comp.BookingId;
        }

        public override int GetHashCode() {
            return BookingId;
        }
        #endregion
    }
}
