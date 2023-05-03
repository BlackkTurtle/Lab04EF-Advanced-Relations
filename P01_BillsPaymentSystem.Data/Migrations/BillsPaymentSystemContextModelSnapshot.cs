﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P01_BillsPaymentSystem.Data;

#nullable disable

namespace P01_BillsPaymentSystem.Data.Migrations
{
    [DbContext(typeof(BillsPaymentSystemContext))]
    partial class BillsPaymentSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("SWIFTCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("BankAccountId")
                        .HasName("PK__BankAccounts");

                    b.ToTable("bankAccounts");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.CreditCard", b =>
                {
                    b.Property<int>("CreditCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Limit")
                        .HasColumnType("REAL");

                    b.Property<double>("MoneyOwed")
                        .HasColumnType("REAL");

                    b.HasKey("CreditCardId")
                        .HasName("PK__CreditCards");

                    b.ToTable("creditCards");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BankAccountId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Unique", true);

                    b.Property<int?>("CreditCardId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Unique", true);

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id")
                        .HasName("PK__PaymentMethods");

                    b.HasIndex("BankAccountId")
                        .IsUnique();

                    b.HasIndex("CreditCardId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("paymentMethods", t =>
                        {
                            t.HasCheckConstraint("CK_PaymentMethod_BankAccountId", "(BankAccountId IS NOT NULL AND CreditCardId IS NULL) OR (BankAccountId IS NULL AND CreditCardId IS NOT NULL)");
                        });
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId")
                        .HasName("PK__Users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.PaymentMethod", b =>
                {
                    b.HasOne("P01_BillsPaymentSystem.Data.Models.BankAccount", "BankAccount")
                        .WithOne("PaymentMethod")
                        .HasForeignKey("P01_BillsPaymentSystem.Data.Models.PaymentMethod", "BankAccountId")
                        .IsRequired()
                        .HasConstraintName("FK_PaymentMethod_BankAccounts");

                    b.HasOne("P01_BillsPaymentSystem.Data.Models.CreditCard", "CreditCard")
                        .WithOne("PaymentMethod")
                        .HasForeignKey("P01_BillsPaymentSystem.Data.Models.PaymentMethod", "CreditCardId")
                        .IsRequired()
                        .HasConstraintName("FK_PaymentMethod_CreditCards");

                    b.HasOne("P01_BillsPaymentSystem.Data.Models.User", "User")
                        .WithMany("PaymentMethods")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_PaymentMethod_Users");

                    b.Navigation("BankAccount");

                    b.Navigation("CreditCard");

                    b.Navigation("User");
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.BankAccount", b =>
                {
                    b.Navigation("PaymentMethod")
                        .IsRequired();
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.CreditCard", b =>
                {
                    b.Navigation("PaymentMethod")
                        .IsRequired();
                });

            modelBuilder.Entity("P01_BillsPaymentSystem.Data.Models.User", b =>
                {
                    b.Navigation("PaymentMethods");
                });
#pragma warning restore 612, 618
        }
    }
}
