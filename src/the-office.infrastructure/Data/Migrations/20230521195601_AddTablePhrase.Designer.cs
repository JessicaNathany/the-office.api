﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using the_office.infrastructure.Data.Context;

#nullable disable

namespace the_office.infrastructure.Data.Migrations
{
    [DbContext(typeof(TheOfficeDbContext))]
    [Migration("20230521195601_AddTablePhrase")]
    partial class AddTablePhrase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("the_office.domain.Entities.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NameActor")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Character", (string)null);
                });

            modelBuilder.Entity("the_office.domain.Entities.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AirDate")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episode", (string)null);
                });

            modelBuilder.Entity("the_office.domain.Entities.EpisodeCharacter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdCharacter")
                        .HasColumnType("integer");

                    b.Property<int>("IdEpisode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdCharacter");

                    b.HasIndex("IdEpisode");

                    b.ToTable("EpisodeCharacter", (string)null);
                });

            modelBuilder.Entity("the_office.domain.Entities.Phrases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Phrase")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Phrase", (string)null);
                });

            modelBuilder.Entity("the_office.domain.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Season", (string)null);
                });

            modelBuilder.Entity("the_office.domain.Entities.Character", b =>
                {
                    b.HasOne("the_office.domain.Entities.Season", null)
                        .WithMany("Characters")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("the_office.domain.Entities.Episode", b =>
                {
                    b.HasOne("the_office.domain.Entities.Season", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId")
                        .IsRequired();

                    b.Navigation("Season");
                });

            modelBuilder.Entity("the_office.domain.Entities.EpisodeCharacter", b =>
                {
                    b.HasOne("the_office.domain.Entities.Character", "Character")
                        .WithMany("Episodes")
                        .HasForeignKey("IdCharacter")
                        .IsRequired();

                    b.HasOne("the_office.domain.Entities.Episode", "Episode")
                        .WithMany("Characters")
                        .HasForeignKey("IdEpisode")
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Episode");
                });

            modelBuilder.Entity("the_office.domain.Entities.Character", b =>
                {
                    b.Navigation("Episodes");
                });

            modelBuilder.Entity("the_office.domain.Entities.Episode", b =>
                {
                    b.Navigation("Characters");
                });

            modelBuilder.Entity("the_office.domain.Entities.Season", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
