using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Scheduling;

namespace SchedulingTests
{
    [TestClass]
    public class MachineTests
    {
        /// <summary>
        /// Provide constants for fixed datetimes in tests
        /// Test date is 7/18/2022
        /// </summary>
        private static class Times
        {
            public static DateTime DayZero0h { get; }
            public static DateTime DayZero12h { get; }
            public static DateTime DayZero12h30m { get; }
            public static DateTime DayZero12h1tick { get; }

            static Times()
            {
                DayZero12h = new DateTime(2022, 7, 18, 0, 0, 0);
                DayZero12h = DayZero0h + TimeSpan.FromHours(12);
                DayZero12h30m = DayZero12h + TimeSpan.FromMinutes(30);
                DayZero12h1tick = DayZero12h + TimeSpan.FromTicks(1);
            }
        }

        /// <summary>
        /// Provides constants for durations in tests
        /// </summary>
        private static class Durations
        {
            public static TimeSpan Duration1h { get; }
            public static TimeSpan Duration30m { get; }
            public static TimeSpan Duration45m { get; }
            public static TimeSpan Duration1m { get; }
            public static TimeSpan Duration2ticks { get; }

            static Durations()
            {
                Duration1h = TimeSpan.FromHours(1);
                Duration30m = TimeSpan.FromMinutes(30);
                Duration45m = TimeSpan.FromMinutes(45);
                Duration1m = TimeSpan.FromMinutes(1);
                Duration2ticks = TimeSpan.FromTicks(2);
            }
        }


        [TestMethod]
        public void MachineCreationTest()
        {
            var machine = new Machine("TestMachine");
            Assert.IsNotNull(machine);
        }

        [TestMethod]
        public void SingleRecipeScheduleTest()
        {
            var machine = new Machine("TestMachine");
            var recipe = new Recipe("TestRecipe", Durations.Duration1h);
            var scheduledRecipe = machine.Schedule(recipe, Times.DayZero12h);
            Assert.AreSame(recipe, scheduledRecipe.Recipe);
        }

        [TestMethod]
        public void TwoRecipesScheduleWithTimeInBetweenTest()
        {
            var machine = new Machine("TestMachine");
            var recipe1 = new Recipe("TestRecipe1", Durations.Duration30m);
            var recipe2 = new Recipe("TestRecipe2", Durations.Duration30m);
            var scheduledRecipe1 = machine.Schedule(recipe1, Times.DayZero0h);
            var scheduledRecipe2 = machine.Schedule(recipe2, Times.DayZero12h);
            Assert.AreSame(recipe1, scheduledRecipe1.Recipe);
            Assert.AreSame(recipe2, scheduledRecipe2.Recipe);
        }

        [TestMethod]
        public void ThreeRecipesScheduleBothConsecutiveAndTimeInBetweenTest()
        {
            var machine = new Machine("TestMachine");
            var recipe1 = new Recipe("TestRecipe1", Durations.Duration30m);
            var recipe2 = new Recipe("TestRecipe2", Durations.Duration30m);
            var recipe3 = new Recipe("TestRecipe2", Durations.Duration30m);
            var scheduledRecipe1 = machine.Schedule(recipe1, Times.DayZero0h);
            var scheduledRecipe2 = machine.Schedule(recipe2, Times.DayZero12h30m);
            var scheduledRecipe3 = machine.Schedule(recipe3, Times.DayZero12h);
            Assert.AreSame(recipe1, scheduledRecipe1.Recipe);
            Assert.AreSame(recipe2, scheduledRecipe2.Recipe);
            Assert.AreSame(recipe3, scheduledRecipe3.Recipe);
        }

        [TestMethod]
        public void TwoRecipesScheduleConsecutiveTest()
        {
            var machine = new Machine("TestMachine");
            var recipe1 = new Recipe("TestRecipe1", Durations.Duration30m);
            var recipe2 = new Recipe("TestRecipe2", Durations.Duration30m);
            var scheduledRecipe1 = machine.Schedule(recipe1, Times.DayZero12h);
            var scheduledRecipe2 = machine.Schedule(recipe2, Times.DayZero12h30m);
            Assert.AreSame(recipe1, scheduledRecipe1.Recipe);
            Assert.AreSame(recipe2, scheduledRecipe2.Recipe);
        }

        [TestMethod]
        public void RecipeCollisionIntervalIsSubsetOfAnotherIntervalTest()
        {
            var machine = new Machine("TestMachine");
            var recipe1Superset = new Recipe("TestRecipe1", Durations.Duration1h);
            var recipe2Subset = new Recipe("TestRecipe2", Durations.Duration1m);
            machine.Schedule(recipe1Superset, Times.DayZero12h);
            var scheduledRecipe2Subset = machine.Schedule(recipe2Subset, Times.DayZero12h30m);
            Assert.IsNull(scheduledRecipe2Subset);
        }

        [TestMethod]
        public void RecipeCollisionIntervalsPartlyOverlapedTest()
        {
            var machine = new Machine("TestMachine");
            var recipe1 = new Recipe("TestRecipe1", Durations.Duration45m);
            var recipe2 = new Recipe("TestRecipe2", Durations.Duration30m);
            machine.Schedule(recipe1, Times.DayZero12h);
            var scheduledRecipe2 = machine.Schedule(recipe2, Times.DayZero12h30m);
            Assert.IsNull(scheduledRecipe2);
        }

        [TestMethod]
        public void RecipeCollisionIntervalsPartlyOverlapedByOneTickTest()
        {
            var machine = new Machine("TestMachine");
            var recipe1 = new Recipe("TestRecipe1", Durations.Duration2ticks);
            var recipe2 = new Recipe("TestRecipe2", Durations.Duration2ticks);
            machine.Schedule(recipe1, Times.DayZero12h1tick);
            var scheduledRecipe2 = machine.Schedule(recipe2, Times.DayZero12h);
            Assert.IsNull(scheduledRecipe2);
        }


    }
}
