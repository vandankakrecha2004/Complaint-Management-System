using System;
using System.Collections.Generic;
using DataProvider.MySQL;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DataProvider.MySQL.Config;

public partial class IiotIntelliscadaDbContext : DbContext
{
    public IiotIntelliscadaDbContext()
    {
    }

    public IiotIntelliscadaDbContext(DbContextOptions<IiotIntelliscadaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<comp_actioncomponent> comp_actioncomponents { get; set; }

    public virtual DbSet<comp_actiontypesmaster> comp_actiontypesmasters { get; set; }

    public virtual DbSet<comp_allocation> comp_allocations { get; set; }

    public virtual DbSet<comp_asset_action_history> comp_asset_action_histories { get; set; }

    public virtual DbSet<comp_asset_status_history> comp_asset_status_histories { get; set; }

    public virtual DbSet<comp_assignment> comp_assignments { get; set; }

    public virtual DbSet<comp_closingremarkmaster> comp_closingremarkmasters { get; set; }

    public virtual DbSet<comp_comment> comp_comments { get; set; }

    public virtual DbSet<comp_complaintsubjectmaster> comp_complaintsubjectmasters { get; set; }

    public virtual DbSet<comp_complainttypesmaster> comp_complainttypesmasters { get; set; }

    public virtual DbSet<comp_componentmaster> comp_componentmasters { get; set; }

    public virtual DbSet<comp_componenttypesmaster> comp_componenttypesmasters { get; set; }

    public virtual DbSet<comp_compstatusmaster> comp_compstatusmasters { get; set; }

    public virtual DbSet<comp_defecttypesmaster> comp_defecttypesmasters { get; set; }

    public virtual DbSet<comp_documents> comp_documents { get; set; }

    public virtual DbSet<comp_generation> comp_generations { get; set; }

    public virtual DbSet<comp_issuetypemaster> comp_issuetypemasters { get; set; }

    public virtual DbSet<comp_mastersla> comp_masterslas { get; set; }

    public virtual DbSet<comp_notificationsqueue> comp_notificationsqueues { get; set; }

    public virtual DbSet<comp_observationmaster> comp_observationmasters { get; set; }

    public virtual DbSet<comp_problemsmaster> comp_problemsmasters { get; set; }

    public virtual DbSet<comp_projectmaster> comp_projectmasters { get; set; }

    public virtual DbSet<comp_readingsnapshot> comp_readingsnapshots { get; set; }

    public virtual DbSet<comp_reason> comp_reasons { get; set; }

    public virtual DbSet<comp_reasontypesmaster> comp_reasontypesmasters { get; set; }

    public virtual DbSet<comp_replace_visit_reason> comp_replace_visit_reasons { get; set; }

    public virtual DbSet<comp_site_alw_history> comp_site_alw_histories { get; set; }

    public virtual DbSet<comp_sitevisit> comp_sitevisits { get; set; }

    public virtual DbSet<comp_solutionmaster> comp_solutionmasters { get; set; }

    public virtual DbSet<comp_statushistory> comp_statushistories { get; set; }

    public virtual DbSet<comp_subproblemsmaster> comp_subproblemsmasters { get; set; }

    public virtual DbSet<comp_systemtypesmaster> comp_systemtypesmasters { get; set; }

    public virtual DbSet<comp_visittypesmaster> comp_visittypesmasters { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=localhost;database=iiot_intelliscada_db_ongc;user=root;password=Vandan@2004;connection timeout=60", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<comp_actioncomponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<comp_actiontypesmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_allocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AllocatedAgency).HasComment("DepartmentId");
            entity.Property(e => e.AllocatedSubNock).HasDefaultValueSql("'-1'");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_asset_action_history>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_asset_status_history>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AssignmentTo).HasComment("UserId");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsVisitRequired).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_closingremarkmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_complaintsubjectmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<comp_complainttypesmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_componentmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_componenttypesmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_compstatusmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<comp_defecttypesmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_documents>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsClosureDoc).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_generation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AllocSubNockId).HasDefaultValueSql("'-1'");
            entity.Property(e => e.IsAssetReplaced).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsInWarranty).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsVisitRequired).HasDefaultValueSql("b'0'");
            entity.Property(e => e.ProjectId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<comp_issuetypemaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_mastersla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<comp_notificationsqueue>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.Property(e => e.IsProcessed).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_observationmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_problemsmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_projectmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_readingsnapshot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_reason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_reasontypesmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_replace_visit_reason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<comp_site_alw_history>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<comp_sitevisit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsAlwDone).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsSiteVisited).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_solutionmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_statushistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_subproblemsmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_systemtypesmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<comp_visittypesmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IsDelete).HasDefaultValueSql("b'0'");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
