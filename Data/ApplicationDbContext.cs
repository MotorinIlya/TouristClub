using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TouristClub.Models;

namespace TouristClub.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Athletes> Athletes { get; set; }

    public virtual DbSet<Coaches> Coaches { get; set; }

    public virtual DbSet<Competitions> Competitions { get; set; }

    public virtual DbSet<CompetitionsToTourists> CompetitionsToTourists { get; set; }

    public virtual DbSet<Diaryes> Diaryes { get; set; }

    public virtual DbSet<Groups> Groups { get; set; }

    public virtual DbSet<Leaders> Leaders { get; set; }

    public virtual DbSet<PartisipantsTrips> PartisipantsTrips { get; set; }

    public virtual DbSet<PlannedTrips> PlannedTrips { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Routes> Routes { get; set; }

    public virtual DbSet<Schedule> Schedule { get; set; }

    public virtual DbSet<Sections> Sections { get; set; }

    public virtual DbSet<StatusTrips> StatusTrips { get; set; }

    public virtual DbSet<TouristToGroup> TouristToGroup { get; set; }

    public virtual DbSet<Tourists> Tourists { get; set; }

    public virtual DbSet<Training> Training { get; set; }

    public virtual DbSet<TrainingAttendance> TrainingAttendance { get; set; }

    public virtual DbSet<Trips> Trips { get; set; }

    public virtual DbSet<TypeTourist> TypeTourist { get; set; }

    public virtual DbSet<TypeTrips> TypeTrips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TouristClub;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Athletes>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Athletes_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.tourist).WithMany(p => p.Athletes)
                .HasForeignKey(d => d.tourist_id)
                .HasConstraintName("Athletes_Athletes_Tourist_1");
        });

        modelBuilder.Entity<Coaches>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Coaches_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.section).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.section_id)
                .HasConstraintName("Coaches_Sections_Coaches_1");

            entity.HasOne(d => d.tourist).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.tourist_id)
                .HasConstraintName("Coaches_Coaches_Tourist_1");
        });

        modelBuilder.Entity<Competitions>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Competitions_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<CompetitionsToTourists>(entity =>
        {
            entity.HasKey(e => e.id).HasName("CompetitionsToTourists_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_competitonNavigation).WithMany(p => p.CompetitionsToTourists)
                .HasForeignKey(d => d.id_competiton)
                .HasConstraintName("CompetitionsToTourists_Comp_1");

            entity.HasOne(d => d.id_touristNavigation).WithMany(p => p.CompetitionsToTourists)
                .HasForeignKey(d => d.id_tourist)
                .HasConstraintName("CompetitionsToTourists_Comp_2");
        });

        modelBuilder.Entity<Diaryes>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Diaryes_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Groups>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Groups_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_coachNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.id_coach)
                .HasConstraintName("Groups_Groups_1");

            entity.HasOne(d => d.id_sectionNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.id_section)
                .HasConstraintName("Groups_Groups_2");
        });

        modelBuilder.Entity<Leaders>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Leaders_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<PartisipantsTrips>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PartisipantsTrips_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_roleNavigation).WithMany(p => p.PartisipantsTrips)
                .HasForeignKey(d => d.id_role)
                .HasConstraintName("PartisipantsTrips_Partisipants_3");

            entity.HasOne(d => d.id_touristNavigation).WithMany(p => p.PartisipantsTrips)
                .HasForeignKey(d => d.id_tourist)
                .HasConstraintName("PartisipantsTrips_Partisipants_2");

            entity.HasOne(d => d.id_tripNavigation).WithMany(p => p.PartisipantsTrips)
                .HasForeignKey(d => d.id_trip)
                .HasConstraintName("PartisipantsTrips_Partisipants_1");
        });

        modelBuilder.Entity<PlannedTrips>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PlannedTrips_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_diaryNavigation).WithMany(p => p.PlannedTrips)
                .HasForeignKey(d => d.id_diary)
                .HasConstraintName("PlannedTrips_Planned_2");

            entity.HasOne(d => d.id_tripNavigation).WithMany(p => p.PlannedTrips)
                .HasForeignKey(d => d.id_trip)
                .HasConstraintName("PlannedTrips_Planned_1");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Roles_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Routes>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Routes_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_type_tripNavigation).WithMany(p => p.Routes)
                .HasForeignKey(d => d.id_type_trip)
                .HasConstraintName("Routes_TypeTrip_1");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Schedule_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.id_coachNavigation).WithMany(p => p.Schedule)
                .HasForeignKey(d => d.id_coach)
                .HasConstraintName("Schedule_Schedule_2");

            entity.HasOne(d => d.id_sectionNavigation).WithMany(p => p.Schedule)
                .HasForeignKey(d => d.id_section)
                .HasConstraintName("Schedule_Schedule_1");
        });

        modelBuilder.Entity<Sections>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Sections_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_leaderNavigation).WithMany(p => p.Sections)
                .HasForeignKey(d => d.id_leader)
                .HasConstraintName("Sections_Leaders_1");
        });

        modelBuilder.Entity<StatusTrips>(entity =>
        {
            entity.HasKey(e => e.id).HasName("StatusTrips_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TouristToGroup>(entity =>
        {
            entity.HasKey(e => e.id).HasName("TouristToGroup_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_groupNavigation).WithMany(p => p.TouristToGroup)
                .HasForeignKey(d => d.id_group)
                .HasConstraintName("TouristToGroup_GT_2");

            entity.HasOne(d => d.id_touristNavigation).WithMany(p => p.TouristToGroup)
                .HasForeignKey(d => d.id_tourist)
                .HasConstraintName("TouristToGroup_GT_1");
        });

        modelBuilder.Entity<Tourists>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Tourists_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.gender).HasMaxLength(1);

            entity.HasOne(d => d.type_touristNavigation).WithMany(p => p.Tourists)
                .HasForeignKey(d => d.type_tourist)
                .HasConstraintName("Tourists_TypeTourist_Tourist_1");
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Training_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.time_to_training).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.id_coachNavigation).WithMany(p => p.Training)
                .HasForeignKey(d => d.id_coach)
                .HasConstraintName("Training_Training_2");

            entity.HasOne(d => d.id_groupNavigation).WithMany(p => p.Training)
                .HasForeignKey(d => d.id_group)
                .HasConstraintName("Training_Training_1");
        });

        modelBuilder.Entity<TrainingAttendance>(entity =>
        {
            entity.HasKey(e => e.id).HasName("TrainingAttendance_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.id_touristNavigation).WithMany(p => p.TrainingAttendance)
                .HasForeignKey(d => d.id_tourist)
                .HasConstraintName("TrainingAttendance_Attendance_2");

            entity.HasOne(d => d.id_trainingNavigation).WithMany(p => p.TrainingAttendance)
                .HasForeignKey(d => d.id_training)
                .HasConstraintName("TrainingAttendance_Attendance_1");
        });

        modelBuilder.Entity<Trips>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Trips_pkey");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.time_to_begin).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.id_instructorNavigation).WithMany(p => p.Trips)
                .HasForeignKey(d => d.id_instructor)
                .HasConstraintName("Trips_Trips_2");

            entity.HasOne(d => d.id_routeNavigation).WithMany(p => p.Trips)
                .HasForeignKey(d => d.id_route)
                .HasConstraintName("Trips_Trips_1");

            entity.HasOne(d => d.id_statusNavigation).WithMany(p => p.Trips)
                .HasForeignKey(d => d.id_status)
                .HasConstraintName("Trips_Trips_3");
        });

        modelBuilder.Entity<TypeTourist>(entity =>
        {
            entity.HasKey(e => e.id).HasName("TypeTourist_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TypeTrips>(entity =>
        {
            entity.HasKey(e => e.id).HasName("TypeTrips_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
