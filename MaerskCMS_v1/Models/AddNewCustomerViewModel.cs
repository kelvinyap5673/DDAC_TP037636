using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class AddNewCustomerViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Customer Name is required. ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer Address is required. ")]
        public string Address { get; set; }
    }
}