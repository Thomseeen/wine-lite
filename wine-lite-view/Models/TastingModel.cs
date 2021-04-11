using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace wine_lite_view.Models {
    public class TastingModel {
        [Key]
        public int TastingId { get; set; }

        #region Data
        public string Name { get; set; }
        public string Taster { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Mappings
        public WineModel Wine { get; set; }
        public virtual ICollection<RatingModel> Ratings { get; private set; } = new ObservableCollection<RatingModel>();
        #endregion
    }
}
