using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Models;
using Microsoft.AspNetCore.Identity;

namespace IgnProtoView.Data
{
    public class IgniteUser : IdentityUser<int>
    {
        public IgniteUser() : base()
        {
            //
        }

        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string FirstNameLastName { get; set; }
        public string IgniteEmail { get; set; }

        public bool IsInternalUser { get; set; }

        //
        public bool LinkClicked { get; set; }

        public bool WorkedOverOneYear { get; set; }
        public bool IsQualifiedForLongTermEmp { get; set; }
        public bool CompleteUndergraduate { get; set; }
        // Remove this field IsApplicationFilled as this will be tracked on IgniteApplication not the User
        public bool IsApplicationFilled { get; set; }
        public bool PreQualificationAccepted { get; set; }
        // Remove HRValued as Application Status Selected Tracks application status of Employees Chosen by HR out of all Endorsed Candidates
        //public bool HRApproved { get; set; }

        public DateTime HiredDate { get; set; }
        public DateTime? TermDate { get; set; }

        // Marks the date that the Manager marks you as qualified to get an application for qualification
        public DateTime? ApplicationApprovalDate { get; set; }
        //Use this field to determine if user is eligible to receive the application
        public bool EligibleForQualification { get; set; }

        // NAV
        public ICollection<IgniteUserApplication> IgniteUserApplications { get; set; }
        public Department Department { get; set; }
        public int FKDepartmentId { get; set; }
        public Title UserTitle { get; set; }
        public int FKTitleId { get; set; }
        public int FKLocationId { get; set; }
        public Location UserLocation { get; set; }
        public BusinessUnit BU { get; set; }
        public int FKBUID { get; set; }
        public IgniteUserType IgniteUserType { get; set; }
        public int FkIgniteUserTypeId { get; set; }

        // Identity User Navigation
        public virtual ICollection<IdentityUserRole<int>> Roles { get; } = new List<IdentityUserRole<int>>();

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; } = new List<IdentityUserClaim<int>>();

        public virtual ICollection<IdentityUserLogin<int>> Logins { get; } = new List<IdentityUserLogin<int>>();
    }
}
