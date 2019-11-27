//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nep.Project.DBModels.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SC_User
    {
        public SC_User()
        {
            this.UserAccess = new HashSet<SC_UserAccess>();
            this.UserRegisterEntries = new HashSet<UserRegisterEntry>();
        }
    
        public decimal UserID { get; set; }
        public Nullable<decimal> OrganizationID { get; set; }
        public Nullable<decimal> ProvinceID { get; set; }
        public Nullable<decimal> GroupID { get; set; }
        public string UserFlag { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public string FirstName { get; set; }
        public string Position { get; set; }
        public string IDNo { get; set; }
        public Nullable<decimal> IDCardFileID { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public Nullable<decimal> EmploeeCardFileID { get; set; }
        public string IsActive { get; set; }
        public Nullable<System.DateTime> RegisterDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string LastName { get; set; }
        public string IsDelete { get; set; }
        public string Salt { get; set; }
        public string ForgetPasswordToken { get; set; }
        public Nullable<System.DateTime> ForgetPasswordRequestDate { get; set; }
        public decimal CreatedByID { get; set; }
        public Nullable<decimal> UpdatedByID { get; set; }
    
        public virtual ICollection<SC_UserAccess> UserAccess { get; set; }
        public virtual MT_Attachment EmployeeCard { get; set; }
        public virtual MT_Attachment IDCard { get; set; }
        public virtual MT_Organization Organization { get; set; }
        public virtual MT_Province Province { get; set; }
        public virtual SC_Group Group { get; set; }
        public virtual ICollection<UserRegisterEntry> UserRegisterEntries { get; set; }
    }
}
