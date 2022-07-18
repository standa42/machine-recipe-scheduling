using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling
{
    internal class Recipe
    {
        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public Recipe(string name, TimeSpan duration)
        {
            Name = name;
            Duration = duration;
        }
    }
}
