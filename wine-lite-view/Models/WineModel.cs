using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string Vine { get; set; }
        public WineType WineType { get; set; }
        public string Taste { get; set; }
        #endregion

        #region Mappings
        public virtual VendorModel Producer { get; set; }

        public virtual ICollection<VendorModel> Vendors { get; private set; } = new ObservableCollection<VendorModel>();
        public virtual ICollection<TastingModel> Tastings { get; private set; } = new ObservableCollection<TastingModel>();
        public virtual ICollection<BookingModel> Bookings { get; private set; } = new ObservableCollection<BookingModel>();
        #endregion
    }
}
