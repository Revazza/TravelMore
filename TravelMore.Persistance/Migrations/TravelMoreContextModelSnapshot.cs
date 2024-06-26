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

            modelBuilder.Entity("DiscountGuest", b =>
                {
                    b.Property<Guid>("DiscountsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.HasKey("DiscountsId", "GuestId");

                    b.HasIndex("GuestId");

                    b.ToTable("DiscountGuest");
                });

            modelBuilder.Entity("TravelMore.Domain.Bookings.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<Guid>("HotelId")
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

                    b.HasIndex("GuestId");

                    b.HasIndex("HotelId");

                    b.ToTable("Bookings");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Booking");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TravelMore.Domain.Discounts.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<Guid?>("MembershipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Subject")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Value", "TravelMore.Domain.Discounts.Discount.Value#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("MembershipId");

                    b.ToTable("Discounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Discount");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TravelMore.Domain.Hotels.Hotel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("DiscountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("HostId")
                        .HasColumnType("int");

                    b.Property<short>("MaxNumberOfGuests")
                        .HasColumnType("smallint");

                    b.ComplexProperty<Dictionary<string, object>>("PricePerNight", "TravelMore.Domain.Hotels.Hotel.PricePerNight#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

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

                    b.ComplexProperty<Dictionary<string, object>>("Code", "TravelMore.Domain.Memberships.Coupons.MembershipCoupon.Code#CouponCode", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("DiscountAmount", "TravelMore.Domain.Memberships.Coupons.MembershipCoupon.DiscountAmount#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.ToTable("MembershipCoupon");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

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
                });

            modelBuilder.Entity("TravelMore.Domain.PaymentsDetails.BookingPaymentDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HostId")
                        .HasColumnType("int");

                    b.Property<int>("PayerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("PriceDetails", "TravelMore.Domain.PaymentsDetails.BookingPaymentDetails.PriceDetails#PriceDetails", b1 =>
                        {
                            b1.IsRequired();

                            b1.ComplexProperty<Dictionary<string, object>>("DiscountedAmount", "TravelMore.Domain.PaymentsDetails.BookingPaymentDetails.PriceDetails#PriceDetails.DiscountedAmount#Money", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(18, 10)
                                        .HasColumnType("decimal(18,10)");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("DiscountedPrice", "TravelMore.Domain.PaymentsDetails.BookingPaymentDetails.PriceDetails#PriceDetails.DiscountedPrice#Money", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(18, 10)
                                        .HasColumnType("decimal(18,10)");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("InitialPrice", "TravelMore.Domain.PaymentsDetails.BookingPaymentDetails.PriceDetails#PriceDetails.InitialPrice#Money", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(18, 10)
                                        .HasColumnType("decimal(18,10)");
                                });
                        });

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

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

            modelBuilder.Entity("TravelMore.Domain.Bookings.ConfirmedBooking", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Bookings.Booking");

                    b.HasDiscriminator().HasValue("ConfirmedBooking");
                });

            modelBuilder.Entity("TravelMore.Domain.Bookings.DraftBooking", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Bookings.Booking");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("PriceDetails", "TravelMore.Domain.Bookings.DraftBooking.PriceDetails#PriceDetails", b1 =>
                        {
                            b1.IsRequired();

                            b1.ComplexProperty<Dictionary<string, object>>("DiscountedAmount", "TravelMore.Domain.Bookings.DraftBooking.PriceDetails#PriceDetails.DiscountedAmount#Money", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(18, 10)
                                        .HasColumnType("decimal(18,10)");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("DiscountedPrice", "TravelMore.Domain.Bookings.DraftBooking.PriceDetails#PriceDetails.DiscountedPrice#Money", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(18, 10)
                                        .HasColumnType("decimal(18,10)");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("InitialPrice", "TravelMore.Domain.Bookings.DraftBooking.PriceDetails#PriceDetails.InitialPrice#Money", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(18, 10)
                                        .HasColumnType("decimal(18,10)");
                                });
                        });

                    b.HasDiscriminator().HasValue("DraftBooking");
                });

            modelBuilder.Entity("TravelMore.Domain.Discounts.LimitedUseDiscount", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Discounts.Discount");

                    b.Property<int>("RemainingUses")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("LimitedUseDiscount");
                });

            modelBuilder.Entity("TravelMore.Domain.Discounts.TimeLimitedDiscount", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Discounts.Discount");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("TimeLimitedDiscount");
                });

            modelBuilder.Entity("TravelMore.Domain.Guests.Guest", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Users.User");

                    b.Property<Guid>("MembershipId")
                        .HasColumnType("uniqueidentifier");

                    b.ComplexProperty<Dictionary<string, object>>("Balance", "TravelMore.Domain.Guests.Guest.Balance#Money", b1 =>
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

            modelBuilder.Entity("DiscountGuest", b =>
                {
                    b.HasOne("TravelMore.Domain.Discounts.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelMore.Domain.Guests.Guest", null)
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelMore.Domain.Bookings.Booking", b =>
                {
                    b.HasOne("TravelMore.Domain.Guests.Guest", "Guest")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TravelMore.Domain.Hotels.Hotel", "Hotel")
                        .WithMany("Bookings")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TravelMore.Domain.Discounts.Discount", b =>
                {
                    b.HasOne("TravelMore.Domain.Bookings.Booking", null)
                        .WithMany("AppliedDiscounts")
                        .HasForeignKey("BookingId");

                    b.HasOne("TravelMore.Domain.Memberships.Membership", null)
                        .WithMany("Discounts")
                        .HasForeignKey("MembershipId");
                });

            modelBuilder.Entity("TravelMore.Domain.Hotels.Hotel", b =>
                {
                    b.HasOne("TravelMore.Domain.Discounts.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelMore.Domain.Users.Hosts.Host", "Host")
                        .WithMany("Hotels")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("Host");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Coupons.MembershipCoupon", b =>
                {
                    b.HasOne("TravelMore.Domain.Memberships.Membership", "Target")
                        .WithMany("Coupons")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Target");
                });

            modelBuilder.Entity("TravelMore.Domain.Memberships.Membership", b =>
                {
                    b.HasOne("TravelMore.Domain.Guests.Guest", "Guest")
                        .WithOne("Membership")
                        .HasForeignKey("TravelMore.Domain.Memberships.Membership", "GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("TravelMore.Domain.PaymentsDetails.BookingPaymentDetails", b =>
                {
                    b.HasOne("TravelMore.Domain.Bookings.ConfirmedBooking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelMore.Domain.Users.Hosts.Host", null)
                        .WithMany("ReceivedPayments")
                        .HasForeignKey("HostId");

                    b.HasOne("TravelMore.Domain.Guests.Guest", "Payer")
                        .WithMany("BookingPayments")
                        .HasForeignKey("PayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Payer");
                });

            modelBuilder.Entity("TravelMore.Domain.Bookings.ConfirmedBooking", b =>
                {
                    b.HasOne("TravelMore.Domain.PaymentsDetails.BookingPaymentDetails", "Payment")
                        .WithOne()
                        .HasForeignKey("TravelMore.Domain.Bookings.ConfirmedBooking", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("TravelMore.Domain.Bookings.Booking", b =>
                {
                    b.Navigation("AppliedDiscounts");
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

            modelBuilder.Entity("TravelMore.Domain.Guests.Guest", b =>
                {
                    b.Navigation("BookingPayments");

                    b.Navigation("Bookings");

                    b.Navigation("Membership")
                        .IsRequired();
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
