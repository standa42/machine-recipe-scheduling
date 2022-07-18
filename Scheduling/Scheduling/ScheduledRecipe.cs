using Serilog;

namespace Scheduling
{
    /// <summary>
    /// Recipe bound to specific datetime interval
    /// </summary>
    public class ScheduledRecipe
    {
        public Recipe Recipe { get; }

        /// <summary>
        /// Time interval to which is the recipe scheduled
        /// </summary>
        public DateTimeInterval TimeInterval { get; }

        public ScheduledRecipe(Recipe recipe, DateTimeInterval timeInterval)
        {
            Recipe = recipe;
            TimeInterval = timeInterval;

            Log.Debug($"Created scheduled recipe {Recipe.Name} from {TimeInterval.Start} to {TimeInterval.End}");
        }

        public override string ToString()
        {
            return $"Recipe {Recipe.Name} scheduled from {TimeInterval.Start} to {TimeInterval.End}";
        }
    }
}
