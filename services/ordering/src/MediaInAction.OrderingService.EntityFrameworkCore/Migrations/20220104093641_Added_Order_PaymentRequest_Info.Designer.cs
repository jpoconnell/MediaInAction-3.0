﻿// <auto-generated />
using System;
using MediaInAction.OrderingService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace MediaInAction.OrderingService.Migrations
{
    [DbContext(typeof(OrderingServiceDbContext))]
    [Migration("20220104093641_Added_Order_PaymentRequest_Info")]
    partial class Added_Order_PaymentRequest_Info
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.PostgreSql)
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediaInAction.OrderingService.Buyers.Buyer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("_paymentTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("PaymentTypeId");

                    b.HasKey("Id");

                    b.HasIndex("_paymentTypeId");

                    b.ToTable("Buyers", (string)null);
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Buyers.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes", (string)null);
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PaymentRequestId")
                        .HasColumnType("uuid");

                    b.Property<string>("PaymentStatus")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("_orderStatusId")
                        .HasColumnType("integer")
                        .HasColumnName("OrderStatusId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("Id");

                    b.HasIndex("_orderStatusId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("text");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("Units")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Orders.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus", (string)null);
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Buyers.Buyer", b =>
                {
                    b.HasOne("MediaInAction.OrderingService.Buyers.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("_paymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentType");
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Orders.Order", b =>
                {
                    b.HasOne("MediaInAction.OrderingService.Buyers.Buyer", null)
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("MediaInAction.OrderingService.Orders.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("_orderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MediaInAction.OrderingService.Orders.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .HasColumnType("text");

                            b1.Property<string>("Description")
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("text");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address");

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Orders.OrderItem", b =>
                {
                    b.HasOne("MediaInAction.OrderingService.Orders.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaInAction.OrderingService.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
