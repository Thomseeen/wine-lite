﻿using System;
using System.ComponentModel.DataAnnotations;

namespace wine_lite_view.Models {
    public class BookingModel {
        [Key]
        public int BookingId { get; set; }

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
