using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    namespace MaerskCMS_v1.Models
    {
        public class VesselModel
        {
            [Key]
            public int VesselID { get; set; }

            [Required(ErrorMessage = "Vessel Name is required. ")]
            public string Vessel { get; set; }

            [Required(ErrorMessage = "Last Name is required. ")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "New Username is required. ")]
            public string Username { get; set; }

            [Required(ErrorMessage = "New Password is Required. ")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "The New Password is not match. ")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "E-mail Address is Required. ")]
            [EmailAddress(ErrorMessage = "Invalid E-mail Address, please try again. ")]
            public string EmailAddress { get; set; }
        }
    }
}