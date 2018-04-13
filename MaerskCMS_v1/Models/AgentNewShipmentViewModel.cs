using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskCMS_v1.Models
{
    public class AgentNewShipmentViewModel
    {
        public int ScheduleID { get; set; }

        public string POD { get; set; }

        public string POA { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public int SpaceAvailable { get; set; }

        public List<SelectListItem> CustomerList { get; set; }

        public IEnumerable<SelectListItem> GetCustomerListItem()
        {
            return CustomerList.Select(c => new SelectListItem()
            {
                Text = c.Text,
                Value = c.Value
            });
        }

        [DisplayName("Contract No (Optional)")]
        public string ContractNo { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [DisplayName("Price (RM)")]
        [RegularExpression(@"^(\d{1,})*(\.\d{1,2})?$", ErrorMessage = "Invalid Format")]
        public decimal Price { get; set; }

        [DisplayName("Commodity")]
        [Required(ErrorMessage = "Commodity is required. ")]
        public string Commodity { get; set; }

        [DisplayName("Container Type")]
        [Required(ErrorMessage = "Container Type is required. ")]
        public string ContainerType { get; set; }

        [DisplayName("Packages Amount")]
        [Required(ErrorMessage = "PackagesAmount is required. ")]
        public int PackagesAmount { get; set; }

        [DisplayName("Kind Of Packages")]
        [Required(ErrorMessage = "Kind Of Packages is required. ")]
        public string KindOfPackages { get; set; }

        [DisplayName("Weight (kg)")]
        [Required(ErrorMessage = "Weight is required. ")]
        public decimal Weight { get; set; }

        [DisplayName("Volume (m^3)")]
        [Required(ErrorMessage = "Volume is required. ")]
        public decimal Volume { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required. ")]
        public string Description { get; set; }
    }
}