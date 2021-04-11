using System;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Implementations.Dragon;

namespace Tamagotchi.Core.Models
{
    public record Dragon : Tamagotchi
    {
        public DragonLifeStage LifeStage { get; set; }
        public DragonAgeingOptions AgeingOptions { get; set; }
    }
}