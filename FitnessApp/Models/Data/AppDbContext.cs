using FitnessApp.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<GoalWorkout> GoalWorkouts { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring many-to-many relationship
        modelBuilder.Entity<GoalWorkout>()
            .HasKey(gw => new { gw.GoalId, gw.WorkoutId });

        modelBuilder.Entity<GoalWorkout>()
            .HasOne(gw => gw.Goal)
            .WithMany(g => g.Workouts)
            .HasForeignKey(gw => gw.GoalId);

        modelBuilder.Entity<GoalWorkout>()
            .HasOne(gw => gw.Workout)
            .WithMany(w => w.Goals)
            .HasForeignKey(gw => gw.WorkoutId);

        // Self-referencing relationship for sub-goals
        modelBuilder.Entity<Goal>()
            .HasMany(g => g.SubGoals)
            .WithOne()
            .HasForeignKey("ParentGoalId");
    }
}
  

