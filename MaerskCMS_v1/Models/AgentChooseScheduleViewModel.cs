using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class AgentChooseScheduleViewModel
    {
        [Key]
        public int ID { get; set; }
        
        public string POD { get; set; }

        public string POA { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        [DisplayName("Space Available")]
        public int SpaceAvailable { get; set; }
    }
}