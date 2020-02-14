using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IgnProtoView.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUnits",
                columns: table => new
                {
                    BUID = table.Column<int>(nullable: false),
                    BusinessUnitName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnits", x => x.BUID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "IgniteUserType",
                columns: table => new
                {
                    IgniteUserTypeId = table.Column<int>(nullable: false),
                    IgniteUserTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgniteUserType", x => x.IgniteUserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false),
                    CityLocation = table.Column<string>(nullable: true),
                    StateLocation = table.Column<string>(nullable: true),
                    CountryLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    TitleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TitleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleInitial = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FirstNameLastName = table.Column<string>(nullable: true),
                    IgniteEmail = table.Column<string>(nullable: true),
                    IsInternalUser = table.Column<bool>(nullable: false),
                    WorkedOverOneYear = table.Column<bool>(nullable: false),
                    IsQualifiedForLongTermEmp = table.Column<bool>(nullable: false),
                    CompleteUndergraduate = table.Column<bool>(nullable: false),
                    IsApplicationFilled = table.Column<bool>(nullable: false),
                    PreQualificationAccepted = table.Column<bool>(nullable: false),
                    HiredDate = table.Column<DateTime>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: true),
                    ApplicationApprovalDate = table.Column<DateTime>(nullable: true),
                    EligibleForQualification = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    FKDepartmentId = table.Column<int>(nullable: false, defaultValueSql: "2"),
                    FKTitleId = table.Column<int>(nullable: false, defaultValueSql: "8"),
                    FKLocationId = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    FKBUID = table.Column<int>(nullable: false, defaultValueSql: "5"),
                    FkIgniteUserTypeId = table.Column<int>(nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_BusinessUnits_FKBUID",
                        column: x => x.FKBUID,
                        principalTable: "BusinessUnits",
                        principalColumn: "BUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_FKDepartmentId",
                        column: x => x.FKDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Locations_FKLocationId",
                        column: x => x.FKLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Titles_FKTitleId",
                        column: x => x.FKTitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_IgniteUserType_FkIgniteUserTypeId",
                        column: x => x.FkIgniteUserTypeId,
                        principalTable: "IgniteUserType",
                        principalColumn: "IgniteUserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IgniteUserApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ManagerName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    BuName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    EmploymentOverOneYear = table.Column<bool>(nullable: false),
                    LongTermEmploymentEligibility = table.Column<bool>(nullable: false),
                    BachelorDegreeQualified = table.Column<bool>(nullable: false),
                    ApplicationCompletionDate = table.Column<DateTime>(nullable: false),
                    ManagerApplicationStatusChangeDate = table.Column<DateTime>(nullable: false),
                    UserApplicationCreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsQualificationQuestionComplete = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    FKApplicationStatusId = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    ApplicationStatusStatusId = table.Column<int>(nullable: true),
                    FkIgniteUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgniteUserApplications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_IgniteUserApplications_ApplicationStatuses_ApplicationStatusStatusId",
                        column: x => x.ApplicationStatusStatusId,
                        principalTable: "ApplicationStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IgniteUserApplications_AspNetUsers_FkIgniteUserId",
                        column: x => x.FkIgniteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsToAnswers",
                columns: table => new
                {
                    QuestionAnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstQuestion = table.Column<string>(nullable: true),
                    SecondQuestion = table.Column<string>(nullable: true),
                    ThirdQuestion = table.Column<string>(nullable: true),
                    FourthQuestion = table.Column<string>(nullable: true),
                    FirstAnswer = table.Column<string>(nullable: true),
                    SecondAnswer = table.Column<string>(nullable: true),
                    ThirdAnswer = table.Column<string>(nullable: true),
                    FourthAnswer = table.Column<string>(nullable: true),
                    CompleteQuestionsDate = table.Column<DateTime>(nullable: true),
                    FkIgniteApplicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsToAnswers", x => x.QuestionAnswerId);
                    table.ForeignKey(
                        name: "FK_QuestionsToAnswers_IgniteUserApplications_FkIgniteApplicationId",
                        column: x => x.FkIgniteApplicationId,
                        principalTable: "IgniteUserApplications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationStatuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "No App" },
                    { 2, "Not Started" },
                    { 3, "Incomplete Prequalification" },
                    { 4, "Incomplete Prequalification" },
                    { 5, "Incomplete Qualification" },
                    { 6, "Complete Qualification" },
                    { 7, "Endorsed" },
                    { 8, "Hold" },
                    { 9, "Selected" }
                });

            migrationBuilder.InsertData(
                table: "BusinessUnits",
                columns: new[] { "BUID", "BusinessUnitName" },
                values: new object[,]
                {
                    { 5, "N/A" },
                    { 4, "CX" },
                    { 3, "MM/Auto" },
                    { 2, "MGE" },
                    { 1, "Corporate" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 0, "N/A" },
                    { 1, "App Dev" },
                    { 2, "Behavioral Science and Innovation" },
                    { 3, "Creative Services" },
                    { 4, "Creative QA" },
                    { 5, "Finance Systems" },
                    { 6, "Ford" },
                    { 7, "Insurance and Card" }
                });

            migrationBuilder.InsertData(
                table: "IgniteUserType",
                columns: new[] { "IgniteUserTypeId", "IgniteUserTypeName" },
                values: new object[,]
                {
                    { 4, "Regular Employee" },
                    { 3, "Manager" },
                    { 1, "Admin" },
                    { 0, "N/A" },
                    { 2, "HR Lead" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "CityLocation", "CountryLocation", "StateLocation" },
                values: new object[,]
                {
                    { 0, "N/A", null, "N/A" },
                    { 1, "Fenton", "United States", "MO" },
                    { 2, "Maumee", "United States", "OH" },
                    { 3, "Twinsburg", "United States", "OH" },
                    { 4, "Lehi", "United States", "UT" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { 15, "Vice President" },
                    { 14, "Director" },
                    { 13, "Network Engineer (Sr)" },
                    { 12, "Graphics Designer" },
                    { 11, "Database Admin II" },
                    { 10, "Business Analyst II" },
                    { 9, "Frontend Developer" },
                    { 5, "Spec Developer" },
                    { 7, "Director" },
                    { 6, "Business Analyst I" },
                    { 4, "Lead Developer" },
                    { 3, "IT Developer Analyst II" },
                    { 2, "IT Developer Analyst I" },
                    { 1, "IT Developer Analyst I" },
                    { 8, "N/A" },
                    { 16, "Owner" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKBUID",
                table: "AspNetUsers",
                column: "FKBUID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKDepartmentId",
                table: "AspNetUsers",
                column: "FKDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKLocationId",
                table: "AspNetUsers",
                column: "FKLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKTitleId",
                table: "AspNetUsers",
                column: "FKTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FkIgniteUserTypeId",
                table: "AspNetUsers",
                column: "FkIgniteUserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IgniteEmail",
                table: "AspNetUsers",
                column: "IgniteEmail",
                unique: true,
                filter: "[IgniteEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IgniteUserApplications_ApplicationStatusStatusId",
                table: "IgniteUserApplications",
                column: "ApplicationStatusStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_IgniteUserApplications_FkIgniteUserId",
                table: "IgniteUserApplications",
                column: "FkIgniteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsToAnswers_FkIgniteApplicationId",
                table: "QuestionsToAnswers",
                column: "FkIgniteApplicationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "QuestionsToAnswers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "IgniteUserApplications");

            migrationBuilder.DropTable(
                name: "ApplicationStatuses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BusinessUnits");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "IgniteUserType");
        }
    }
}
