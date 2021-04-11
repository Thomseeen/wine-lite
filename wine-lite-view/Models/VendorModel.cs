using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace wine_lite_view.Models {
    public class VendorModel {
        [Key]
        public int VendorId { get; set; }

        #region Data
        public bool IsProducer { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Mappings
        public virtual ICollection<VendorModel> Vendors { get; private set; } = new ObservableCollection<VendorModel>();
        public virtual ICollection<BookingModel> Bookings { get; private set; } = new ObservableCollection<BookingModel>();
        #endregion
    }
}
