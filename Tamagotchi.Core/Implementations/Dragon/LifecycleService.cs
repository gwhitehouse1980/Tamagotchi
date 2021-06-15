using System;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Interfaces;
using Tamagotchi.Core.Models;

namespace Tamagotchi.Core.Implementations.Dragon
{
    public class LifecycleService : ILifecycleService<Models.Dragon>
    {
        private readonly IElapsedService _elapsedService;
        private readonly ITimeService _timeService;

        public LifecycleService(IElapsedService elapsedService, ITimeService timeService)
        {
            _elapsedService = elapsedService;
            _timeService = timeService;
        }

        public Models.Dragon Hatch(string name, DragonAgeingOptions ageingOptions)
        {
            // Only input here is name
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Parameter name must contain a value", nameof(name));
            }

            return new Models.Dragon(
                DragonLifeStage.Baby,
                ageingOptions,
                name,
                0,
                10,
                 _timeService.GetCurrentTime(),
                  new ActionStatus
                  {
                      SatisfactionLevel = SatisfactionLevel.Neutral,
                      LastAction = _timeService.GetCurrentTime(),
                      LastChecked = _timeService.GetCurrentTime()
                  },
               new ActionStatus
               {
                   SatisfactionLevel = SatisfactionLevel.Neutral,
                   LastAction = _timeService.GetCurrentTime(),
                   LastChecked = _timeService.GetCurrentTime()
               });
        }

        public Models.Dragon Age(Models.Dragon tamagotchi)
        {
            return tamagotchi with
            {
                Age = (int)_elapsedService.GetElapsedTime(tamagotchi.Hatched).TotalSeconds,
                LifeStage = Update(tamagotchi.Age, tamagotchi.AgeingOptions)
            };
        }

        private DragonLifeStage Update(int age, DragonAgeingOptions ageingOptions)
        {
            if (age > ageingOptions.DeadAfter)
                return DragonLifeStage.Dead;
            else if (age > ageingOptions.AdultAfter)
                return DragonLifeStage.Adult;
            else if (age > ageingOptions.TeenAfter)
                return DragonLifeStage.Teen;
            else if (age > ageingOptions.ChildAfter)
                return DragonLifeStage.Child;
            else
                return DragonLifeStage.Baby;
        }
    }
}
