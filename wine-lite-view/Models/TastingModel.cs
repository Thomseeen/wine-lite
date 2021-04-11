using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace wine_lite_view.Models {
    public class TastingModel {
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
        public WineModel Wine { get; set; }
        [Required]
        public virtual ICollection<RatingModel> Ratings { get; private set; } = new ObservableCollection<RatingModel>();
        #endregion

        #region Dynamic Data
        [NotMapped]
        public float OverallRating => Ratings.Select(rating => rating.Rate).Sum();
        [NotMapped]
        public float AvgRatingOffset => OverallRating - Wine.AvgRating;
        #endregion

        #region Comparable
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var comp = (TastingModel)obj;
            return TastingId == comp.TastingId;
        }

        public override int GetHashCode() {
            return TastingId;
        }
        #endregion
    }
}
