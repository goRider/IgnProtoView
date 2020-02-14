using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Data;
using IgnProtoView.Models;

namespace IgnProtoView.ViewModels
{
    public class IgniteUserReportView
    {
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public bool LinkClicked { get; set; }
        public DateTime ApplicationStatusCompleteDate { get; set; }
        public DateTime ManagerResponseDate { get; set; }
        public string ManagerResponse { get; set; }
        public int ApplicationId { get; set; }
        public int FkIgniteUserId { get; set; }
        public IgniteUser IgniteUser { get; set; }
        public List<IgniteUserApplication> IgniteUserApplication { get; set; }
    }
}
