using System;
using System.Collections.Generic;
using Serilog;

namespace Scheduling
{
    /// <summary>
    /// Machine that processes scheduled recipes
    /// </summary>
    public class Machine
    {
        public string Name { get; }

        private readonly IList<ScheduledRecipe> Recipes;

        public Machine(string name)
        {
            Name = name;
            Recipes = new List<ScheduledRecipe>();

            Log.Debug($"Machine {Name} created");
        }

        /// <summary>
        /// Schedules recipe to machine if there is no time interval conflict
        /// </summary>
        /// <param name="start">Scheduled start of the recipe on the machine</param>
        /// <returns>Scheduled recipe if scheduling was successfull (no time conflicts), null otherwise</returns>
        public ScheduledRecipe Schedule(Recipe recipe, DateTime start)
        {
            // TODO: consider keeping recipes sorted by scheduled time and hence faster interval collision check could be implemented
            var addedRecipe = new ScheduledRecipe(recipe, new DateTimeInterval(start, recipe.Duration));

            // check added recipe for datetime interval conflicts with recipes already scheduled on the machine
            var addedRecipeCollision = false;

            foreach (var machineRecipe in Recipes)
            {
                if (DateTimeInterval.Intersection(addedRecipe.TimeInterval, machineRecipe.TimeInterval))
                {
                    addedRecipeCollision = true;
                    Log.Information($"Time inteval collision of recipe on machine {machineRecipe.Recipe.Name} and added recipe {addedRecipe.Recipe.Name} on machine {Name}");
                }
            }

            if (addedRecipeCollision)
            {
                Log.Information($"Recipe {addedRecipe.Recipe.Name} was not added to machine {Name} due to collision");
                return null;
            }
            else
            {
                Recipes.Add(addedRecipe);
                Log.Information($"Recipe {addedRecipe.Recipe.Name} was added to machine {Name}");
                return addedRecipe;
            }
        }

        /// <summary>
        /// Retuns all recipes scheduled on the machine
        /// </summary>
        public IList<ScheduledRecipe> GetAllScheduledRecipes()
        {
            return Recipes;
        }
    }
}
