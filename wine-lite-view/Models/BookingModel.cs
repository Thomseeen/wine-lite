using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wine_lite_view.Models {
    public class BookingModel {
        public int Id { get; set; }

        #region Data
        public int Quantity { get; set; }
        public float Price { get; set; }
        public DateTime Time { get; set; }
        #endregion

        #region Mappings
        public WineModel Wine { get; set; }
        public VendorModel Vendor { get; set; }
        #endregion
    }
}
