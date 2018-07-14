using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCOMSapps.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SmtpHost = table.Column<string>(nullable: true),
                    SmtpPort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CountyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    RoleTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CountyId = table.Column<int>(nullable: false),
                    DefaultShiftId = table.Column<int>(nullable: false),
                    DefaultStationId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    SupervisorId = table.Column<Guid>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DefaultLocation = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OosTitleEmailAddress = table.Column<string>(nullable: true),
                    OosTitlesCopyEmailAddress = table.Column<string>(nullable: true),
                    PocUserId = table.Column<string>(nullable: true),
                    SharedData = table.Column<bool>(nullable: false),
                    Theme = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentSupervisors",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false),
                    SupervisorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentSupervisors", x => new { x.DepartmentId, x.SupervisorId });
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountyId = table.Column<int>(nullable: false),
                    CustAddr1 = table.Column<string>(nullable: false),
                    CustAddr2 = table.Column<string>(nullable: true),
                    CustCity = table.Column<string>(nullable: false),
                    CustEmail = table.Column<string>(nullable: true),
                    CustFName = table.Column<string>(nullable: false),
                    CustFName2 = table.Column<string>(nullable: true),
                    CustFName3 = table.Column<string>(nullable: true),
                    CustLName = table.Column<string>(nullable: false),
                    CustLName2 = table.Column<string>(nullable: true),
                    CustLName3 = table.Column<string>(nullable: true),
                    CustPhone = table.Column<string>(nullable: true),
                    CustPhone2 = table.Column<string>(nullable: true),
                    CustState = table.Column<string>(nullable: false),
                    CustZip = table.Column<string>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDt = table.Column<DateTime>(nullable: true),
                    EntryDt = table.Column<DateTime>(nullable: false),
                    FollowUp = table.Column<bool>(nullable: false),
                    InitialLocation = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastInteraction = table.Column<int>(nullable: false),
                    LastTransfer = table.Column<int>(nullable: false),
                    NewVeh = table.Column<bool>(nullable: false),
                    RecDt = table.Column<DateTime>(nullable: false),
                    RecMethod = table.Column<int>(nullable: false),
                    SentBackToLhDlrDt = table.Column<DateTime>(nullable: true),
                    SentToDmvDt = table.Column<DateTime>(nullable: true),
                    SentUserId = table.Column<string>(nullable: true),
                    TitleIssueDt = table.Column<DateTime>(nullable: true),
                    TitleNotes = table.Column<string>(nullable: true),
                    TitleRecievedFromType = table.Column<int>(nullable: false),
                    TitleState = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    VehMake = table.Column<string>(nullable: false),
                    VehModel = table.Column<string>(nullable: false),
                    VehYr = table.Column<int>(nullable: false),
                    Vin = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
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
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppRoleId = table.Column<string>(nullable: true),
                    CountyId = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: true),
                    CreatedDt = table.Column<DateTime>(nullable: false),
                    DeletedByUserId = table.Column<Guid>(nullable: true),
                    DeletedDt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departments_AspNetUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    CountyId = table.Column<int>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    Manager = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustAddr1 = table.Column<string>(nullable: true),
                    CustAddr2 = table.Column<string>(nullable: true),
                    CustCity = table.Column<string>(nullable: true),
                    CustEmail = table.Column<string>(nullable: true),
                    CustFName = table.Column<string>(nullable: true),
                    CustFName2 = table.Column<string>(nullable: true),
                    CustFName3 = table.Column<string>(nullable: true),
                    CustLName = table.Column<string>(nullable: true),
                    CustLName2 = table.Column<string>(nullable: true),
                    CustLName3 = table.Column<string>(nullable: true),
                    CustPhone = table.Column<string>(nullable: false),
                    CustPhone2 = table.Column<string>(nullable: true),
                    CustState = table.Column<string>(nullable: true),
                    CustZip = table.Column<string>(nullable: true),
                    FollowUpDt = table.Column<DateTime>(nullable: true),
                    FollowUpNotes = table.Column<string>(nullable: true),
                    InteractionDt = table.Column<DateTime>(nullable: false),
                    InteractionType = table.Column<string>(nullable: true),
                    InteractionUserId = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    LhDlrAddr1 = table.Column<string>(nullable: true),
                    LhDlrAddr2 = table.Column<string>(nullable: true),
                    LhDlrCity = table.Column<string>(nullable: true),
                    LhDlrName = table.Column<string>(nullable: true),
                    LhDlrState = table.Column<string>(nullable: true),
                    LhDlrZip = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ReceivedDt = table.Column<DateTime>(nullable: true),
                    TitleId = table.Column<int>(nullable: false),
                    TitleIssuedDt = table.Column<DateTime>(nullable: true),
                    TitleRecievedFromType = table.Column<int>(nullable: false),
                    TitleState = table.Column<string>(nullable: true),
                    VehMake = table.Column<string>(nullable: true),
                    VehModel = table.Column<string>(nullable: true),
                    VehYr = table.Column<int>(nullable: false),
                    Vin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InRouteByUserId = table.Column<string>(nullable: true),
                    InRouteDt = table.Column<DateTime>(nullable: true),
                    IsInRoute = table.Column<bool>(nullable: false),
                    IsReceived = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Method = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ReceivedByUserId = table.Column<string>(nullable: true),
                    ReceivedDt = table.Column<DateTime>(nullable: true),
                    TitleId = table.Column<int>(nullable: false),
                    TransferRequestUserId = table.Column<string>(nullable: true),
                    TransferRequestedDt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Departments_CreatedByUserId",
                table: "Departments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DeletedByUserId",
                table: "Departments",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_TitleId",
                table: "Interactions",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CountyId",
                table: "Locations",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_LocationId",
                table: "Transfers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_TitleId",
                table: "Transfers",
                column: "TitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

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
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DepartmentSupervisors");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Counties");
        }
    }
}
