using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling
{
    internal class Machine
    {
        public string Name { get; set; }

        private List<ScheduledRecipe> Recipes { get; set; }

        public Machine(string name)
        {
            Name = name;
            Recipes = new List<ScheduledRecipe>();
        }

        public ScheduledRecipe Schedule(Recipe recipe, DateTime start)
        {
            var addedRecipe = new ScheduledRecipe(recipe, new TimeInterval(start, recipe.Duration));

            var addedRecipeCollision = false; 

            foreach (var machineRecipe in Recipes)
            {
                if (TimeInterval.Intersection(addedRecipe.TimeInterval, machineRecipe.TimeInterval))
                {
                    addedRecipeCollision = true;
                    Console.WriteLine($"Time inteval collision of recipe on machine {machineRecipe.Recipe.Name} and added recipe {addedRecipe.Recipe.Name} on machine {Name}");
                }
            }

            return addedRecipeCollision ? null : addedRecipe;
        }

        public List<ScheduledRecipe> GetAllScheduledRecipes()
        {
            return Recipes;
        }
    }
}
