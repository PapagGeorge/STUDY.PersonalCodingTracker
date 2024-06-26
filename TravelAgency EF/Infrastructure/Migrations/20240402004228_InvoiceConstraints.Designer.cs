﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TravelAgencyDbContext))]
    [Migration("20240402004228_InvoiceConstraints")]
    partial class InvoiceConstraints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Accommodation", b =>
                {
                    b.Property<long>("AccommodationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AccommodationId"));

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<long>("DestinationId")
                        .HasColumnType("bigint");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("PricePerPersonPerDay")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("StarRating")
                        .HasColumnType("int");

                    b.HasKey("AccommodationId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Accommodation");
                });

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CustomerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Entities.Destination", b =>
                {
                    b.Property<long>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DestinationId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DestinationId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("Domain.Entities.Invoice", b =>
                {
                    b.Property<long>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("InvoiceId"));

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("IssuedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 4, 2, 3, 42, 27, 554, DateTimeKind.Local).AddTicks(1108));

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Domain.Entities.Package", b =>
                {
                    b.Property<long>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PackageId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("PackageId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Domain.Entities.PackageAccommodation", b =>
                {
                    b.Property<long>("PackageId")
                        .HasColumnType("bigint");

                    b.Property<long>("AccommodationId")
                        .HasColumnType("bigint");

                    b.HasKey("PackageId", "AccommodationId");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Package_Accomodation");
                });

            modelBuilder.Entity("Domain.Entities.PackageDestination", b =>
                {
                    b.Property<long>("PackageId")
                        .HasColumnType("bigint");

                    b.Property<long>("DestinationId")
                        .HasColumnType("bigint");

                    b.HasKey("PackageId", "DestinationId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Package_Destination");
                });

            modelBuilder.Entity("Domain.Entities.PackageService", b =>
                {
                    b.Property<long>("PackageId")
                        .HasColumnType("bigint");

                    b.Property<long>("ServiceId")
                        .HasColumnType("bigint");

                    b.HasKey("PackageId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Package_Service");
                });

            modelBuilder.Entity("Domain.Entities.PackageTransportation", b =>
                {
                    b.Property<long>("PackageId")
                        .HasColumnType("bigint");

                    b.Property<long>("TransportationId")
                        .HasColumnType("bigint");

                    b.HasKey("PackageId", "TransportationId");

                    b.HasIndex("TransportationId");

                    b.ToTable("Package_Transportation");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Property<long>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long>("InvoiceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PaymentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Domain.Entities.Service", b =>
                {
                    b.Property<long>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ServiceId"));

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<long>("DestinationId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.HasKey("ServiceId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TransactionId"));

                    b.Property<long?>("AccommodationId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PackageId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ServiceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("TransactionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 4, 2, 3, 42, 27, 553, DateTimeKind.Local).AddTicks(9571));

                    b.Property<long?>("TransportationId")
                        .HasColumnType("bigint");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PackageId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TransportationId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Domain.Entities.Transportation", b =>
                {
                    b.Property<long>("TransportationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TransportationId"));

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<long>("DestinationId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("TransportationMode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransportationId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Transportation");
                });

            modelBuilder.Entity("Domain.Entities.Accommodation", b =>
                {
                    b.HasOne("Domain.Entities.Destination", "Destination")
                        .WithMany("Accommodations")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");
                });

            modelBuilder.Entity("Domain.Entities.Invoice", b =>
                {
                    b.HasOne("Domain.Entities.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.Entities.PackageAccommodation", b =>
                {
                    b.HasOne("Domain.Entities.Accommodation", "Accommodation")
                        .WithMany("PackageAccommodation")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Package", "Package")
                        .WithMany("PackageAccommodation")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("Domain.Entities.PackageDestination", b =>
                {
                    b.HasOne("Domain.Entities.Destination", "Destination")
                        .WithMany("PackageDestination")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Package", "Package")
                        .WithMany("PackageDestination")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("Domain.Entities.PackageService", b =>
                {
                    b.HasOne("Domain.Entities.Package", "Package")
                        .WithMany("PackageServices")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Service", "Service")
                        .WithMany("PackageServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Domain.Entities.PackageTransportation", b =>
                {
                    b.HasOne("Domain.Entities.Package", "Package")
                        .WithMany("PackageTransportation")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Transportation", "Transportation")
                        .WithMany("PackageTransportation")
                        .HasForeignKey("TransportationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("Transportation");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.HasOne("Domain.Entities.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Invoice", "Invoice")
                        .WithMany("Payments")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("Domain.Entities.Service", b =>
                {
                    b.HasOne("Domain.Entities.Destination", "Destination")
                        .WithMany("Services")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Domain.Entities.Accommodation", "Accommodation")
                        .WithMany("Transactions")
                        .HasForeignKey("AccommodationId");

                    b.HasOne("Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Package", "Package")
                        .WithMany("Transactions")
                        .HasForeignKey("PackageId");

                    b.HasOne("Domain.Entities.Service", "Service")
                        .WithMany("Transactions")
                        .HasForeignKey("ServiceId");

                    b.HasOne("Domain.Entities.Transportation", "Transportation")
                        .WithMany("Transactions")
                        .HasForeignKey("TransportationId");

                    b.Navigation("Accommodation");

                    b.Navigation("Customer");

                    b.Navigation("Package");

                    b.Navigation("Service");

                    b.Navigation("Transportation");
                });

            modelBuilder.Entity("Domain.Entities.Transportation", b =>
                {
                    b.HasOne("Domain.Entities.Destination", "Destination")
                        .WithMany("Transportations")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");
                });

            modelBuilder.Entity("Domain.Entities.Accommodation", b =>
                {
                    b.Navigation("PackageAccommodation");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Destination", b =>
                {
                    b.Navigation("Accommodations");

                    b.Navigation("PackageDestination");

                    b.Navigation("Services");

                    b.Navigation("Transportations");
                });

            modelBuilder.Entity("Domain.Entities.Invoice", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Package", b =>
                {
                    b.Navigation("PackageAccommodation");

                    b.Navigation("PackageDestination");

                    b.Navigation("PackageServices");

                    b.Navigation("PackageTransportation");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.Service", b =>
                {
                    b.Navigation("PackageServices");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.Transportation", b =>
                {
                    b.Navigation("PackageTransportation");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
