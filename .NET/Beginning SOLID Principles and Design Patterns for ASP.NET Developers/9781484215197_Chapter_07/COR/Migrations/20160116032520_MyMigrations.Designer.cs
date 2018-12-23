using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using COR.Core;

namespace COR.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20160116032520_MyMigrations")]
    partial class MyMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("COR.Core.Customer", b =>
                {
                    b.Property<string>("CustomerID");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("CustomerID");

                    b.HasAnnotation("Relational:TableName", "Customers");
                });

            modelBuilder.Entity("COR.Core.FileStoreEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<DateTime>("UploadedOn");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "FileStore");
                });
        }
    }
}
