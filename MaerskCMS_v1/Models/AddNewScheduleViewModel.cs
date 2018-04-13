using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskCMS_v1.Models
{
    public class AddNewScheduleViewModel
    {
        [Key]
        public int ID { get; set; }

        public List<SelectListItem> Vessel { get; set; }

        public IEnumerable<SelectListItem> GetVesselListItem()
        {
            return Vessel.Select(c => new SelectListItem()
            {
                Text = c.Text,
                Value = c.Value
            });
        }

        [DisplayName("POD")]
        [Required(ErrorMessage = "Please enter the Point of Departure. ")]
        public string POD { get; set; }

        [DisplayName("POA")]
        [Required(ErrorMessage = "Please enter the Point of Arrival. ")]
        public string POA { get; set; }

        [DisplayName("Depart On")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Departure { get; set; }

        [DisplayName("Arrive On")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Arrival { get; set; }
    }
}