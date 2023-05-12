﻿// <auto-generated />
using System;
using Kyoto.Database.BotFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kyoto.Database.Migrations.BotFactory
{
    [DbContext(typeof(DatabaseBotFactoryContext))]
    partial class DatabaseBotFactoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.Bot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("CanJoinGroups")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanReadAllGroupMessages")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ExternalUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PrivateId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("SupportsInlineQueries")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExternalUserId");

                    b.ToTable("Bots");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.ExternalUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

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

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ExternalUsers");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.MenuPanel", b =>
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

                    b.ToTable("MenuPanels");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.User", b =>
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

            modelBuilder.Entity("Kyoto.Database.CommonModels.Command", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalData")
                        .HasColumnType("text");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ExternalUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("Step")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.Bot", b =>
                {
                    b.HasOne("Kyoto.Database.BotFactory.Models.ExternalUser", "ExternalUser")
                        .WithMany()
                        .HasForeignKey("ExternalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalUser");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.ExternalUser", b =>
                {
                    b.HasOne("Kyoto.Database.BotFactory.Models.User", "User")
                        .WithOne("TelegramUser")
                        .HasForeignKey("Kyoto.Database.BotFactory.Models.ExternalUser", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.MenuPanel", b =>
                {
                    b.HasOne("Kyoto.Database.BotFactory.Models.ExternalUser", "ExternalUser")
                        .WithOne("MenuPanel")
                        .HasForeignKey("Kyoto.Database.BotFactory.Models.MenuPanel", "ExternalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalUser");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.ExternalUser", b =>
                {
                    b.Navigation("MenuPanel");
                });

            modelBuilder.Entity("Kyoto.Database.BotFactory.Models.User", b =>
                {
                    b.Navigation("TelegramUser");
                });
#pragma warning restore 612, 618
        }
    }
}
