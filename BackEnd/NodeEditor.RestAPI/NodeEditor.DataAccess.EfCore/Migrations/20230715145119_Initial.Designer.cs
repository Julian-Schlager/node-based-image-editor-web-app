﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodeEditor.DataAccess.EfCore;

#nullable disable

namespace NodeEditor.DataAccess.EfCore.Migrations
{
    [DbContext(typeof(NodeEditorContext))]
    [Migration("20230715145119_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NodeEditor.Entities.DataInput", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DataInputType")
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NodeTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NodeTypeId");

                    b.ToTable("DataInputs");
                });

            modelBuilder.Entity("NodeEditor.Entities.DataInputValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DataInputId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("NodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DataInputId");

                    b.HasIndex("NodeId");

                    b.ToTable("DataInputValues");
                });

            modelBuilder.Entity("NodeEditor.Entities.Node", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("NodeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NodeTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NodeGroupId");

                    b.HasIndex("NodeTypeId");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("NodeEditor.Entities.NodeGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("NodesGroups");
                });

            modelBuilder.Entity("NodeEditor.Entities.NodeType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModificationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NodeTypes");
                });

            modelBuilder.Entity("NodeEditor.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NodeEditor.Entities.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("NodeEditor.Entities.DataInput", b =>
                {
                    b.HasOne("NodeEditor.Entities.NodeType", "NodeType")
                        .WithMany("DataInputs")
                        .HasForeignKey("NodeTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NodeType");
                });

            modelBuilder.Entity("NodeEditor.Entities.DataInputValue", b =>
                {
                    b.HasOne("NodeEditor.Entities.DataInput", "DataInput")
                        .WithMany("DataInputValues")
                        .HasForeignKey("DataInputId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NodeEditor.Entities.Node", "Node")
                        .WithMany("DataInputValues")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DataInput");

                    b.Navigation("Node");
                });

            modelBuilder.Entity("NodeEditor.Entities.Node", b =>
                {
                    b.HasOne("NodeEditor.Entities.NodeGroup", "NodeGroup")
                        .WithMany("Nodes")
                        .HasForeignKey("NodeGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NodeEditor.Entities.NodeType", "NodeType")
                        .WithMany("Nodes")
                        .HasForeignKey("NodeTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NodeGroup");

                    b.Navigation("NodeType");
                });

            modelBuilder.Entity("NodeEditor.Entities.NodeGroup", b =>
                {
                    b.HasOne("NodeEditor.Entities.User", "User")
                        .WithMany("NodeGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NodeEditor.Entities.UserSettings", b =>
                {
                    b.HasOne("NodeEditor.Entities.User", "User")
                        .WithOne("UserSettings")
                        .HasForeignKey("NodeEditor.Entities.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NodeEditor.Entities.DataInput", b =>
                {
                    b.Navigation("DataInputValues");
                });

            modelBuilder.Entity("NodeEditor.Entities.Node", b =>
                {
                    b.Navigation("DataInputValues");
                });

            modelBuilder.Entity("NodeEditor.Entities.NodeGroup", b =>
                {
                    b.Navigation("Nodes");
                });

            modelBuilder.Entity("NodeEditor.Entities.NodeType", b =>
                {
                    b.Navigation("DataInputs");

                    b.Navigation("Nodes");
                });

            modelBuilder.Entity("NodeEditor.Entities.User", b =>
                {
                    b.Navigation("NodeGroups");

                    b.Navigation("UserSettings")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
