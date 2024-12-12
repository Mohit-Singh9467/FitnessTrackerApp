namespace FitnessApp.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string Name { get; set; }
        public string ?Description { get; set; }
        public List<GoalWorkout> Workouts { get; set; } = new List<GoalWorkout>();
        public List<Goal> SubGoals { get; set; } = new List<Goal>();
    }
}
