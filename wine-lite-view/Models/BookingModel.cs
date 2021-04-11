using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wine_lite_view.Models {
    public class BookingModel {
        [Key]
        public int BookingId { get; set; }

        #region Data
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        #endregion

        #region Mappings
        [Required]
        public WineModel Wine { get; set; }
        [Required]
        public VendorModel Vendor { get; set; }
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

            var comp = (BookingModel)obj;
            return BookingId == comp.BookingId;
        }

        public override int GetHashCode() {
            return BookingId;
        }
        #endregion
    }
}
