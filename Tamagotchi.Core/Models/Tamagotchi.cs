using System;
using Tamagotchi.Core.Enums;

namespace Tamagotchi.Core.Models
{
    public abstract record Tamagotchi
    {
        public string Name { get; init; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public DateTime Hatched { get; set; }
        public ActionStatus Hunger { get; set; }
        public ActionStatus Happiness { get; set; }
    }
}