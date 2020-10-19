using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace company.Models
{
    [MetadataType(typeof(metadataOrderView))]
    public partial class ordersview
    {
       
    }
    public class metadataOrderView

    {


        public int ColorId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public string color { get; set; }
        public string Img { get; set; }
        public int UserId { get; set; }
        public int Quentity { get; set; }
        public DateTime OrderDate { get; set; }
       
    }
}