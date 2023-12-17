﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(FeedbacksDbContext))]
    [Migration("20230306045931_conf")]
    partial class conf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblPages");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PasswordReset")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblUsers");
                });

            modelBuilder.Entity("Domain.Entities.UserPages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FkIdPage")
                        .HasColumnType("int");

                    b.Property<int>("FkIdUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TblUserPages");
                });

            modelBuilder.Entity("Infrastructure.Data.RelationshipExpertFeedbak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FkIdExpert")
                        .HasColumnType("int");

                    b.Property<int>("FkIdFeedback")
                        .HasColumnType("int");

                    b.Property<int?>("FkIdSpecialty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FkIdExpert");

                    b.HasIndex("FkIdFeedback");

                    b.HasIndex("FkIdSpecialty");

                    b.ToTable("TblRelationshipExpertFeedbak");
                });

            modelBuilder.Entity("Infrastructure.Data.RelationshipTagFeedbak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FkIdFeedback")
                        .HasColumnType("int");

                    b.Property<int>("FkIdTag")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FkIdFeedback");

                    b.HasIndex("FkIdTag");

                    b.ToTable("TblRelationshipTagFeedbak");
                });

            modelBuilder.Entity("Infrastructure.Data.TblAnsower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Confirrmed")
                        .HasColumnType("bit");

                    b.Property<int>("FkIdExpert")
                        .HasColumnType("int");

                    b.Property<int>("FkIdFeedback")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReferralDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Respond")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FkIdFeedback");

                    b.ToTable("TblAnsower");
                });

            modelBuilder.Entity("Infrastructure.Data.TblCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FkIdCustomer")
                        .HasColumnType("int");

                    b.Property<int>("FkIdProduct")
                        .HasColumnType("int");

                    b.Property<string>("Resources")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Source")
                        .HasColumnType("tinyint");

                    b.Property<string>("SourceAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FkIdCustomer");

                    b.HasIndex("FkIdProduct");

                    b.ToTable("TblCase");
                });

            modelBuilder.Entity("Infrastructure.Data.TblCustomer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAndFamily")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblCustomer");
                });

            modelBuilder.Entity("Infrastructure.Data.TblExpert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Education")
                        .HasColumnType("tinyint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldOfStudy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FkIdSpecialty")
                        .HasColumnType("int");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FkIdSpecialty");

                    b.ToTable("TblExpert");
                });

            modelBuilder.Entity("Infrastructure.Data.TblFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FkIdCustomer")
                        .HasColumnType("int");

                    b.Property<int>("FkIdProduct")
                        .HasColumnType("int");

                    b.Property<byte>("Priorty")
                        .HasColumnType("tinyint");

                    b.Property<string>("Resources")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RespondDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<byte?>("Similarity")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Source")
                        .HasColumnType("tinyint");

                    b.Property<string>("SourceAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("State")
                        .HasColumnType("tinyint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FkIdCustomer");

                    b.HasIndex("FkIdProduct");

                    b.ToTable("TblFeedback");
                });

            modelBuilder.Entity("Infrastructure.Data.TblKnowledgeTree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("TblKnowledgeTree");
                });

            modelBuilder.Entity("Infrastructure.Data.TblProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblProduct");
                });

            modelBuilder.Entity("Infrastructure.Data.TblSpecialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblSpecialtie");
                });

            modelBuilder.Entity("Infrastructure.Data.TblTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TblTag");
                });

            modelBuilder.Entity("Infrastructure.Data.RelationshipExpertFeedbak", b =>
                {
                    b.HasOne("Infrastructure.Data.TblExpert", "TblExpertVi")
                        .WithMany("TblRelationshipTagFeedbakIs")
                        .HasForeignKey("FkIdExpert")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Infrastructure.Data.TblFeedback", "TblFeedbackVi")
                        .WithMany("TblRelationshipExpertFeedbakIc")
                        .HasForeignKey("FkIdFeedback")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Infrastructure.Data.TblSpecialty", "TblSpecialtyVi")
                        .WithMany("TblRelationshipTagFeedbakIs")
                        .HasForeignKey("FkIdSpecialty")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("TblExpertVi");

                    b.Navigation("TblFeedbackVi");

                    b.Navigation("TblSpecialtyVi");
                });

            modelBuilder.Entity("Infrastructure.Data.RelationshipTagFeedbak", b =>
                {
                    b.HasOne("Infrastructure.Data.TblFeedback", "TblFeedbackVi")
                        .WithMany("TblRelationshipTagFeedbakIc")
                        .HasForeignKey("FkIdFeedback")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Infrastructure.Data.TblTag", "TblTagVi")
                        .WithMany("TblRelationshipTagFeedbakIc")
                        .HasForeignKey("FkIdTag")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TblFeedbackVi");

                    b.Navigation("TblTagVi");
                });

            modelBuilder.Entity("Infrastructure.Data.TblAnsower", b =>
                {
                    b.HasOne("Infrastructure.Data.TblFeedback", "TblFeedbackVi")
                        .WithMany("TblAnsowerIc")
                        .HasForeignKey("FkIdFeedback")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TblFeedbackVi");
                });

            modelBuilder.Entity("Infrastructure.Data.TblCase", b =>
                {
                    b.HasOne("Infrastructure.Data.TblCustomer", "TblCustomerVi")
                        .WithMany("TblCaseIc")
                        .HasForeignKey("FkIdCustomer")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Infrastructure.Data.TblProduct", "TblProductVi")
                        .WithMany("TblCaseIc")
                        .HasForeignKey("FkIdProduct")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TblCustomerVi");

                    b.Navigation("TblProductVi");
                });

            modelBuilder.Entity("Infrastructure.Data.TblExpert", b =>
                {
                    b.HasOne("Infrastructure.Data.TblSpecialty", "TblSpecialtyVi")
                        .WithMany("TblExpertIc")
                        .HasForeignKey("FkIdSpecialty")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TblSpecialtyVi");
                });

            modelBuilder.Entity("Infrastructure.Data.TblFeedback", b =>
                {
                    b.HasOne("Infrastructure.Data.TblCustomer", "TblCustomerVi")
                        .WithMany("TblFeedbackIc")
                        .HasForeignKey("FkIdCustomer")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Infrastructure.Data.TblProduct", "TblProductVi")
                        .WithMany("TblFeedbackIc")
                        .HasForeignKey("FkIdProduct")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TblCustomerVi");

                    b.Navigation("TblProductVi");
                });

            modelBuilder.Entity("Infrastructure.Data.TblKnowledgeTree", b =>
                {
                    b.HasOne("Infrastructure.Data.TblKnowledgeTree", "Parent")
                        .WithMany("TblKnowledgeTreeIc")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Infrastructure.Data.TblCustomer", b =>
                {
                    b.Navigation("TblCaseIc");

                    b.Navigation("TblFeedbackIc");
                });

            modelBuilder.Entity("Infrastructure.Data.TblExpert", b =>
                {
                    b.Navigation("TblRelationshipTagFeedbakIs");
                });

            modelBuilder.Entity("Infrastructure.Data.TblFeedback", b =>
                {
                    b.Navigation("TblAnsowerIc");

                    b.Navigation("TblRelationshipExpertFeedbakIc");

                    b.Navigation("TblRelationshipTagFeedbakIc");
                });

            modelBuilder.Entity("Infrastructure.Data.TblKnowledgeTree", b =>
                {
                    b.Navigation("TblKnowledgeTreeIc");
                });

            modelBuilder.Entity("Infrastructure.Data.TblProduct", b =>
                {
                    b.Navigation("TblCaseIc");

                    b.Navigation("TblFeedbackIc");
                });

            modelBuilder.Entity("Infrastructure.Data.TblSpecialty", b =>
                {
                    b.Navigation("TblExpertIc");

                    b.Navigation("TblRelationshipTagFeedbakIs");
                });

            modelBuilder.Entity("Infrastructure.Data.TblTag", b =>
                {
                    b.Navigation("TblRelationshipTagFeedbakIc");
                });
#pragma warning restore 612, 618
        }
    }
}