using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace company.Models
{
    [MetadataType(typeof(metadatapreviousorder))]
    public partial class PreviousOrder
    {

    }
    public class metadatapreviousorder {
        
       [Required(AllowEmptyStrings = false, ErrorMessage = "Size  required")]
        public string Size { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quentity required")]
        public int Quentity { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "color  required")]
        public string color { get; set; }
        public int productid { get; set; }
        public int previd { get; set; }
        public int userid { get; set; }
         [WebMethod] 
        public void removerow(int id)
        {
            tshirtsEntities db = new tshirtsEntities();
            var row = db.PreviousOrder.Find((id));
            db.PreviousOrder.Remove(row);
            db.SaveChanges();
        }

    
    }
}