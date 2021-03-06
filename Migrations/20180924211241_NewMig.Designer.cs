﻿// <auto-generated />
using firstbelt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace firstbelt.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20180924211241_NewMig")]
    partial class NewMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("firstbelt.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Desc")
                        .IsRequired();

                    b.Property<string>("DurType")
                        .IsRequired();

                    b.Property<int>("Duration");

                    b.Property<DateTime>("Time");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("firstbelt.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<int>("UserId");

                    b.HasKey("ParticipantId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("firstbelt.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("firstbelt.Models.Activity", b =>
                {
                    b.HasOne("firstbelt.Models.User", "User")
                        .WithMany("Activities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("firstbelt.Models.Participant", b =>
                {
                    b.HasOne("firstbelt.Models.Activity", "Activity")
                        .WithMany("Participants")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("firstbelt.Models.User", "User")
                        .WithMany("Participants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
