using System;
using Tamagotchi.Core.Helpers;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Implementations
{
    public class TimeService : ITimeService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now.Flatten();
        }
    }
}