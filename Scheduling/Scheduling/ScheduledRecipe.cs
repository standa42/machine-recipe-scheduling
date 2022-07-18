using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling
{
    internal class ScheduledRecipe
    {
        public Recipe Recipe { get; set; }

        public TimeInterval TimeInterval { get; set; }

        public ScheduledRecipe(Recipe recipe, TimeInterval timeInterval)
        {
            Recipe = recipe;
            TimeInterval = timeInterval;
        }
    }
}
