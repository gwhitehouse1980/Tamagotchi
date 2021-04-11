using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Implementations.Dragon;
using Tamagotchi.Core.Interfaces;
using Tamagotchi.Core.Models;
using Tamagotchi.Helpers;

namespace Tamagotchi.Implementations
{
    [DisallowConcurrentExecution]
    public class GameService : IJob
    {
        private readonly ILifecycleService<Dragon> _lifecycleService;
        private readonly IPettingService _pettingService;
        private readonly IFeedingService _feedingService;
        private readonly IContext _context;
        private readonly Dictionary<string, Func<Dragon, Dragon>> _userActions;
        private readonly List<Func<Dragon, Dragon>> _timeActions;

        public GameService(ILifecycleService<Dragon> lifecycleService, IPettingService pettingService,
            IFeedingService feedingService, IContext context)
        {
            _lifecycleService = lifecycleService;
            _pettingService = pettingService;
            _feedingService = feedingService;
            _context = context;

            _userActions = new Dictionary<string, Func<Dragon, Dragon>>
            {
                {"Feed", _feedingService.Perform}, {"Pet", _pettingService.Perform},
                {"F", _feedingService.Perform}, {"P", _pettingService.Perform}
            };

            _timeActions = new List<Func<Dragon, Dragon>>
            {
                _feedingService.Check, _pettingService.Check, _lifecycleService.Age
            };
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            Console.Clear();

            var myDragon = _context.Get<Dragon>();

            if (myDragon.LifeStage == DragonLifeStage.Dead)
            {
                DrawDeath(myDragon);
            }

            DrawDragon(myDragon);

            myDragon = CaptureUserInput(myDragon);

            // Carry out regular actions
            _timeActions.ForEach(o => myDragon = o.Invoke(myDragon));
            
            _context.Update(myDragon);

            return Task.FromResult("");
        }

        private Dragon _performUserAction(string action, Dragon dragon)
        {
            if (_userActions.Any(o => o.Key.Equals(action)))
            {
                var userAction = _userActions
                    .FirstOrDefault(o => o.Key.Equals(action));

                dragon = userAction.Value(dragon);
            }

            return dragon;
        }

        private Dragon CaptureUserInput(Dragon myDragon)
        {
            Utils.WriteAt($"What would you like to do? type (F)eed or (P)et", 0, 15, ConsoleColor.Yellow);
            Console.SetCursorPosition(50, 15);
            var action = Console.ReadLine();

            myDragon = _performUserAction(action, myDragon);

            return myDragon;
        }

        private void DrawDeath(Dragon myDragon)
        {
            Utils.WriteAt($"Unfortunately {myDragon.Name} has died due to old age :-(", 2, 1, ConsoleColor.Red);
            Utils.WriteAt($"Press any key to close", 0, 3, ConsoleColor.Yellow);
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        private void DrawDragon(Dragon myDragon)
        {
            Utils.WriteAt($"This is {myDragon.Name}, he is a {myDragon.LifeStage}", 2, 1);
            Utils.WriteAt("          ____ __", 2, 3, ConsoleColor.Green);
            Utils.WriteAt("         { --.\\  |", 2, 4, ConsoleColor.Green);
            Utils.WriteAt("          '-._\\ | (\\___", 2, 5, ConsoleColor.Green);
            Utils.WriteAt("              `\\|{/ ^ _)", 2, 6, ConsoleColor.Green);
            Utils.WriteAt("          .'^^^^^^^  /`", 2, 7, ConsoleColor.Green);
            Utils.WriteAt("         //\\   ) ,  /", 2, 8, ConsoleColor.Green);
            Utils.WriteAt("   ,  _.'/  `\\<-- \\<", 2, 9, ConsoleColor.Green);
            Utils.WriteAt("    `^^^`     ^^   ^^", 2, 10, ConsoleColor.Green);
            
            string happiness = null, hunger = null;
            var color = ConsoleColor.Blue;
            switch (myDragon.Happiness.SatisfactionLevel)
            {
                case SatisfactionLevel.VeryBad:
                    happiness = "Very unhappy";
                    color = ConsoleColor.Red;
                    break;
                case SatisfactionLevel.Bad:
                    happiness = "Unhappy";
                    color = ConsoleColor.Red;
                    break;
                case SatisfactionLevel.Neutral:
                    happiness = "Okay";
                    color = ConsoleColor.Blue;
                    break;
                case SatisfactionLevel.Good:
                    happiness = "Happy";
                    color = ConsoleColor.Green;
                    break;
                case SatisfactionLevel.VeryGood:
                    happiness = "Very happy";
                    color = ConsoleColor.Green;
                    break;
            }
            Utils.WriteAt($"He is feeling {happiness}", 2, 12, color);
            
            color = ConsoleColor.Blue;
            switch (myDragon.Hunger.SatisfactionLevel)
            {
                case SatisfactionLevel.VeryBad:
                    hunger = "Very hungry";
                    color = ConsoleColor.Red;
                    break;
                case SatisfactionLevel.Bad:
                    hunger = "Hungry";
                    color = ConsoleColor.Red;
                    break;
                case SatisfactionLevel.Neutral:
                    hunger = "Not hungry";
                    color = ConsoleColor.Blue;
                    break;
                case SatisfactionLevel.Good:
                    hunger = "Well fed";
                    color = ConsoleColor.Green;
                    break;
                case SatisfactionLevel.VeryGood:
                    hunger = "Full";
                    color = ConsoleColor.Green;
                    break;
            }
            
            Utils.WriteAt($"And is {hunger}", 2, 13, color);
        }
    }
}