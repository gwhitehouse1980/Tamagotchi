using System;
using Microsoft.Extensions.Options;
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
            
            return new()
            {
                Name = name,
                Age = 0,
                Weight = 10,
                AgeingOptions = ageingOptions,
                Happiness = new ActionStatus
                {
                    SatisfactionLevel = SatisfactionLevel.Neutral,
                    LastAction = _timeService.GetCurrentTime(),
                    LastChecked = _timeService.GetCurrentTime()
                },
                Hunger = new ActionStatus
                {
                    SatisfactionLevel = SatisfactionLevel.Neutral,
                    LastAction = _timeService.GetCurrentTime(),
                    LastChecked = _timeService.GetCurrentTime()
                },
                Hatched = _timeService.GetCurrentTime(),
                LifeStage = DragonLifeStage.Baby
            };
        }

        public Models.Dragon Age(Models.Dragon tamagotchi)
        {
            // Update dragons age
            tamagotchi.Age = (int)_elapsedService.GetElapsedTime(tamagotchi.Hatched).TotalSeconds;
            
            // Check status
            if (tamagotchi.Age > tamagotchi.AgeingOptions.DeadAfter) tamagotchi.LifeStage = DragonLifeStage.Dead;
            else if (tamagotchi.Age > tamagotchi.AgeingOptions.AdultAfter) tamagotchi.LifeStage = DragonLifeStage.Adult;
            else if (tamagotchi.Age > tamagotchi.AgeingOptions.TeenAfter) tamagotchi.LifeStage = DragonLifeStage.Teen;
            else if (tamagotchi.Age > tamagotchi.AgeingOptions.ChildAfter) tamagotchi.LifeStage = DragonLifeStage.Child;
            else tamagotchi.LifeStage = DragonLifeStage.Baby;

            return tamagotchi;
        }
    }
}