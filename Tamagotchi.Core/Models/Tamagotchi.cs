using System;

namespace Tamagotchi.Core.Models
{
    public abstract record Tamagotchi(
        string Name,
        int Age,
        int Weight,
        DateTime Hatched,
        ActionStatus Hunger,
        ActionStatus Happiness);
}
