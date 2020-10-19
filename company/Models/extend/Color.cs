using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace company.Models
{
    [MetadataType(typeof(colorsproduct))]
    public partial class Color
    {
    }
    public class colorsproduct
    {
        [Key]
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public byte[] Img { get; set; }
        public string color1 { get; set; }
       
    }
}