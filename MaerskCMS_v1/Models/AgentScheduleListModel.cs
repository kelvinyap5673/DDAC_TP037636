using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class AgentScheduleListViewModel
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Vessel Name")]
        public string VesselName { get; set; }

        [DisplayName("POD")]
        public string POD { get; set; }

        [DisplayName("POA")]
        public string POA { get; set; }

        [DisplayName("Departure")]
        public string Departure { get; set; }

        [DisplayName("Arrival")]
        public string Arrival { get; set; }
    }
}