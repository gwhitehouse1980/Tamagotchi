using System;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Implementations
{
    public class ElapsedService : IElapsedService
    {
        private readonly ITimeService _timeService;

        public ElapsedService(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        public TimeSpan GetElapsedTime(DateTime since)
        {
            var current = _timeService.GetCurrentTime();
            
            return current - since;
        }
    }
}