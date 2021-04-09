﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wine_lite_view.Models {
    public class RatingModel {
        public int Id { get; set; }

        #region Data
        public string Name { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Mappings
        public TastingModel Tasting { get; set; }
        #endregion

    }
}