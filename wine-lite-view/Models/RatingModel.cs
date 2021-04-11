using System.ComponentModel.DataAnnotations;

namespace wine_lite_view.Models {
    public class RatingModel {
        [Key]
        public int RatingId { get; set; }

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
