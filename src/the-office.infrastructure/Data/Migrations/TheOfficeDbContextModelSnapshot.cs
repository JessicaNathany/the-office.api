﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using the_office.infrastructure.Data.Context;

#nullable disable

namespace the_office.infrastructure.Data.Migrations
{
    [DbContext(typeof(TheOfficeDbContext))]
    partial class TheOfficeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CharacterEpisode", b =>
                {
                    b.Property<int>("CharactersId")
                        .HasColumnType("integer");

                    b.Property<int>("EpisodesId")
                        .HasColumnType("integer");

                    b.HasKey("CharactersId", "EpisodesId");

                    b.HasIndex("EpisodesId");

                    b.ToTable("CharacterEpisode");
                });

            modelBuilder.Entity("CharacterSeason", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<int>("SeasonId")
                        .HasColumnType("integer");

                    b.HasKey("CharacterId", "SeasonId");

                    b.HasIndex("SeasonId");

                    b.ToTable("CharacterSeason");
                });

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
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("NameActor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("SeasonId");

                    b.ToTable("Character", (string)null);
                });

            modelBuilder.Entity("the_office.domain.Entities.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AirDate")
                        .HasColumnType("date");

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("SeasonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("SeasonId");

                    b.ToTable("Episode", (string)null);
                });

            modelBuilder.Entity("the_office.domain.Entities.Phrases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<string>("Phrase")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

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

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(750)
                        .HasColumnType("character varying(750)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("TotalEpisodes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Season", (string)null);
                });

            modelBuilder.Entity("CharacterEpisode", b =>
                {
                    b.HasOne("the_office.domain.Entities.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .IsRequired();

                    b.HasOne("the_office.domain.Entities.Episode", null)
                        .WithMany()
                        .HasForeignKey("EpisodesId")
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterSeason", b =>
                {
                    b.HasOne("the_office.domain.Entities.Character", null)
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .IsRequired();

                    b.HasOne("the_office.domain.Entities.Season", null)
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .IsRequired();
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

            modelBuilder.Entity("the_office.domain.Entities.Season", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
