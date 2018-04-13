using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskCMS_v1.Models
{
    public class RegisterModel
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "First Name is required. ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required. ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "New Username is required. ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "New Password is Required. ")]
        [DataType(DataType.Password)]
        [MaxLength(32, ErrorMessage="Maximum 32 characters. ")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters, at least one letter and one number. ")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The New Password is not match. ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="E-mail Address is Required. ")]
        [EmailAddress(ErrorMessage ="Invalid E-mail Address, please try again. ")]
        public string EmailAddress { get; set; }
    }
}