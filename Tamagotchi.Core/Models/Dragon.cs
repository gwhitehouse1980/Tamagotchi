using System;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Implementations.Dragon;

namespace Tamagotchi.Core.Models
{
    public record Dragon(
        DragonLifeStage LifeStage,
        DragonAgeingOptions AgeingOptions,
        string Name,
        int Age,
        int Weight,
        DateTime Hatched,
        ActionStatus Hunger,
        ActionStatus Happiness
        ) : Tamagotchi(Name, Age, Weight, Hatched, Hunger, Happiness);
}
