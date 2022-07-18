using System;
using Serilog;

namespace Scheduling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Application started");

            var today12h = DateTime.Today + TimeSpan.FromHours(12);
            var today12h30m = today12h + TimeSpan.FromMinutes(30);
            var today13h29m = today12h30m + TimeSpan.FromMinutes(59);
            var today11h59m = today12h - TimeSpan.FromMinutes(1);

            var heatingMachine = new Machine("Heating-Machine");
            var mixingMachine = new Machine("Mixing-Machine");
            var packagingMachine = new Machine("Packaging-Machine");

            var heatingRecipe1 = new Recipe("Heating-Recipe-1", TimeSpan.FromMinutes(30));
            var heatingRecipe2 = new Recipe("Heating-Recipe-2", TimeSpan.FromMinutes(60));
            var heatingRecipe3_ConflictWithRecipe2 = new Recipe("Heating-Recipe-2_Conflict-With-3", TimeSpan.FromMinutes(5));
            var heatingRecipt4_ConflictWithRecipe1 = new Recipe("Heating-Recipe-4_Conflict-With-1", TimeSpan.FromSeconds(61));

            heatingMachine.Schedule(heatingRecipe1, today12h);
            heatingMachine.Schedule(heatingRecipe2 , today12h30m);
            heatingMachine.Schedule(heatingRecipe3_ConflictWithRecipe2, today13h29m);
            heatingMachine.Schedule(heatingRecipt4_ConflictWithRecipe1, today11h59m);

            Log.Information($"Recipes scheduled on {heatingMachine.Name}:");
            foreach (var heatingMachineRecipe in heatingMachine.GetAllScheduledRecipes())
            {
                Log.Information($"- {heatingMachineRecipe}");
            }
            
            Log.Information("Application ended, press any key to close..");
            Console.ReadKey();
        }
    }
}
