﻿// <auto-generated />
using System;
using LoginFormForGameInUnity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoginFormForGameInUnity.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221216021410_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LoginFormForGameInUnity.Models.ResetPasswordRequest", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Token");

                    b.ToTable("PasswordsReset");
                });

            modelBuilder.Entity("LoginFormForGameInUnity.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswodSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LoginFormForGameInUnity.Models.UserLoginRequest", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Username");

                    b.ToTable("UsersLogin");
                });

            modelBuilder.Entity("LoginFormForGameInUnity.Models.UserRegisterRequest", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Username");

                    b.ToTable("UsersRegister");
                });

            modelBuilder.Entity("LoginFormForGameInUnity.Models.UserScore", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("RecordPlayDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("UserScores");
                });
#pragma warning restore 612, 618
        }
    }
}
