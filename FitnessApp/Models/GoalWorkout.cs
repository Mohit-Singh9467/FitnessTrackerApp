using FitnessApp.Models;

public class GoalWorkout
{
    public int GoalId { get; set; }
    public Goal Goal { get; set; }

    public int WorkoutId { get; set; }
    public Workout Workout { get; set; }

    public int Repetitions { get; set; }
    public int Sets { get; set; }
}