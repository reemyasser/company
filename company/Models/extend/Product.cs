using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace company.Models
{
    [MetadataType(typeof(ColorProduct))]
    public partial class Product
    {
       
      
       
    }
  
    public class ColorProduct
    {
        public double Price { get; set; }
        public string ProductName { get; set; }
        public double whalesaleprice { get; set; }
    }
}