using System;
using Serilog;

namespace Scheduling
{
    /// <summary>
    /// Recipe to be processed on a machine
    /// </summary>
    public class Recipe
    {
        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public Recipe(string name, TimeSpan duration)
        {
            Name = name;
            Duration = duration;

            Log.Debug($"Recipe {Name} with duration {duration} created");
        }
    }
}
