using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace company.Models
{
[MetadataType(typeof(MetadataOrder))]
    public partial class Order
    {
    
    }
    public class MetadataOrder
    {

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        

    }
    }