//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestCodingASPNET_QE_DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UserMaster
    {
        public int UserMasterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string UserPassword { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public System.DateTime ModifiedTime { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
