using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestCodingASPNET_QE_DAL
{
    /// <summary>
    /// Login Screen - View Model Class
    /// </summary>
    public class LoginVM
    {
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    public class RegisterVM
    {
        public int UserMasterId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string UserPassword { get; set; }
        public bool IsActive { get; set; } = true;
        public System.DateTime CreatedTime { get; set; } = DateTime.Now;
        public System.DateTime ModifiedTime { get; set; } = DateTime.Now;
    }
    public class UserInfoVM
    {
        public int UserMasterId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string UserPassword { get; set; }
    }
    public class CarVM
    {
        public int CarMasterId { get; set; }

        [Required(ErrorMessage = "Car Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Year Made is required")]
        public int YearMade { get; set; }
        public bool IsActive { get; set; } = true;
        public System.DateTime CreatedTime { get; set; } = DateTime.Now;
        public System.DateTime ModifiedTime { get; set; } = DateTime.Now;
    }
    public class CarInfoVM
    {
        public int CarMasterId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int YearMade { get; set; }
    }
}