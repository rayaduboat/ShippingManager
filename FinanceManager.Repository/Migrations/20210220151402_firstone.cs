using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManager.Repository.Migrations
{
    public partial class firstone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.EnsureSchema(
            //    name: "Batches");

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        Name = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
            //        ConcurrencyStamp = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        UserName = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
            //        Email = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(nullable: false),
            //        PasswordHash = table.Column<string>(nullable: true),
            //        SecurityStamp = table.Column<string>(nullable: true),
            //        ConcurrencyStamp = table.Column<string>(nullable: true),
            //        PhoneNumber = table.Column<string>(nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
            //        LockoutEnabled = table.Column<bool>(nullable: false),
            //        AccessFailedCount = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LinkCreator",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
            //        SenderId = table.Column<int>(nullable: false),
            //        BatchId = table.Column<int>(nullable: false),
            //        ShippersId = table.Column<int>(nullable: false),
            //        HashString = table.Column<string>(nullable: true),
            //        ActualString = table.Column<string>(nullable: true),
            //        LinkStatus = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
            //        ExpiryFlag = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
            //        ActualLink = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LinkCreator", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReferenceNumber",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ShippersId = table.Column<int>(nullable: false),
            //        BatchId = table.Column<int>(nullable: false),
            //        SenderId = table.Column<int>(nullable: false),
            //        RefLabel = table.Column<string>(maxLength: 50, nullable: true),
            //        StatusFlag = table.Column<string>(maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReferenceNumber", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RefId = table.Column<int>(nullable: false),
            //        shipperId = table.Column<int>(nullable: false),
            //        batchId = table.Column<int>(nullable: false),
            //        SenderId = table.Column<int>(nullable: false),
            //        RecipientId = table.Column<int>(nullable: false),
            //        OriginalStatus = table.Column<int>(nullable: true),
            //        NewStatus = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
            //        TimeOfChange = table.Column<DateTime>(type: "datetime", nullable: true),
            //        UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
            //        StatusDetails = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ClientUsage",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        SenderID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RecipientID = table.Column<int>(nullable: false),
            //        Title = table.Column<string>(maxLength: 75, nullable: true),
            //        SendName = table.Column<string>(maxLength: 75, nullable: true),
            //        RecName = table.Column<string>(maxLength: 75, nullable: true),
            //        SendTown = table.Column<string>(maxLength: 75, nullable: true),
            //        RecTown = table.Column<string>(maxLength: 75, nullable: true),
            //        NumOfTrips = table.Column<int>(nullable: true),
            //        NumOfRecipients = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Country",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CountryName = table.Column<string>(maxLength: 100, nullable: false),
            //        IsSelected = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Country", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExpenditureReport",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ExpReportID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Batch = table.Column<int>(nullable: true),
            //        Name = table.Column<string>(maxLength: 50, nullable: true),
            //        Description = table.Column<string>(maxLength: 50, nullable: true),
            //        ModeOfPayment = table.Column<string>(maxLength: 20, nullable: true),
            //        ExpenseType = table.Column<string>(maxLength: 25, nullable: true),
            //        Amount = table.Column<decimal>(type: "money", nullable: true),
            //        CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
            //        ActualBatch = table.Column<string>(maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpenditureReport", x => x.ExpReportID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExpenseItems",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(maxLength: 50, nullable: true),
            //        Description = table.Column<string>(maxLength: 50, nullable: true),
            //        IsSelected = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpenseItems", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExpensePeriod",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PERIOD = table.Column<string>(maxLength: 50, nullable: true),
            //        IsSelected = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpensePeriod", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FreightAgent",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        FreightAgentID = table.Column<int>(nullable: false),
            //        CompanyName = table.Column<string>(maxLength: 100, nullable: false),
            //        FirstName = table.Column<string>(maxLength: 25, nullable: true),
            //        LastName = table.Column<string>(maxLength: 25, nullable: true),
            //        AddressLineOne = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineTwo = table.Column<string>(maxLength: 75, nullable: true),
            //        PostCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 10, nullable: true),
            //        TownCity = table.Column<string>(maxLength: 25, nullable: true),
            //        Country = table.Column<string>(maxLength: 50, nullable: true),
            //        Telephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: true),
            //        EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
            //        WebAddress = table.Column<string>(maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FreightAgent", x => x.FreightAgentID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GhanaCities",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        City = table.Column<string>(maxLength: 50, nullable: false),
            //        IsSelected = table.Column<bool>(nullable: true),
            //        Priority = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GhanaCities", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GHCity",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false),
            //        City = table.Column<string>(maxLength: 50, nullable: false),
            //        IsSelected = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Items",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ItemID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(maxLength: 75, nullable: true),
            //        Model = table.Column<string>(maxLength: 75, nullable: true),
            //        Description = table.Column<string>(maxLength: 200, nullable: true),
            //        Colour = table.Column<string>(maxLength: 75, nullable: true),
            //        UnitCost = table.Column<decimal>(type: "money", nullable: true),
            //        Type = table.Column<string>(maxLength: 75, nullable: true),
            //        Brand = table.Column<string>(maxLength: 75, nullable: true),
            //        Specifications = table.Column<string>(maxLength: 75, nullable: true),
            //        Condition = table.Column<string>(maxLength: 75, nullable: true),
            //        Make = table.Column<string>(maxLength: 75, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Item__727E83EBCB113D7C", x => x.ItemID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "License",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        LicenseID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LicenseNumber = table.Column<string>(maxLength: 50, nullable: false),
            //        UserEmailAddress = table.Column<string>(maxLength: 50, nullable: false),
            //        LicenceStartDate = table.Column<DateTime>(type: "date", nullable: false),
            //        LicenceEndDate = table.Column<DateTime>(type: "date", nullable: false),
            //        NumberOfUsers = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_License", x => x.LicenseID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LinkCreator",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "datetime", nullable: true),
            //        SenderId = table.Column<int>(nullable: false),
            //        BatchId = table.Column<int>(nullable: false),
            //        RefNum = table.Column<int>(nullable: false),
            //        UrlLink = table.Column<string>(nullable: true),
            //        UniqueString = table.Column<string>(nullable: true),
            //        OrderStatus = table.Column<int>(nullable: true),
            //        LinkStatus = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LinkCreator", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Login",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(maxLength: 150, nullable: false),
            //        EmailAddress = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
            //        Password = table.Column<string>(fixedLength: true, maxLength: 200, nullable: false),
            //        HashedPassword = table.Column<string>(fixedLength: true, maxLength: 200, nullable: false),
            //        PasswordSalt = table.Column<string>(fixedLength: true, maxLength: 200, nullable: false),
            //        Role = table.Column<string>(fixedLength: true, maxLength: 200, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Login", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PayMode",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PAYMODE = table.Column<string>(maxLength: 50, nullable: true),
            //        IsSelected = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PayMode", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PickingAddressList",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(maxLength: 75, nullable: true),
            //        FirstName = table.Column<string>(maxLength: 75, nullable: true),
            //        LastName = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineOne = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineTwo = table.Column<string>(maxLength: 75, nullable: true),
            //        PostCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: true),
            //        PostTown = table.Column<string>(maxLength: 75, nullable: true),
            //        Country = table.Column<string>(maxLength: 75, nullable: true),
            //        Telephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 30, nullable: true),
            //        EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
            //        RegistrationDate = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PickingAddressList", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PickingAddressListRep",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(maxLength: 75, nullable: true),
            //        FirstName = table.Column<string>(maxLength: 75, nullable: true),
            //        LastName = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineOne = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineTwo = table.Column<string>(maxLength: 75, nullable: true),
            //        PostCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: true),
            //        PostTown = table.Column<string>(maxLength: 75, nullable: true),
            //        Country = table.Column<string>(maxLength: 75, nullable: true),
            //        Telephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 30, nullable: true),
            //        EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
            //        RegistrationDate = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PickingAddressListRep", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReferenceNumber",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ShippersId = table.Column<int>(nullable: false),
            //        BatchId = table.Column<int>(nullable: false),
            //        SenderId = table.Column<int>(nullable: false),
            //        RefLabel = table.Column<string>(maxLength: 50, nullable: true),
            //        StatusFlag = table.Column<string>(maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReferenceNumber", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SenderRecipients",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        RecipientID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SenderID = table.Column<int>(nullable: false),
            //        Title = table.Column<string>(maxLength: 75, nullable: true),
            //        FirstName = table.Column<string>(maxLength: 75, nullable: true),
            //        LastName = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineOne = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineTwo = table.Column<string>(maxLength: 75, nullable: true),
            //        PostCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: true),
            //        PostTown = table.Column<string>(maxLength: 75, nullable: true),
            //        County = table.Column<string>(fixedLength: true, maxLength: 75, nullable: true),
            //        Country = table.Column<string>(maxLength: 75, nullable: true),
            //        Telephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 30, nullable: true),
            //        EmailAddress = table.Column<string>(maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SenderRecipients", x => x.RecipientID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripAudit",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        shipperId = table.Column<int>(nullable: false),
            //        batchId = table.Column<int>(nullable: false),
            //        SenderId = table.Column<int>(nullable: false),
            //        RecipientId = table.Column<int>(nullable: false),
            //        OriginalStatus = table.Column<int>(nullable: true),
            //        NewStatus = table.Column<int>(nullable: true),
            //        TimeOfChange = table.Column<DateTime>(type: "datetime", nullable: true),
            //        UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
            //        StatusDetails = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripListReport",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        TripReportID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ActualBatch = table.Column<string>(maxLength: 20, nullable: true),
            //        ActualRef = table.Column<string>(maxLength: 20, nullable: true),
            //        ItemID = table.Column<int>(nullable: false),
            //        SenderName = table.Column<string>(maxLength: 50, nullable: false),
            //        RecipientName = table.Column<string>(maxLength: 50, nullable: false),
            //        BatchID = table.Column<int>(nullable: false),
            //        ItemName = table.Column<string>(maxLength: 50, nullable: true),
            //        Comment = table.Column<string>(maxLength: 50, nullable: true),
            //        TelSender = table.Column<string>(maxLength: 50, nullable: true),
            //        TelRecipient = table.Column<string>(maxLength: 50, nullable: true),
            //        SendTown = table.Column<string>(maxLength: 50, nullable: true),
            //        RecTown = table.Column<string>(maxLength: 50, nullable: true),
            //        Quantity = table.Column<int>(nullable: true),
            //        Total = table.Column<decimal>(type: "money", nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripListReport", x => x.TripReportID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UKCity",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false),
            //        City = table.Column<string>(maxLength: 50, nullable: false),
            //        IsSelected = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UKCity", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Username = table.Column<string>(nullable: false),
            //        Password = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleId = table.Column<string>(nullable: false),
            //        ClaimType = table.Column<string>(nullable: true),
            //        ClaimValue = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(nullable: false),
            //        ClaimType = table.Column<string>(nullable: true),
            //        ClaimValue = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
            //        ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
            //        ProviderDisplayName = table.Column<string>(nullable: true),
            //        UserId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        RoleId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
            //        Name = table.Column<string>(maxLength: 128, nullable: false),
            //        Value = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Shippers",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ShippersID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LicenseID = table.Column<int>(nullable: false),
            //        Title = table.Column<string>(maxLength: 75, nullable: true),
            //        FirstName = table.Column<string>(maxLength: 75, nullable: true),
            //        LastName = table.Column<string>(maxLength: 75, nullable: true),
            //        DOB = table.Column<DateTime>(type: "date", nullable: true),
            //        CompanyName = table.Column<string>(maxLength: 75, nullable: true),
            //        VATNumber = table.Column<string>(maxLength: 75, nullable: false),
            //        CompanyNumber = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineOne = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineTwo = table.Column<string>(maxLength: 75, nullable: true),
            //        PostTown = table.Column<string>(maxLength: 75, nullable: true),
            //        County = table.Column<string>(maxLength: 75, nullable: true),
            //        PostCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: true),
            //        Country = table.Column<string>(maxLength: 75, nullable: true),
            //        Telephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 30, nullable: true),
            //        EmailAddress = table.Column<string>(maxLength: 75, nullable: true),
            //        Password = table.Column<string>(nullable: true),
            //        WebAddress = table.Column<string>(maxLength: 100, nullable: true),
            //        Code = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Shippers", x => x.ShippersID);
            //        table.ForeignKey(
            //            name: "FK_Shippers_License",
            //            column: x => x.LicenseID,
            //            principalSchema: "Batches",
            //            principalTable: "License",
            //            principalColumn: "LicenseID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Batch",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        BatchID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreatedOnDate = table.Column<DateTime>(type: "date", nullable: false),
            //        CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
            //        ShippersID = table.Column<int>(nullable: false),
            //        ActualBatch = table.Column<string>(maxLength: 50, nullable: false),
            //        IsSelected = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Batch", x => x.BatchID);
            //        table.ForeignKey(
            //            name: "FK_Batch_Shippers",
            //            column: x => x.ShippersID,
            //            principalSchema: "Batches",
            //            principalTable: "Shippers",
            //            principalColumn: "ShippersID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BatchLabel",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        BatchLabelID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ShippersID = table.Column<int>(nullable: false),
            //        CreatedOnDate = table.Column<DateTime>(type: "date", nullable: false),
            //        LabelCode = table.Column<string>(maxLength: 10, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BatchLabel", x => x.BatchLabelID);
            //        table.ForeignKey(
            //            name: "FK_BatchLabel_Shippers",
            //            column: x => x.ShippersID,
            //            principalSchema: "Batches",
            //            principalTable: "Shippers",
            //            principalColumn: "ShippersID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Senders",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        SenderID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ShippersID = table.Column<int>(nullable: false),
            //        Title = table.Column<string>(maxLength: 75, nullable: true),
            //        FirstName = table.Column<string>(maxLength: 75, nullable: false),
            //        LastName = table.Column<string>(maxLength: 75, nullable: false),
            //        Gender = table.Column<string>(maxLength: 50, nullable: true),
            //        AddressLineOne = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineTwo = table.Column<string>(maxLength: 75, nullable: true),
            //        PostCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: false),
            //        PostTown = table.Column<string>(maxLength: 75, nullable: true),
            //        County = table.Column<string>(fixedLength: true, maxLength: 75, nullable: true),
            //        Country = table.Column<string>(maxLength: 75, nullable: true),
            //        Telephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 30, nullable: false),
            //        EmailAddress = table.Column<string>(maxLength: 100, nullable: false),
            //        Password = table.Column<string>(maxLength: 250, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Sender__BB4991ABD1AAA538", x => x.SenderID);
            //        table.ForeignKey(
            //            name: "FK_Senders_Shippers",
            //            column: x => x.ShippersID,
            //            principalSchema: "Batches",
            //            principalTable: "Shippers",
            //            principalColumn: "ShippersID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Expenditure",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ExpenditureID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BatchID = table.Column<int>(nullable: true),
            //        Name = table.Column<string>(maxLength: 50, nullable: true),
            //        Description = table.Column<string>(maxLength: 50, nullable: true),
            //        ModeOfPayment = table.Column<string>(maxLength: 20, nullable: true),
            //        ExpenseType = table.Column<string>(maxLength: 25, nullable: true),
            //        Amount = table.Column<decimal>(type: "money", nullable: true),
            //        IsReceiptIssued = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: true),
            //        CreatedDate = table.Column<DateTime>(type: "date", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Expenditure", x => x.ExpenditureID);
            //        table.ForeignKey(
            //            name: "FK_Expenditure_Batch",
            //            column: x => x.BatchID,
            //            principalSchema: "Batches",
            //            principalTable: "Batch",
            //            principalColumn: "BatchID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Income",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        IncomeID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BatchID = table.Column<int>(nullable: true),
            //        ModeOfPayment = table.Column<string>(maxLength: 20, nullable: true),
            //        ActualRef = table.Column<string>(maxLength: 20, nullable: true),
            //        Amount = table.Column<decimal>(type: "money", nullable: true),
            //        CreatedDate = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Income", x => x.IncomeID);
            //        table.ForeignKey(
            //            name: "FK_Income_Batch",
            //            column: x => x.BatchID,
            //            principalSchema: "Batches",
            //            principalTable: "Batch",
            //            principalColumn: "BatchID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Performance",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BatchID = table.Column<int>(nullable: true),
            //        AccountYear = table.Column<string>(maxLength: 50, nullable: true),
            //        AccountMonth = table.Column<string>(maxLength: 50, nullable: true),
            //        AcountDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        AccountPeriod = table.Column<int>(nullable: true),
            //        TotalIncome = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
            //        TotalExpenditure = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
            //        AditionalIncome = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
            //        ProfitLoss = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
            //        ProfitLossPercent = table.Column<decimal>(type: "decimal(18, 0)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Performance", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Performance_Batch",
            //            column: x => x.BatchID,
            //            principalSchema: "Batches",
            //            principalTable: "Batch",
            //            principalColumn: "BatchID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Recipients",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        RecipientID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SenderID = table.Column<int>(nullable: false),
            //        Title = table.Column<string>(maxLength: 75, nullable: true),
            //        FirstName = table.Column<string>(maxLength: 75, nullable: true),
            //        LastName = table.Column<string>(maxLength: 75, nullable: true),
            //        Gender = table.Column<string>(maxLength: 50, nullable: true),
            //        AddressLineOne = table.Column<string>(maxLength: 75, nullable: true),
            //        AddressLineTwo = table.Column<string>(maxLength: 75, nullable: true),
            //        PostCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 20, nullable: true),
            //        PostTown = table.Column<string>(maxLength: 75, nullable: true),
            //        County = table.Column<string>(fixedLength: true, maxLength: 75, nullable: true),
            //        Country = table.Column<string>(maxLength: 75, nullable: true),
            //        Telephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 30, nullable: true),
            //        EmailAddress = table.Column<string>(maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Receiver__FEBB5F072C82413E", x => x.RecipientID);
            //        table.ForeignKey(
            //            name: "FK_Recipients_Senders",
            //            column: x => x.SenderID,
            //            principalSchema: "Batches",
            //            principalTable: "Senders",
            //            principalColumn: "SenderID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripDetails",
            //    schema: "Batches",
            //    columns: table => new
            //    {
            //        TripID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ActualRef = table.Column<string>(maxLength: 20, nullable: true),
            //        ItemID = table.Column<int>(nullable: true),
            //        SenderID = table.Column<int>(nullable: false),
            //        RecipientID = table.Column<int>(nullable: false),
            //        BatchID = table.Column<int>(nullable: false),
            //        Name = table.Column<string>(maxLength: 50, nullable: true),
            //        Comment = table.Column<string>(maxLength: 50, nullable: true),
            //        Quantity = table.Column<int>(nullable: true),
            //        Total = table.Column<decimal>(type: "money", nullable: true),
            //        GrandTotal = table.Column<decimal>(type: "money", nullable: true),
            //        OrderDate = table.Column<DateTime>(type: "date", nullable: true),
            //        Status = table.Column<string>(maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripDetails", x => x.TripID);
            //        table.ForeignKey(
            //            name: "FK_TripDetails_Batch",
            //            column: x => x.BatchID,
            //            principalSchema: "Batches",
            //            principalTable: "Batch",
            //            principalColumn: "BatchID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_TripDetails_Items",
            //            column: x => x.ItemID,
            //            principalSchema: "Batches",
            //            principalTable: "Items",
            //            principalColumn: "ItemID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_TripDetails_Recipients",
            //            column: x => x.RecipientID,
            //            principalSchema: "Batches",
            //            principalTable: "Recipients",
            //            principalColumn: "RecipientID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_TripDetails_Senders",
            //            column: x => x.SenderID,
            //            principalSchema: "Batches",
            //            principalTable: "Senders",
            //            principalColumn: "SenderID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "AspNetRoles",
            //    column: "NormalizedName",
            //    unique: true,
            //    filter: "([NormalizedName] IS NOT NULL)");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "([NormalizedUserName] IS NOT NULL)");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Batch_ShippersID",
            //    schema: "Batches",
            //    table: "Batch",
            //    column: "ShippersID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BatchLabel_ShippersID",
            //    schema: "Batches",
            //    table: "BatchLabel",
            //    column: "ShippersID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Expenditure_BatchID",
            //    schema: "Batches",
            //    table: "Expenditure",
            //    column: "BatchID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Income_BatchID",
            //    schema: "Batches",
            //    table: "Income",
            //    column: "BatchID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Performance_BatchID",
            //    schema: "Batches",
            //    table: "Performance",
            //    column: "BatchID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Recipients_SenderID",
            //    schema: "Batches",
            //    table: "Recipients",
            //    column: "SenderID");

            //migrationBuilder.CreateIndex(
            //    name: "NonClusteredIndex-20140216-190105",
            //    schema: "Batches",
            //    table: "Recipients",
            //    columns: new[] { "FirstName", "LastName", "Telephone", "RecipientID" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Senders_ShippersID",
            //    schema: "Batches",
            //    table: "Senders",
            //    column: "ShippersID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Shippers_LicenseID",
            //    schema: "Batches",
            //    table: "Shippers",
            //    column: "LicenseID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TripDetails_BatchID",
            //    schema: "Batches",
            //    table: "TripDetails",
            //    column: "BatchID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TripDetails_ItemID",
            //    schema: "Batches",
            //    table: "TripDetails",
            //    column: "ItemID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TripDetails_RecipientID",
            //    schema: "Batches",
            //    table: "TripDetails",
            //    column: "RecipientID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TripDetails_SenderID",
            //    schema: "Batches",
            //    table: "TripDetails",
            //    column: "SenderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AspNetRoleClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserLogins");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "LinkCreator");

            //migrationBuilder.DropTable(
            //    name: "ReferenceNumber");

            //migrationBuilder.DropTable(
            //    name: "TripAudit");

            //migrationBuilder.DropTable(
            //    name: "BatchLabel",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "ClientUsage",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Country",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Expenditure",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "ExpenditureReport",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "ExpenseItems",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "ExpensePeriod",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "FreightAgent",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "GhanaCities",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "GHCity",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Income",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "LinkCreator",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Login",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "PayMode",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Performance",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "PickingAddressList",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "PickingAddressListRep",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "ReferenceNumber",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "SenderRecipients",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "TripAudit",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "TripDetails",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "TripListReport",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "UKCity",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Users",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "Batch",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Items",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Recipients",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Senders",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "Shippers",
            //    schema: "Batches");

            //migrationBuilder.DropTable(
            //    name: "License",
            //    schema: "Batches");
        }
    }
}
