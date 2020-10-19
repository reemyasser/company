using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace company.Models
{
    [MetadataType(typeof(metadataOrderDetails))]
    public partial class OrderDetails
    {
    } 
    public class metadataOrderDetails
    {

        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailsId { get; set; }
        public int Quentity { get; set; }
        public string Size { get; set; }
        public int ColorId { get; set; }
    }
}