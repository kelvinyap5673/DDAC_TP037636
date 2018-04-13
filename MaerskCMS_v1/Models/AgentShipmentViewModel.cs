using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class AgentShipmentViewModel
    {
        [DisplayName("Shipment ID")]
        public string ID { get; set; }

        [DisplayName("POD")]
        public string POD { get; set; }

        [DisplayName("POA")]
        public string POA { get; set; }

        [DisplayName("Departure")]
        public string Departure { get; set; }

        [DisplayName("Arrival")]
        public string Arrival { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
    }
}