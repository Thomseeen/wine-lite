using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wine_lite_view.Models {
    public class TastingModel {
        public int Id { get; set; }

        #region Data
        public string Name { get; set; }
        public string Taster { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Mappings
        public WineModel Wine { get; set; }
        #endregion
    }
}
