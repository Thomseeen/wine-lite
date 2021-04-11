using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace wine_lite_view.Models {
    public class Tasting {
        [Key]
        public int TastingId { get; set; }

        #region Data
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Taster { get; set; }
        #endregion

        #region Mappings
        [Required]
        public Wine Wine { get; set; }
        [Required]
        public virtual ICollection<Rating> Ratings { get; set; }
        #endregion

        #region Dynamic Data
        [NotMapped]
        public float OverallRating => Ratings?.Select(rating => rating.Rate).DefaultIfEmpty().Sum() ?? 0;
        [NotMapped]
        public float AvgRatingOffset => OverallRating - Wine.AvgRating;
        #endregion

        #region Comparable
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var comp = (Tasting)obj;
            return TastingId == comp.TastingId;
        }

        public override int GetHashCode() {
            return TastingId;
        }
        #endregion
    }
}
