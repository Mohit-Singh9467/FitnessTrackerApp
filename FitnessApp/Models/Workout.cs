namespace FitnessApp.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public string WorkoutType { get; set; }
        public string Description { get; set; }
        public List<GoalWorkout> Goals { get; set; } = new List<GoalWorkout>();
    }
}


