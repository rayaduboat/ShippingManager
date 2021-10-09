using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Repository
{
    public partial class FinanceManagerDbContext : DbContext
    {
        public FinanceManagerDbContext()
        {
        }

        public FinanceManagerDbContext(DbContextOptions<FinanceManagerDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<IdentityUser> IdentityUser { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Batch> Batch { get; set; }
        public virtual DbSet<BatchLabel> BatchLabel { get; set; }
        public virtual DbSet<ClientUsage> ClientUsage { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Expenditure> Expenditure { get; set; }
        public virtual DbSet<ExpenditureReport> ExpenditureReport { get; set; }
        public virtual DbSet<ExpenseItems> ExpenseItems { get; set; }
        public virtual DbSet<ExpensePeriod> ExpensePeriod { get; set; }
        public virtual DbSet<FreightAgent> FreightAgent { get; set; }
        public virtual DbSet<GhanaCities> GhanaCities { get; set; }
        public virtual DbSet<Ghcity> Ghcity { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<License> License { get; set; }
        public virtual DbSet<LinkCreator> LinkCreator { get; set; }
        public virtual DbSet<LinkCreator1> LinkCreator1 { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<PayMode> PayMode { get; set; }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<PickingAddressList> PickingAddressList { get; set; }
        public virtual DbSet<PickingAddressListRep> PickingAddressListRep { get; set; }
        public virtual DbSet<Recipients> Recipients { get; set; }
        public virtual DbSet<ReferenceNumber> ReferenceNumber { get; set; }
        public virtual DbSet<ReferenceNumber1> ReferenceNumber1 { get; set; }
        public virtual DbSet<SenderRecipient> SenderRecipient { get; set; }
        public virtual DbSet<SenderRecipients> SenderRecipients { get; set; }
        public virtual DbSet<Senders> Senders { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<TripAudit> TripAudit { get; set; }
        public virtual DbSet<TripDetails> TripDetails { get; set; }
        public virtual DbSet<TripListReport> TripListReport { get; set; }
        public virtual DbSet<Ukcity> Ukcity { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VwSenderRecipientData> VwSenderRecipientData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=sql6007.site4now.net;Initial Catalog=DB_A612B8_gyawu;User Id=DB_A612B8_gyawu_admin;Password=Ray4455rab!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch", "Batches");

                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.ActualBatch)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.ShippersId).HasColumnName("ShippersID");

                entity.HasOne(d => d.Shippers)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.ShippersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batch_Shippers");
            });

            modelBuilder.Entity<BatchLabel>(entity =>
            {
                entity.ToTable("BatchLabel", "Batches");

                entity.Property(e => e.BatchLabelId).HasColumnName("BatchLabelID");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.LabelCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ShippersId).HasColumnName("ShippersID");

                entity.HasOne(d => d.Shippers)
                    .WithMany(p => p.BatchLabel)
                    .HasForeignKey(d => d.ShippersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchLabel_Shippers");
            });

            modelBuilder.Entity<ClientUsage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClientUsage", "Batches");

                entity.Property(e => e.RecName).HasMaxLength(75);

                entity.Property(e => e.RecTown).HasMaxLength(75);

                entity.Property(e => e.RecipientId).HasColumnName("RecipientID");

                entity.Property(e => e.SendName).HasMaxLength(75);

                entity.Property(e => e.SendTown).HasMaxLength(75);

                entity.Property(e => e.SenderId)
                    .HasColumnName("SenderID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title).HasMaxLength(75);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Expenditure>(entity =>
            {
                entity.ToTable("Expenditure", "Batches");

                entity.Property(e => e.ExpenditureId).HasColumnName("ExpenditureID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ExpenseType).HasMaxLength(25);

                entity.Property(e => e.IsReceiptIssued)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ModeOfPayment).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Expenditure)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_Expenditure_Batch");
            });

            modelBuilder.Entity<ExpenditureReport>(entity =>
            {
                entity.HasKey(e => e.ExpReportId);

                entity.ToTable("ExpenditureReport", "Batches");

                entity.Property(e => e.ExpReportId).HasColumnName("ExpReportID");

                entity.Property(e => e.ActualBatch).HasMaxLength(50);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ExpenseType).HasMaxLength(25);

                entity.Property(e => e.ModeOfPayment).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ExpenseItems>(entity =>
            {
                entity.ToTable("ExpenseItems", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ExpensePeriod>(entity =>
            {
                entity.ToTable("ExpensePeriod", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Period)
                    .HasColumnName("PERIOD")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FreightAgent>(entity =>
            {
                entity.ToTable("FreightAgent", "Batches");

                entity.Property(e => e.FreightAgentId)
                    .HasColumnName("FreightAgentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TownCity).HasMaxLength(25);

                entity.Property(e => e.WebAddress).HasMaxLength(100);
            });

            modelBuilder.Entity<GhanaCities>(entity =>
            {
                entity.ToTable("GhanaCities", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ghcity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GHCity", "Batches");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("Income", "Batches");

                entity.Property(e => e.IncomeId).HasColumnName("IncomeID");

                entity.Property(e => e.ActualRef).HasMaxLength(20);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ModeOfPayment).HasMaxLength(20);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Income)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_Income_Batch");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__Item__727E83EBCB113D7C");

                entity.ToTable("Items", "Batches");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Brand).HasMaxLength(75);

                entity.Property(e => e.Colour).HasMaxLength(75);

                entity.Property(e => e.Condition).HasMaxLength(75);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Make).HasMaxLength(75);

                entity.Property(e => e.Model).HasMaxLength(75);

                entity.Property(e => e.Name).HasMaxLength(75);

                entity.Property(e => e.Specifications).HasMaxLength(75);

                entity.Property(e => e.Type).HasMaxLength(75);

                entity.Property(e => e.UnitCost).HasColumnType("money");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.ToTable("License", "Batches");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.Property(e => e.LicenceEndDate).HasColumnType("date");

                entity.Property(e => e.LicenceStartDate).HasColumnType("date");

                entity.Property(e => e.LicenseNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserEmailAddress)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LinkCreator>(entity =>
            {
                entity.ToTable("LinkCreator", "Batches");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<LinkCreator1>(entity =>
            {
                entity.ToTable("LinkCreator");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExpiryFlag)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LinkStatus)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login", "Batches");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Role)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<PayMode>(entity =>
            {
                entity.ToTable("PayMode", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Paymode1)
                    .HasColumnName("PAYMODE")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Performance>(entity =>
            {
                entity.ToTable("Performance", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountMonth).HasMaxLength(50);

                entity.Property(e => e.AccountYear).HasMaxLength(50);

                entity.Property(e => e.AcountDate).HasColumnType("datetime");

                entity.Property(e => e.AditionalIncome).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.ProfitLoss).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProfitLossPercent).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalExpenditure).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalIncome).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Performance)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_Performance_Batch");
            });

            modelBuilder.Entity<PickingAddressList>(entity =>
            {
                entity.ToTable("PickingAddressList", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.Country).HasMaxLength(75);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(75);

                entity.Property(e => e.LastName).HasMaxLength(75);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostTown).HasMaxLength(75);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(75);
            });

            modelBuilder.Entity<PickingAddressListRep>(entity =>
            {
                entity.ToTable("PickingAddressListRep", "Batches");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.Country).HasMaxLength(75);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(75);

                entity.Property(e => e.LastName).HasMaxLength(75);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostTown).HasMaxLength(75);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(75);
            });

            modelBuilder.Entity<Recipients>(entity =>
            {
                entity.HasKey(e => e.RecipientId)
                    .HasName("PK__Receiver__FEBB5F072C82413E");

                entity.ToTable("Recipients", "Batches");

                entity.HasIndex(e => new { e.FirstName, e.LastName, e.Telephone, e.RecipientId })
                    .HasName("NonClusteredIndex-20140216-190105");

                entity.Property(e => e.RecipientId).HasColumnName("RecipientID");

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.Country).HasMaxLength(75);

                entity.Property(e => e.County)
                    .HasMaxLength(75)
                    .IsFixedLength();

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(75);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(75);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostTown).HasMaxLength(75);

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(75);

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Recipients)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipients_Senders");
            });

            modelBuilder.Entity<ReferenceNumber>(entity =>
            {
                entity.ToTable("ReferenceNumber", "Batches");

                entity.Property(e => e.RefLabel).HasMaxLength(50);

                entity.Property(e => e.StatusFlag).HasMaxLength(50);
            });

            modelBuilder.Entity<ReferenceNumber1>(entity =>
            {
                entity.ToTable("ReferenceNumber");

                entity.Property(e => e.RefLabel).HasMaxLength(50);

                entity.Property(e => e.StatusFlag).HasMaxLength(50);
            });

            modelBuilder.Entity<Senders>(entity =>
            {
                entity.HasKey(e => e.SenderId)
                    .HasName("PK__Sender__BB4991ABD1AAA538");

                entity.ToTable("Senders", "Batches");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.Country).HasMaxLength(75);

                entity.Property(e => e.County)
                    .HasMaxLength(75)
                    .IsFixedLength();

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(75);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(75);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostTown).HasMaxLength(75);

                entity.Property(e => e.ShippersId).HasColumnName("ShippersID");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(75);
            });

            modelBuilder.Entity<SenderRecipient>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SenderRecipient", "rayaduboat");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(75);

                entity.Property(e => e.LastName).HasMaxLength(75);

                entity.Property(e => e.Title).HasMaxLength(75);
            });

            modelBuilder.Entity<SenderRecipients>(entity =>
            {
                entity.HasKey(e => e.RecipientId);

                entity.ToTable("SenderRecipients", "Batches");

                entity.Property(e => e.RecipientId).HasColumnName("RecipientID");

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.Country).HasMaxLength(75);

                entity.Property(e => e.County)
                    .HasMaxLength(75)
                    .IsFixedLength();

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(75);

                entity.Property(e => e.LastName).HasMaxLength(75);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostTown).HasMaxLength(75);

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(75);
            });
        modelBuilder.Entity<Shippers>(entity =>
            {
                entity.ToTable("Shippers", "Batches");

                entity.Property(e => e.ShippersId).HasColumnName("ShippersID");

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CompanyName).HasMaxLength(75);

                entity.Property(e => e.CompanyNumber).HasMaxLength(75);

                entity.Property(e => e.Country).HasMaxLength(75);

                entity.Property(e => e.County).HasMaxLength(75);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(75);

                entity.Property(e => e.FirstName).HasMaxLength(75);

                entity.Property(e => e.LastName).HasMaxLength(75);

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostTown).HasMaxLength(75);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(75);

                entity.Property(e => e.Vatnumber)
                    .IsRequired()
                    .HasColumnName("VATNumber")
                    .HasMaxLength(75);

                entity.Property(e => e.WebAddress).HasMaxLength(100);

                entity.HasOne(d => d.License)
                    .WithMany(p => p.Shippers)
                    .HasForeignKey(d => d.LicenseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shippers_License");
            });

            modelBuilder.Entity<TripAudit>(entity =>
            {
                entity.ToTable("TripAudit", "Batches");

                entity.Property(e => e.BatchId).HasColumnName("batchId");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.StatusDetails).HasMaxLength(20);

                entity.Property(e => e.TimeOfChange).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            });


            //modelBuilder.Entity<TripAudit1>(entity =>
            //{
            //    entity.ToTable("TripAudit");

            //    entity.Property(e => e.BatchId).HasColumnName("batchId");

            //    entity.Property(e => e.NewStatus)
            //        .HasMaxLength(10)
            //        .IsFixedLength();

            //    entity.Property(e => e.ShipperId).HasColumnName("shipperId");

            //    entity.Property(e => e.TimeOfChange).HasColumnType("datetime");

            //    entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            //});

            modelBuilder.Entity<TripDetails>(entity =>
            {
                entity.HasKey(e => e.TripId);

                entity.ToTable("TripDetails", "Batches");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.Property(e => e.ActualRef).HasMaxLength(20);

                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.GrandTotal).HasColumnType("money");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.RecipientId).HasColumnName("RecipientID");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.TripDetails)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripDetails_Batch");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TripDetails)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_TripDetails_Items");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.TripDetails)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripDetails_Recipients");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.TripDetails)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripDetails_Senders");
            });

            modelBuilder.Entity<TripListReport>(entity =>
            {
                entity.HasKey(e => e.TripReportId);

                entity.ToTable("TripListReport", "Batches");

                entity.Property(e => e.TripReportId).HasColumnName("TripReportID");

                entity.Property(e => e.ActualBatch).HasMaxLength(20);

                entity.Property(e => e.ActualRef).HasMaxLength(20);

                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.Property(e => e.RecTown).HasMaxLength(50);

                entity.Property(e => e.RecipientName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SendTown).HasMaxLength(50);

                entity.Property(e => e.SenderName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TelRecipient).HasMaxLength(50);

                entity.Property(e => e.TelSender).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("money");
            });

            modelBuilder.Entity<Ukcity>(entity =>
            {
                entity.ToTable("UKCity", "Batches");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);
            });





            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Users", "Batches");

                entity.Property(e => e.AddressLineOne).HasMaxLength(75);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(75);

                entity.Property(e => e.Country).HasMaxLength(75);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PostTown).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(100);

                entity.Property(e => e.ShippersId).HasColumnName("ShippersID");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(50);
            });
           

            modelBuilder.Entity<VwSenderRecipientData>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_SenderRecipientData", "rayaduboat");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("companyName")
                    .HasMaxLength(75);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(75);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(75);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
