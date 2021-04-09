using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wine_lite_view.Models {
    public class VendorModel {
        public int Id { get; set; }

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
        #endregion
    }
}
