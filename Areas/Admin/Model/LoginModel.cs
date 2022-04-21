using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZealEducationManager.Areas.Admin.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}