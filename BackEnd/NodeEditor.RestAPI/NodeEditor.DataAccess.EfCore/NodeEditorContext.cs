using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeEditor.Entities;
using System.Configuration;

namespace NodeEditor.DataAccess.EfCore
{
    public class NodeEditorContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Node> Nodes { get;set; }
        public DbSet<NodeGroup> NodesGroups { get; set; }
        public DbSet<NodeType> NodeTypes { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<DataInput> DataInputs { get; set; }
        public DbSet<DataInputValue> DataInputValues { get; set; }

        public NodeEditorContext(DbContextOptions<NodeEditorContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Node>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<NodeGroup>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<NodeType>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<UserSettings>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<DataInput>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<DataInputValue>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<NodeType>()
                .HasMany(e => e.Nodes)
                .WithOne(e => e.NodeType)
                .HasForeignKey(e => e.NodeTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Node>()
                .HasOne(e => e.NodeType)
                .WithMany(e => e.Nodes)
                .HasForeignKey(e => e.NodeTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Node>()
                .HasMany(e => e.NextNode)
                .WithOne(e => e.PreviousNode)
                .HasForeignKey(e => e.PreviousNodeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NodeType>()
                .HasMany(e => e.DataInputs)
                .WithOne(e => e.NodeType)
                .HasForeignKey(e => e.NodeTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DataInput>()
                .HasOne(e => e.NodeType)
                .WithMany(e => e.DataInputs)
                .HasForeignKey(e => e.NodeTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Node>()
                .HasMany(e => e.DataInputValues)
                .WithOne(e => e.Node)
                .HasForeignKey(e => e.DataInputId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DataInputValue>()
                .HasOne(e => e.Node)
                .WithMany(e => e.DataInputValues)
                .HasForeignKey(e => e.NodeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DataInput>()
                .HasMany(e => e.DataInputValues)
                .WithOne(e => e.DataInput)
                .HasForeignKey(e => e.DataInputId);

            modelBuilder.Entity<DataInputValue>()
                .HasOne(e => e.DataInput)
                .WithMany(e => e.DataInputValues)
                .HasForeignKey(e => e.DataInputId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
