using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class AddNewVesselViewModel
    {
        [Required(ErrorMessage = "Vessel Name is Required")]
        [DisplayName("Vessel Name")]
        public string VesselName { get; set; }

        [DisplayName("Description (Optional)")]
        public string VesselDescription { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Size")]
        public int VesselSize { get; set; }
    }
}