using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class MainPageViewModel
    {
        [Required]
        [DisplayName("Shipment ID")]
        [MaxLength(10, ErrorMessage = "Shipment ID must have 10 digits")]
        [MinLength(10, ErrorMessage = "Shipment ID must have 10 digits")]
        [RegularExpression("^4[0-9]*$", ErrorMessage = "The Shipment ID must be numeric & starts with 4")]
        public int ShipmentID { get; set; }

        [Required(ErrorMessage = "Point of Departure is required. ")]
        [DisplayName("Point of Departure")]
        public string POD { get; set; }

        [Required(ErrorMessage = "Point of Arrival is required. ")]
        [DisplayName("Point of Arrival")]
        public string POA { get; set; }
    }
}