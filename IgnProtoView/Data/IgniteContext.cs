using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IgnProtoView.Data
{
    public class IgniteContext : IdentityDbContext<IgniteUser, IgniteRole, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IgniteContext(DbContextOptions<IgniteContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<IgniteUser> IgniteUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<IgniteUserApplication> IgniteUserApplications { get; set; }
        public DbSet<QuestionToAnswer> QuestionsToAnswers { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            #region Database Structure

            
            #region Identity model for Claims for User
            //builder.Entity
            #endregion

            #region IgniteUser

            builder.Entity<IgniteUser>(e =>
            {
                e.HasIndex(i => i.IgniteEmail).IsUnique();
                e.HasIndex(i => i.Email).IsUnique();

                // Set Default
                e.Property(p => p.FKDepartmentId).HasDefaultValueSql("2");
                e.Property(p => p.FKTitleId).HasDefaultValueSql("8");
                e.Property(p => p.FKLocationId).HasDefaultValueSql("0");
                e.Property(p => p.FKBUID).HasDefaultValueSql("5");
                e.Property(p => p.FkIgniteUserTypeId).HasDefaultValueSql("0");
                e.Property(p => p.EligibleForQualification).HasDefaultValueSql("0");

                e.HasOne<Department>(i => i.Department).WithMany(d => d.IgniteUsers).HasForeignKey(f => f.FKDepartmentId);

                e.HasOne<IgniteUserType>(it => it.IgniteUserType).WithMany(ig => ig.IgniteUsers).HasForeignKey(f => f.FkIgniteUserTypeId);

                e.HasOne(l => l.UserLocation).WithMany(iu => iu.IgniteUsers).HasForeignKey(f => f.FKLocationId);


                #region Identity model for Claims for User
                e.HasMany(c => c.Claims).WithOne().HasForeignKey(f => f.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                e.HasMany(l => l.Logins).WithOne().HasForeignKey(f => f.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                e.HasMany(i => i.Roles).WithOne().HasForeignKey(f => f.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                #endregion
            });

            #endregion

            #region Ignite User Type
            builder.Entity<IgniteUserType>(e =>
            {
                e.HasKey(k => k.IgniteUserTypeId);
                e.Property(p => p.IgniteUserTypeId).ValueGeneratedNever();

                #region Ignite Type Seed Data
                e.HasData(
                    new IgniteUserType
                    {
                        IgniteUserTypeId = 0,
                        IgniteUserTypeName = "N/A"
                    },
                    new IgniteUserType
                    {
                        IgniteUserTypeId = 1,
                        IgniteUserTypeName = "Admin"
                    },
                    new IgniteUserType
                    {
                        IgniteUserTypeId = 2,
                        IgniteUserTypeName = "HR Lead"
                    },
                    new IgniteUserType
                    {
                        IgniteUserTypeId = 3,
                        IgniteUserTypeName = "Manager"
                    },
                    new IgniteUserType
                    {
                        IgniteUserTypeId = 4,
                        IgniteUserTypeName = "Regular Employee"
                    }); ;
                #endregion
            });
            #endregion

            #region BusinessUnit

            builder.Entity<BusinessUnit>(e =>
            {
                e.HasKey(k => k.BUID);
                e.Property(p => p.BUID).ValueGeneratedNever();

                e.HasMany(iu => iu.IgniteUsers).WithOne(bu => bu.BU).HasForeignKey(fk => fk.FKBUID);

                #region Business Unit Seed Data

                e.HasData(
                new BusinessUnit
                {
                    BUID = 1,
                    BusinessUnitName = "Corporate"
                },
                new BusinessUnit
                {
                    BUID = 2,
                    BusinessUnitName = "MGE"
                },
                new BusinessUnit
                {
                    BUID = 3,
                    BusinessUnitName = "MM/Auto"
                },
                new BusinessUnit
                {
                    BUID = 4,
                    BusinessUnitName = "CX"
                },
                new BusinessUnit
                {
                    BUID = 5,
                    BusinessUnitName = "N/A"
                });

                #endregion
            });
            #endregion

            #region Department

            builder.Entity<Department>(e =>
            {
                e.HasKey(k => k.DepartmentId);
                e.Property(p => p.DepartmentId).ValueGeneratedNever();

                #region Department Seed Data
                //add Department Values
                e.HasData(
                new Department
                {
                    DepartmentId = 0,
                    DepartmentName = "N/A"
                },
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "App Dev"
                },
                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "Behavioral Science and Innovation"
                },
                new Department
                {
                    DepartmentId = 3,
                    DepartmentName = "Creative Services"
                },
                new Department
                {
                    DepartmentId = 4,
                    DepartmentName = "Creative QA"
                },
                new Department
                {
                    DepartmentId = 5,
                    DepartmentName = "Finance Systems"
                },
                new Department
                {
                    DepartmentId = 6,
                    DepartmentName = "Ford"
                },
                new Department
                {
                    DepartmentId = 7,
                    DepartmentName = "Insurance and Card"
                });
                #endregion
            });

            #endregion

            #region Location

            builder.Entity<Location>(e =>
            {
                e.HasKey(k => k.LocationId);
                e.Property(p => p.LocationId).ValueGeneratedNever();

                #region Location Seed Data

                e.HasData(
                new Location
                {
                    LocationId = 0,
                    CityLocation = "N/A",
                    StateLocation = "N/A"
                },
                new Location
                {
                    LocationId = 1,
                    CityLocation = "Fenton",
                    StateLocation = "MO",
                    CountryLocation = "United States"
                },
                new Location
                {
                    LocationId = 2,
                    CityLocation = "Maumee",
                    StateLocation = "OH",
                    CountryLocation = "United States"
                },
                new Location
                {
                    LocationId = 3,
                    CityLocation = "Twinsburg",
                    StateLocation = "OH",
                    CountryLocation = "United States"
                },
                new Location
                {
                    LocationId = 4,
                    CityLocation = "Lehi",
                    StateLocation = "UT",
                    CountryLocation = "United States"
                });
                #endregion
            });
            #endregion

            #region Title

            builder.Entity<Title>(e =>
            {
                e.HasKey(k => k.TitleId);

                e.HasMany(iu => iu.IgniteUsers).WithOne(t => t.UserTitle).HasForeignKey(f => f.FKTitleId);

                #region Title Seed Data

                e.HasData(
                new Title
                {
                    TitleId = 1,
                    TitleName = "IT Developer Analyst I"
                },
                new Title
                {
                    TitleId = 2,
                    TitleName = "IT Developer Analyst I"
                },
                new Title
                {
                    TitleId = 3,
                    TitleName = "IT Developer Analyst II"
                },
                new Title
                {
                    TitleId = 4,
                    TitleName = "Lead Developer"
                },
                new Title
                {
                    TitleId = 5,
                    TitleName = "Spec Developer"
                },
                new Title
                {
                    TitleId = 6,
                    TitleName = "Business Analyst I"
                },
                new Title
                {
                    TitleId = 7,
                    TitleName = "Director"
                },
                new Title
                {
                    TitleId = 8,
                    TitleName = "N/A"
                },
                new Title
                {
                    TitleId = 9,
                    TitleName = "Frontend Developer"
                },
                new Title
                {
                    TitleId = 10,
                    TitleName = "Business Analyst II"
                },
                new Title
                {
                    TitleId = 11,
                    TitleName = "Database Admin II"
                },
                new Title
                {
                    TitleId = 12,
                    TitleName = "Graphics Designer"
                },
                new Title
                {
                    TitleId = 13,
                    TitleName = "Network Engineer (Sr)"
                },
                new Title
                {
                    TitleId = 14,
                    TitleName = "Director"
                },
                new Title
                {
                    TitleId = 15,
                    TitleName = "Vice President"
                },
                new Title
                {
                    TitleId = 16,
                    TitleName = "Owner"
                });

                #endregion
            });

            #endregion

            #region Ignite User Application

            builder.Entity<IgniteUserApplication>(e =>
            {
                e.HasKey(k => k.ApplicationId);
                e.Property(p => p.ApplicationId).ValueGeneratedOnAdd();
                e.Property(p => p.FKApplicationStatusId).HasDefaultValueSql("1");
                e.Property(p => p.IsQualificationQuestionComplete).HasDefaultValueSql("0");
                e.Property(p => p.UserApplicationCreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                //e.HasOne(q => q.QuestionToAnswer).WithOne(ua => ua.IgniteUserApplication)
                //    .HasForeignKey<IgniteUserApplication>(f => f.FkQuestionToAnswerId).OnDelete(DeleteBehavior.Cascade);

                e.HasOne(iu => iu.IgniteUser).WithMany(ua => ua.IgniteUserApplications)
                    .HasForeignKey(f => f.FkIgniteUserId).OnDelete(DeleteBehavior.Cascade);
            });

            #endregion


            #region Question and Answer

            builder.Entity<QuestionToAnswer>(e =>
            {
                e.HasKey(k => k.QuestionAnswerId);
                e.Property(p => p.QuestionAnswerId).ValueGeneratedOnAdd();

                e.HasOne(ua => ua.IgniteUserApplication).WithOne(q => q.QuestionToAnswer).HasForeignKey<QuestionToAnswer>(f => f.FkIgniteApplicationId).OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Application Status

            builder.Entity<ApplicationStatus>(e =>
            {
                e.HasKey(k => k.StatusId);
                e.Property(p => p.StatusId).ValueGeneratedNever();

                #region Application Status Seed Data

                e.HasData(
                new ApplicationStatus
                {
                    StatusId = 1,
                    StatusName = Models.Utility.IgniteApplicationStatus.NoApplication,
                },
                new ApplicationStatus
                {
                    StatusId = 2,
                    StatusName = Models.Utility.IgniteApplicationStatus.NotStarted,
                },
                new ApplicationStatus
                {
                    StatusId = 3,
                    StatusName = Models.Utility.IgniteApplicationStatus.IncompletePreQual,
                },
                new ApplicationStatus
                {
                    StatusId = 4,
                    StatusName = Models.Utility.IgniteApplicationStatus.CompletePreQual,
                },
                new ApplicationStatus
                {
                    StatusId = 5,
                    StatusName = Models.Utility.IgniteApplicationStatus.IncompleteQual,
                },
                new ApplicationStatus
                {
                    StatusId = 6,
                    StatusName = Models.Utility.IgniteApplicationStatus.CompleteQual,
                },
                new ApplicationStatus
                {
                    StatusId = 7,
                    StatusName = Models.Utility.IgniteApplicationStatus.Endorsed,
                },
                new ApplicationStatus
                {
                    StatusId = 8,
                    StatusName = Models.Utility.IgniteApplicationStatus.Hold,
                },
                new ApplicationStatus
                {
                    StatusId = 9,
                    StatusName = Models.Utility.IgniteApplicationStatus.SelectedUser,
                });
                #endregion
            });

            #endregion

            #endregion
        }
    }
}
