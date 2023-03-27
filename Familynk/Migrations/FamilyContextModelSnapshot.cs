﻿// <auto-generated />
using System;
using Familynk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Familynk.Migrations
{
    [DbContext(typeof(FamilyContext))]
    partial class FamilyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Familynk.Models.FamilyCalendar", b =>
                {
                    b.Property<int>("FamilyCalendarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FamilyId")
                        .HasColumnType("int");

                    b.Property<string>("FamilyName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FamilyCalendarId");

                    b.ToTable("FamilyCalendars");
                });

            modelBuilder.Entity("Familynk.Models.FamilyEvent", b =>
                {
                    b.Property<int>("FamilyEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CalendarId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("FamilyEventId");

                    b.HasIndex("CalendarId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Familynk.Models.FamilyMember", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("FamilyUnitId")
                        .HasColumnType("int");

                    b.Property<int>("GetFamilyCalendarFamilyCalendarId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MessageBubbleColor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FamilyUnitId");

                    b.HasIndex("GetFamilyCalendarFamilyCalendarId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Familynk.Models.FamilyUnit", b =>
                {
                    b.Property<int>("FamilyUnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("GetCalendarFamilyCalendarId")
                        .HasColumnType("int");

                    b.Property<int>("RulesHouseRulesId")
                        .HasColumnType("int");

                    b.HasKey("FamilyUnitId");

                    b.HasIndex("GetCalendarFamilyCalendarId");

                    b.HasIndex("RulesHouseRulesId");

                    b.ToTable("Neighborhood");
                });

            modelBuilder.Entity("Familynk.Models.HouseRules", b =>
                {
                    b.Property<int>("HouseRulesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("FamilyMembersCreateEvents")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("FamilyMembersCustomizeKitchen")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("FamilyMembersInviteOtherMembers")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("MagneticMessageLifespan")
                        .HasColumnType("time(6)");

                    b.Property<TimeSpan>("StickyNoteLifespan")
                        .HasColumnType("time(6)");

                    b.HasKey("HouseRulesId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("Familynk.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ScrapBookId")
                        .HasColumnType("int");

                    b.Property<int?>("ScrapId")
                        .HasColumnType("int");

                    b.Property<int?>("width")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("ScrapBookId");

                    b.HasIndex("ScrapId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Familynk.Models.Messages.AppMessage", b =>
                {
                    b.Property<int>("AppMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SenderId")
                        .HasColumnType("longtext");

                    b.HasKey("AppMessageId");

                    b.ToTable("AppMessage");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AppMessage");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Familynk.Models.Scrap", b =>
                {
                    b.Property<int>("ScrapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MemberTagId")
                        .HasColumnType("longtext");

                    b.Property<int>("ScrapBookId")
                        .HasColumnType("int");

                    b.Property<string>("SenderId")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ScrapId");

                    b.HasIndex("ScrapBookId");

                    b.ToTable("Scraps");
                });

            modelBuilder.Entity("Familynk.Models.ScrapBook", b =>
                {
                    b.Property<int>("ScrapBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FamilyUnitId")
                        .HasColumnType("int");

                    b.Property<string>("MemberTagId")
                        .HasColumnType("longtext");

                    b.Property<string>("SenderId")
                        .HasColumnType("longtext");

                    b.HasKey("ScrapBookId");

                    b.HasIndex("FamilyUnitId")
                        .IsUnique();

                    b.ToTable("ScrapBooks");
                });

            modelBuilder.Entity("Familynk.Models.UserSettings", b =>
                {
                    b.Property<int>("UserSettingsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SpeechBubbleColor")
                        .HasColumnType("int");

                    b.HasKey("UserSettingsID");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Familynk.Models.Messages.Comment", b =>
                {
                    b.HasBaseType("Familynk.Models.Messages.AppMessage");

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<int?>("FamilyEventId")
                        .HasColumnType("int");

                    b.Property<bool>("HasReply")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsReply")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ReplyTo")
                        .HasColumnType("int");

                    b.Property<int?>("ScrapId")
                        .HasColumnType("int");

                    b.HasIndex("FamilyEventId");

                    b.HasIndex("ReplyTo");

                    b.HasIndex("ScrapId");

                    b.HasDiscriminator().HasValue("Comment");
                });

            modelBuilder.Entity("Familynk.Models.Messages.DirectMessage", b =>
                {
                    b.HasBaseType("Familynk.Models.Messages.AppMessage");

                    b.Property<int>("DirectMessageId")
                        .HasColumnType("int");

                    b.Property<string>("FamilyMemberId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FamilyMemberId1")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RecipientId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasIndex("FamilyMemberId");

                    b.HasIndex("FamilyMemberId1");

                    b.HasDiscriminator().HasValue("DirectMessage");
                });

            modelBuilder.Entity("Familynk.Models.Messages.FamilyMessage", b =>
                {
                    b.HasBaseType("Familynk.Models.Messages.AppMessage");

                    b.Property<string>("FamilyMemberId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("FamilyMessageId")
                        .HasColumnType("int");

                    b.Property<int>("FamilyUnitId")
                        .HasColumnType("int");

                    b.HasIndex("FamilyMemberId");

                    b.HasIndex("FamilyUnitId");

                    b.ToTable("AppMessage", t =>
                        {
                            t.Property("FamilyMemberId")
                                .HasColumnName("FamilyMessage_FamilyMemberId");
                        });

                    b.HasDiscriminator().HasValue("FamilyMessage");
                });

            modelBuilder.Entity("Familynk.Models.Messages.MagneticMessage", b =>
                {
                    b.HasBaseType("Familynk.Models.Messages.AppMessage");

                    b.Property<int>("MagneticMessageId")
                        .HasColumnType("int");

                    b.Property<int?>("PictureImageId")
                        .HasColumnType("int");

                    b.HasIndex("PictureImageId");

                    b.HasDiscriminator().HasValue("MagneticMessage");
                });

            modelBuilder.Entity("Familynk.Models.Messages.Notification", b =>
                {
                    b.HasBaseType("Familynk.Models.Messages.AppMessage");

                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.Property<string>("RecipientId")
                        .HasColumnType("varchar(255)");

                    b.HasIndex("RecipientId");

                    b.ToTable("AppMessage", t =>
                        {
                            t.Property("RecipientId")
                                .HasColumnName("Notification_RecipientId");
                        });

                    b.HasDiscriminator().HasValue("Notification");
                });

            modelBuilder.Entity("Familynk.Models.FamilyEvent", b =>
                {
                    b.HasOne("Familynk.Models.FamilyCalendar", "GetFamilyCalendar")
                        .WithMany("Events")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetFamilyCalendar");
                });

            modelBuilder.Entity("Familynk.Models.FamilyMember", b =>
                {
                    b.HasOne("Familynk.Models.FamilyUnit", null)
                        .WithMany("Members")
                        .HasForeignKey("FamilyUnitId");

                    b.HasOne("Familynk.Models.FamilyCalendar", "GetFamilyCalendar")
                        .WithMany()
                        .HasForeignKey("GetFamilyCalendarFamilyCalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetFamilyCalendar");
                });

            modelBuilder.Entity("Familynk.Models.FamilyUnit", b =>
                {
                    b.HasOne("Familynk.Models.FamilyCalendar", "GetCalendar")
                        .WithMany()
                        .HasForeignKey("GetCalendarFamilyCalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Familynk.Models.HouseRules", "Rules")
                        .WithMany()
                        .HasForeignKey("RulesHouseRulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetCalendar");

                    b.Navigation("Rules");
                });

            modelBuilder.Entity("Familynk.Models.Image", b =>
                {
                    b.HasOne("Familynk.Models.ScrapBook", null)
                        .WithMany("Photos")
                        .HasForeignKey("ScrapBookId");

                    b.HasOne("Familynk.Models.Scrap", null)
                        .WithMany("Images")
                        .HasForeignKey("ScrapId");
                });

            modelBuilder.Entity("Familynk.Models.Scrap", b =>
                {
                    b.HasOne("Familynk.Models.ScrapBook", null)
                        .WithMany("Entries")
                        .HasForeignKey("ScrapBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Familynk.Models.ScrapBook", b =>
                {
                    b.HasOne("Familynk.Models.FamilyUnit", null)
                        .WithOne("FamilyScraps")
                        .HasForeignKey("Familynk.Models.ScrapBook", "FamilyUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Familynk.Models.FamilyMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Familynk.Models.FamilyMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Familynk.Models.FamilyMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Familynk.Models.FamilyMember", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Familynk.Models.Messages.Comment", b =>
                {
                    b.HasOne("Familynk.Models.FamilyEvent", null)
                        .WithMany("Comments")
                        .HasForeignKey("FamilyEventId");

                    b.HasOne("Familynk.Models.Messages.AppMessage", "OriginalPost")
                        .WithMany()
                        .HasForeignKey("ReplyTo");

                    b.HasOne("Familynk.Models.Scrap", null)
                        .WithMany("Comments")
                        .HasForeignKey("ScrapId");

                    b.Navigation("OriginalPost");
                });

            modelBuilder.Entity("Familynk.Models.Messages.DirectMessage", b =>
                {
                    b.HasOne("Familynk.Models.FamilyMember", null)
                        .WithMany("DMsRecieved")
                        .HasForeignKey("FamilyMemberId");

                    b.HasOne("Familynk.Models.FamilyMember", null)
                        .WithMany("DMsSent")
                        .HasForeignKey("FamilyMemberId1");
                });

            modelBuilder.Entity("Familynk.Models.Messages.FamilyMessage", b =>
                {
                    b.HasOne("Familynk.Models.FamilyMember", "Sender")
                        .WithMany()
                        .HasForeignKey("FamilyMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Familynk.Models.FamilyUnit", "Family")
                        .WithMany("FamilyChat")
                        .HasForeignKey("FamilyUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Family");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Familynk.Models.Messages.MagneticMessage", b =>
                {
                    b.HasOne("Familynk.Models.Image", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureImageId");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("Familynk.Models.Messages.Notification", b =>
                {
                    b.HasOne("Familynk.Models.FamilyMember", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("Familynk.Models.FamilyCalendar", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Familynk.Models.FamilyEvent", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Familynk.Models.FamilyMember", b =>
                {
                    b.Navigation("DMsRecieved");

                    b.Navigation("DMsSent");
                });

            modelBuilder.Entity("Familynk.Models.FamilyUnit", b =>
                {
                    b.Navigation("FamilyChat");

                    b.Navigation("FamilyScraps")
                        .IsRequired();

                    b.Navigation("Members");
                });

            modelBuilder.Entity("Familynk.Models.Scrap", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("Familynk.Models.ScrapBook", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
