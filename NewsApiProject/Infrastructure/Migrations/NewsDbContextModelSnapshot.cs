﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(NewsDbContext))]
    partial class NewsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "author");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "content");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<int>("NewsApiResponseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "publishedAt");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.Property<string>("SourceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "title");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "url");

                    b.Property<string>("UrlToImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "urlToImage");

                    b.HasKey("ArticleId");

                    b.HasIndex("NewsApiResponseId");

                    b.HasIndex("SourceId");

                    b.ToTable("Articles");

                    b.HasAnnotation("Relational:JsonPropertyName", "articles");
                });

            modelBuilder.Entity("Domain.Models.NewsApiResponse", b =>
                {
                    b.Property<int>("NewsApiResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewsApiResponseId"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "status");

                    b.Property<int>("TotalResults")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "totalResults");

                    b.HasKey("NewsApiResponseId");

                    b.ToTable("NewsApiResponses");
                });

            modelBuilder.Entity("Domain.Models.Source", b =>
                {
                    b.Property<int>("Unique")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Unique");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Unique"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("SourceId")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.HasKey("Unique");

                    b.ToTable("Sources");

                    b.HasAnnotation("Relational:JsonPropertyName", "source");
                });

            modelBuilder.Entity("Domain.Models.Article", b =>
                {
                    b.HasOne("Domain.Models.NewsApiResponse", "NewsApiResponse")
                        .WithMany("Articles")
                        .HasForeignKey("NewsApiResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Source", "Source")
                        .WithMany("Articles")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NewsApiResponse");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("Domain.Models.NewsApiResponse", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Domain.Models.Source", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
