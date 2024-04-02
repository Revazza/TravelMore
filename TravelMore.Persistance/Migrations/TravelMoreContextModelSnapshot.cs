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

            modelBuilder.Entity("TravelMore.Domain.PaymentsDetails.PaymentDetails", b =>
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

                    b.ComplexProperty<Dictionary<string, object>>("Fee", "TravelMore.Domain.PaymentsDetails.PaymentDetails.Fee#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Payment", "TravelMore.Domain.PaymentsDetails.PaymentDetails.Payment#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("TotalPayment", "TravelMore.Domain.PaymentsDetails.PaymentDetails.TotalPayment#Money", b1 =>
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
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

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

            modelBuilder.Entity("TravelMore.Domain.Users.Guests.Guest", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Users.User");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Balance", "TravelMore.Domain.Users.Guests.Guest.Balance#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 10)
                                .HasColumnType("decimal(18,10)");
                        });

                    b.HasDiscriminator().HasValue("Guest");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.Hosts.Host", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Users.User");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Host");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.PremiumGuests.PremiumGuest", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Users.Guests.Guest");

                    b.HasDiscriminator().HasValue("PremiumGuest");
                });

            modelBuilder.Entity("TravelMore.Domain.Users.StandartGuests.StandardGuest", b =>
                {
                    b.HasBaseType("TravelMore.Domain.Users.Guests.Guest");

                    b.HasDiscriminator().HasValue("StandardGuest");
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

                    b.HasOne("TravelMore.Domain.PaymentsDetails.PaymentDetails", "PaymentDetails")
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

            modelBuilder.Entity("TravelMore.Domain.PaymentsDetails.PaymentDetails", b =>
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

            modelBuilder.Entity("TravelMore.Domain.Users.Guests.Guest", b =>
                {
                    b.Navigation("Bookings");

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
