using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class CustomerListViewModel
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Added")]
        public string AddedDT { get; set; }
    }
}