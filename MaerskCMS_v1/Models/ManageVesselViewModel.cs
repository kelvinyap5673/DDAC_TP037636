using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class ManageVesselViewModel
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Vessel Name")]
        public string VesselName { get; set; }

        [DisplayName("Description")]
        public string VesselDescription { get; set; }

        [DisplayName("Size")]
        public int VesselSize { get; set; }

        [DisplayName("Added")]
        public string AddedDT { get; set; }
    }
}