using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class AgentShipmentDetailsViewModel
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

        [DisplayName("ContractNo")]
        public string ContractNo { get; set; }

        [DisplayName("Price")]
        public string Price { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Shipper Name")]
        public string ShipperName { get; set; }

        [DisplayName("Shipper Address")]
        public string ShipperAddress { get; set; }

        [DisplayName("Consignee Name")]
        public string ConsigneeName { get; set; }

        [DisplayName("Consignee Address")]
        public string ConsigneeAddress { get; set; }

        [DisplayName("Commodity")]
        public string Commodity { get; set; }

        [DisplayName("Container Type")]
        public string ContainerType { get; set; }

        [DisplayName("Packages Amount")]
        public string PackagesAmount { get; set; }

        [DisplayName("Kind Of Packages")]
        public string KindOfPackages { get; set; }

        [DisplayName("Weight (kg)")]
        public string Weight { get; set; }

        [DisplayName("Volume (m^3)")]
        public string Volume { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}