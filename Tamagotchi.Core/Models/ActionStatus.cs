using System;
using Tamagotchi.Core.Enums;

namespace Tamagotchi.Core.Models
{
    public record ActionStatus
    {
        public DateTime LastAction { get; set; }
        public SatisfactionLevel SatisfactionLevel { get; set; }
        public DateTime LastChecked { get; set; }
    }
}