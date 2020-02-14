using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Data;
using IgnProtoView.Models;
using IgnProtoView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IgnProtoView.ViewComponents
{
    public class WeeklyReportViewComponent : ViewComponent
    {
        private IgniteContext _context;

        public WeeklyReportViewComponent(IgniteContext context)
        {
            _context = context;
        }

        public IgniteUser IgniteUser { get; set; }

        public IViewComponentResult Invoke()
        {
            var users = _context.IgniteUsers.AsNoTracking();
            var igniteUserApplications = _context.IgniteUserApplications.AsNoTracking();
            var depts = _context.Departments.AsNoTracking();

            // inner join on dept and user
            users.Include(ig => ig.Department);
            List<IgniteUser> igUsers = users.ToList();
            List<Department> departments = depts.ToList();
            IgniteUser usr = new IgniteUser();
            IgniteUserApplication app = new IgniteUserApplication();
            Department dept = new Department();

            foreach (var m in igUsers)
            {
                usr = m;
                dept = m.Department;
                igUsers.Add(usr);
                departments.Add(dept);
            }

            var joinRes = (from i in igUsers
                join d in departments on i.FKDepartmentId equals d.DepartmentId
                select new IgniteUserReportView
                {
                    AppUserId = i.Id,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName
                }).ToList();

            IgniteUserReportView vm = new IgniteUserReportView()
            {
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                LinkClicked = usr.LinkClicked,
                DepartmentId = dept.DepartmentId,
                DepartmentName = dept.DepartmentName
            };
            //var viewName = isNullOrEmptyViewName ?? DefaultViewName : viewName


            return View(igUsers);
        }
    }
}
