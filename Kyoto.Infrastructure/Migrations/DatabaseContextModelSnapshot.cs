﻿// <auto-generated />
using System;
using Kyoto.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kyoto.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kyoto.Infrastructure.Models.Bot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ExternalUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExternalUserId");

                    b.ToTable("Bot");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.ExecutiveCommand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalData")
                        .HasColumnType("text");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ExternalUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ExecutiveTelegramCommands");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.ExternalUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ExecutiveCommandId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PrivateId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExecutiveCommandId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TelegramUsers");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.MenuPanel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrentMenuItem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ExternalUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PreviousMenuItem")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExternalUserId")
                        .IsUnique();

                    b.ToTable("MenuPanel");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.Bot", b =>
                {
                    b.HasOne("Kyoto.Infrastructure.Models.ExternalUser", "ExternalUser")
                        .WithMany("Bots")
                        .HasForeignKey("ExternalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalUser");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.ExternalUser", b =>
                {
                    b.HasOne("Kyoto.Infrastructure.Models.ExecutiveCommand", "ExecutiveCommand")
                        .WithMany()
                        .HasForeignKey("ExecutiveCommandId");

                    b.HasOne("Kyoto.Infrastructure.Models.User", "User")
                        .WithOne("TelegramUser")
                        .HasForeignKey("Kyoto.Infrastructure.Models.ExternalUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExecutiveCommand");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.MenuPanel", b =>
                {
                    b.HasOne("Kyoto.Infrastructure.Models.ExternalUser", "ExternalUser")
                        .WithOne("MenuPanel")
                        .HasForeignKey("Kyoto.Infrastructure.Models.MenuPanel", "ExternalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalUser");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.ExternalUser", b =>
                {
                    b.Navigation("Bots");

                    b.Navigation("MenuPanel");
                });

            modelBuilder.Entity("Kyoto.Infrastructure.Models.User", b =>
                {
                    b.Navigation("TelegramUser");
                });
#pragma warning restore 612, 618
        }
    }
}
