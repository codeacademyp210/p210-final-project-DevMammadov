﻿// <auto-generated />
using System;
using MainProjectFull.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MainProjectFull.Migrations
{
    [DbContext(typeof(CodeContext))]
    [Migration("20190809172724_addTypeToPort")]
    partial class addTypeToPort
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MainProjectFull.Models.Blocklist", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("blockedId");

                    b.Property<int>("blockerId");

                    b.HasKey("id");

                    b.ToTable("Blocklist");
                });

            modelBuilder.Entity("MainProjectFull.Models.Company", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.HasKey("id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("MainProjectFull.Models.Cv", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Photo");

                    b.Property<int>("ProfileId");

                    b.Property<string>("Type");

                    b.HasKey("id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Cv");
                });

            modelBuilder.Entity("MainProjectFull.Models.Experience", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About");

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Position");

                    b.Property<int>("ProfileId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("MainProjectFull.Models.Follow", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("folowerId");

                    b.Property<int>("folowingId");

                    b.HasKey("id");

                    b.ToTable("Follow");
                });

            modelBuilder.Entity("MainProjectFull.Models.Languages", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.HasKey("id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("MainProjectFull.Models.Messages", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("GetterId");

                    b.Property<int>("Message");

                    b.Property<int>("SenderId");

                    b.Property<bool>("Status");

                    b.HasKey("id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MainProjectFull.Models.Notify", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Status");

                    b.Property<string>("Topic");

                    b.Property<string>("Type");

                    b.Property<int>("UsersId");

                    b.HasKey("id");

                    b.HasIndex("UsersId");

                    b.ToTable("Notify");
                });

            modelBuilder.Entity("MainProjectFull.Models.Portfolio", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About");

                    b.Property<string>("Behance");

                    b.Property<string>("Github");

                    b.Property<string>("Name");

                    b.Property<int>("ProfileId");

                    b.Property<int>("Type");

                    b.Property<int>("View");

                    b.Property<string>("Website");

                    b.HasKey("id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Portfolio");
                });

            modelBuilder.Entity("MainProjectFull.Models.PortfolioImages", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("PortfolioId");

                    b.HasKey("id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PortfolioImages");
                });

            modelBuilder.Entity("MainProjectFull.Models.Position", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.Property<bool>("Veryfied");

                    b.HasKey("id");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("MainProjectFull.Models.Profile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BackColor");

                    b.Property<string>("Cover");

                    b.Property<bool>("MessageButton");

                    b.Property<string>("Photo");

                    b.Property<string>("TextColor");

                    b.Property<int>("UsersId");

                    b.Property<int>("View");

                    b.HasKey("id");

                    b.HasIndex("UsersId");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("MainProjectFull.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("MainProjectFull.Models.SkillsHeader", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ProfileId");

                    b.HasKey("id");

                    b.HasIndex("ProfileId");

                    b.ToTable("SkillsHeader");
                });

            modelBuilder.Entity("MainProjectFull.Models.SkillsHeaderPosition", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PositionId");

                    b.Property<int>("SkillsHeaderId");

                    b.HasKey("id");

                    b.HasIndex("PositionId");

                    b.HasIndex("SkillsHeaderId");

                    b.ToTable("SkillsHeaderPosition");
                });

            modelBuilder.Entity("MainProjectFull.Models.SocialAct", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About");

                    b.Property<string>("Company")
                        .IsRequired();

                    b.Property<string>("Position")
                        .IsRequired();

                    b.Property<int>("UsersId");

                    b.HasKey("id");

                    b.HasIndex("UsersId");

                    b.ToTable("SocialAct");
                });

            modelBuilder.Entity("MainProjectFull.Models.University", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<string>("Profession");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("UserId");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("University");
                });

            modelBuilder.Entity("MainProjectFull.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("Behance");

                    b.Property<DateTime>("Birth");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Facebook");

                    b.Property<bool>("Gender");

                    b.Property<string>("Github");

                    b.Property<string>("Hobbi");

                    b.Property<string>("Linkedin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Profession");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MainProjectFull.Models.UsersLanguages", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LanguagesId");

                    b.Property<string>("Level");

                    b.Property<int>("UsersId");

                    b.HasKey("id");

                    b.HasIndex("LanguagesId");

                    b.HasIndex("UsersId");

                    b.ToTable("UsersLanguages");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MainProjectFull.Models.Cv", b =>
                {
                    b.HasOne("MainProjectFull.Models.Profile", "Profile")
                        .WithMany("Cv")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.Experience", b =>
                {
                    b.HasOne("MainProjectFull.Models.Company", "Company")
                        .WithMany("Experience")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MainProjectFull.Models.Profile", "Profile")
                        .WithMany("Experience")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.Notify", b =>
                {
                    b.HasOne("MainProjectFull.Models.Users", "User")
                        .WithMany("Notify")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.Portfolio", b =>
                {
                    b.HasOne("MainProjectFull.Models.Profile", "Profile")
                        .WithMany("Portfolios")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.PortfolioImages", b =>
                {
                    b.HasOne("MainProjectFull.Models.Portfolio", "Portfolio")
                        .WithMany("PortfolioImages")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.Profile", b =>
                {
                    b.HasOne("MainProjectFull.Models.Users", "User")
                        .WithMany("Profile")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.SkillsHeader", b =>
                {
                    b.HasOne("MainProjectFull.Models.Profile", "Profile")
                        .WithMany("SkillsHeader")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.SkillsHeaderPosition", b =>
                {
                    b.HasOne("MainProjectFull.Models.Position", "Position")
                        .WithMany("SkillsHeaderPosition")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MainProjectFull.Models.SkillsHeader", "SkillsHeader")
                        .WithMany("SkillsHeaderPosition")
                        .HasForeignKey("SkillsHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.SocialAct", b =>
                {
                    b.HasOne("MainProjectFull.Models.Users", "User")
                        .WithMany("SocialAct")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.University", b =>
                {
                    b.HasOne("MainProjectFull.Models.Users", "User")
                        .WithMany("University")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MainProjectFull.Models.UsersLanguages", b =>
                {
                    b.HasOne("MainProjectFull.Models.Languages", "Language")
                        .WithMany("UsersLanguages")
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MainProjectFull.Models.Users", "User")
                        .WithMany("UsersLanguages")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("MainProjectFull.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("MainProjectFull.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("MainProjectFull.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("MainProjectFull.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MainProjectFull.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("MainProjectFull.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
