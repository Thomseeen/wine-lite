using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wine_lite_view.Models {
    public class WineModel {
        public int Id { get; set; }

        #region Data
        public string Name { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string Vine { get; set; }
        public string WineType { get; set; }
        public string Taste { get; set; }
        #endregion

        #region Mappings
        public VendorModel Producer { get; set; }
        #endregion
    }
}
