﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelMore.Persistance.Contexts.TravelMore;

#nullable disable

namespace TravelMore.Persistance.Migrations
{
    [DbContext(typeof(TravelMoreContext))]
    partial class TravelMoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelMore.Domain.Bookings.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookedHotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<Guid>("PaymentDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Details", "TravelMore.Domain.Bookings.Booking.Details#BookingDetails", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<short>("NumberOfDays")
                                .HasColumnType("smallint");

                            b1.Property<short>("NumberOfGuests")
                                .HasColumnType("smallint");

                            b1.ComplexProperty<Dictionary<string, object>>("Schedule", "TravelMore.Domain.Bookings.Booking.Details#BookingDetails.Schedule#BookingSchedule", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<DateTime>("From")
                                        .HasColumnType("datetime2");

                                    b2.Property<DateTime>("To")
                                        .HasColumnType("datetime2");
                                });
                        });

                    b.HasKey("Id");

                    b.HasIndex("BookedHotelId");

                    b.HasIndex("GuestId");

                    b.HasIndex("PaymentDetailsId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TravelMore.Domain.Hotels.Hotel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("HostId")
                        .HasColumnType("int");

                    b.Property<short>("MaxNumberOfGuests")
                        .HasColumnType("smallint");

                    b.ComplexProperty<Dictionary<string, object>>("PricePerDay", "TravelMore.Domain.Hotels.Hotel.PricePerDay#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Coupons.MembershipCoupon", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.ToTable("MembershipCoupon");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Discounts.MembershipDiscount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.ToTable("MembershipDiscount");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("PricePerMonth", "TravelMore.Domain.Memberships.Membership.PricePerMonth#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PricePerYear", "TravelMore.Domain.Memberships.Membership.PricePerYear#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("GuestId")
                        .IsUnique();

                    b.ToTable("Memberships");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Membership");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TravelMore.Domain.PaymentsDetails.BookingPaymentDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("HostId")
                        .HasColumnType("int");

                    b.Property<int>("PayerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Fee", "TravelMore.Domain.PaymentsDetails.BookingPaymentDetails.Fee#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Payment", "TravelMore.Domain.PaymentsDetails.BookingPaymentDetails.Payment#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("TotalPayment", "TravelMore.Domain.PaymentsDetails.BookingPaymentDetails.TotalPayment#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.HasIndex("PayerId");

                    b.ToTable("PaymentsDetails");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.PremiumMembership", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Memberships.Membership");

                    b.HasDiscriminator().HasValue("PremiumMembership");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.StandardMembership", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Memberships.Membership");

                    b.HasDiscriminator().HasValue("StandardMembership");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.Guests.Guest", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Users.User");

                    b.Property<Guid>("MembershipId")
                        .HasColumnType("uniqueidentifier");

                    b.ComplexProperty<Dictionary<string, object>>("Balance", "TravelMore.Domain.Users.Guests.Guest.Balance#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Guest");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.Hosts.Host", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Users.User");

                    b.HasDiscriminator().HasValue("Host");
                });

            modelBuilder.Entity("TravelMore.Domain.Bookings.Booking", b =>
                {
                    b.HasOne("TravelMore.Domain.Hotels.Hotel", "BookedHotel")
                        .WithMany("Bookings")
                        .HasForeignKey("BookedHotelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TravelMore.Domain.Users.Guests.Guest", "Guest")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TravelMore.Domain.PaymentsDetails.BookingPaymentDetails", "PaymentDetails")
                        .WithMany()
                        .HasForeignKey("PaymentDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookedHotel");

                    b.Navigation("Guest");

                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("TravelMore.Domain.Hotels.Hotel", b =>
                {
                    b.HasOne("TravelMore.Domain.Users.Hosts.Host", "Host")
                        .WithMany("Hotels")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Coupons.MembershipCoupon", b =>
                {
                    b.HasOne("TravelMore.Domain.Memberships.Membership", "Target")
                        .WithMany("Coupons")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TravelMore.Domain.Common.Models.Money", "DiscountAmount", b1 =>
                        {
                            b1.Property<Guid>("MembershipCouponId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");

                            b1.HasKey("MembershipCouponId");

                            b1.ToTable("MembershipCoupon");

                            b1.WithOwner()
                                .HasForeignKey("MembershipCouponId");
                        });

                    b.OwnsOne("TravelMore.Domain.Coupons.ValueObjects.CouponCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("MembershipCouponId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MembershipCouponId");

                            b1.ToTable("MembershipCoupon");

                            b1.WithOwner()
                                .HasForeignKey("MembershipCouponId");
                        });

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("DiscountAmount")
                        .IsRequired();

                    b.Navigation("Target");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Discounts.MembershipDiscount", b =>
                {
                    b.HasOne("TravelMore.Domain.Memberships.Membership", "Target")
                        .WithMany("Discounts")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Target");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Membership", b =>
                {
                    b.HasOne("TravelMore.Domain.Users.Guests.Guest", "Guest")
                        .WithOne("Membership")
                        .HasForeignKey("TravelMore.Domain.Memberships.Membership", "GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("TravelMore.Domain.PaymentsDetails.BookingPaymentDetails", b =>
                {
                    b.HasOne("TravelMore.Domain.Users.Hosts.Host", "Host")
                        .WithMany("ReceivedPayments")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TravelMore.Domain.Users.Guests.Guest", "Payer")
                        .WithMany("Payments")
                        .HasForeignKey("PayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Host");

                    b.Navigation("Payer");
                });

            modelBuilder.Entity("TravelMore.Domain.Hotels.Hotel", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Membership", b =>
                {
                    b.Navigation("Coupons");

                    b.Navigation("Discounts");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.Guests.Guest", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Membership")
                        .IsRequired();

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.Hosts.Host", b =>
                {
                    b.Navigation("Hotels");

                    b.Navigation("ReceivedPayments");
                });
#pragma warning restore 612, 618
        }
    }
}
