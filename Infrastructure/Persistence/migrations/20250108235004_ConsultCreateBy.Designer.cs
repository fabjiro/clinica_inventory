﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250108235004_ConsultCreateBy")]
    partial class ConsultCreateBy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConsultEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AntecedentFamily")
                        .HasColumnType("text");

                    b.Property<string>("AntecedentPersonal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BilogicalEvaluation")
                        .HasColumnType("text");

                    b.Property<string>("Clinicalhistory")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("DiastolicPressure")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("ExamComplementaryId")
                        .HasColumnType("uuid");

                    b.Property<string>("FunctionalEvaluation")
                        .HasColumnType("text");

                    b.Property<Guid?>("ImageExamId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Motive")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Nextappointment")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("OxygenSaturation")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<string>("PsychologicalEvaluation")
                        .HasColumnType("text");

                    b.Property<decimal?>("Pulse")
                        .HasColumnType("numeric");

                    b.Property<string>("Recipe")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Size")
                        .HasColumnType("numeric");

                    b.Property<string>("SocialEvaluation")
                        .HasColumnType("text");

                    b.Property<decimal?>("SystolicPressure")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserCreatedById")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ExamComplementaryId");

                    b.HasIndex("ImageExamId");

                    b.HasIndex("PatientId");

                    b.HasIndex("UserCreatedById");

                    b.ToTable("consult");
                });

            modelBuilder.Entity("Domain.Entities.BackupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("backup");
                });

            modelBuilder.Entity("Domain.Entities.CivilStatusEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("civil_status");
                });

            modelBuilder.Entity("Domain.Entities.ExamEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("exam");
                });

            modelBuilder.Entity("Domain.Entities.GroupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("group");
                });

            modelBuilder.Entity("Domain.Entities.ImageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CompactUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("image");
                });

            modelBuilder.Entity("Domain.Entities.PatientEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<Guid>("AvatarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CivilStatusId")
                        .HasColumnType("uuid");

                    b.Property<int>("ConsultCount")
                        .HasColumnType("integer");

                    b.Property<string>("ContactPerson")
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("Identification")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<Guid>("RolId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TypeSex")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("CivilStatusId");

                    b.HasIndex("RolId");

                    b.ToTable("patient");
                });

            modelBuilder.Entity("Domain.Entities.RolEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("rol");
                });

            modelBuilder.Entity("Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AvatarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("03f1c228-f9fc-40a2-8a88-45d786148fe0"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RolId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("RolId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ConsultEntity", b =>
                {
                    b.HasOne("Domain.Entities.ExamEntity", "ComplementaryTest")
                        .WithMany()
                        .HasForeignKey("ExamComplementaryId");

                    b.HasOne("Domain.Entities.ImageEntity", "Image")
                        .WithMany()
                        .HasForeignKey("ImageExamId");

                    b.HasOne("Domain.Entities.PatientEntity", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserEntity", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("UserCreatedById");

                    b.Navigation("ComplementaryTest");

                    b.Navigation("Image");

                    b.Navigation("Patient");

                    b.Navigation("UserCreatedBy");
                });

            modelBuilder.Entity("Domain.Entities.ExamEntity", b =>
                {
                    b.HasOne("Domain.Entities.GroupEntity", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Domain.Entities.PatientEntity", b =>
                {
                    b.HasOne("Domain.Entities.ImageEntity", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.CivilStatusEntity", "CivilStatus")
                        .WithMany()
                        .HasForeignKey("CivilStatusId");

                    b.HasOne("Domain.Entities.RolEntity", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");

                    b.Navigation("CivilStatus");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("Domain.Entities.ImageEntity", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.RolEntity", "Rol")
                        .WithMany("Users")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Domain.Entities.RolEntity", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
