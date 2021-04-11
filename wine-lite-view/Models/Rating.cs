using System.ComponentModel.DataAnnotations;

namespace wine_lite_view.Models {
    public class Rating {
        [Key]
        public int RatingId { get; set; }

        #region Data
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rate { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Mappings
        [Required]
        public Tasting Tasting { get; set; }
        #endregion

        #region Comparable
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var comp = (Rating)obj;
            return RatingId == comp.RatingId;
        }

        public override int GetHashCode() {
            return RatingId;
        }
        #endregion
    }
}
