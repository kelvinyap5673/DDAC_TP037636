using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class AgentModel
    {
        public int ID { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Registration { get; set; }

        public bool Status { get; set; }
    }
}