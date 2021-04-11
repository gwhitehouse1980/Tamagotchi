using System;
using Microsoft.Extensions.Options;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Implementations
{
    public class FeedingService : IFeedingService
    {
        private readonly ISatisfactionService _satisfactionService;
        private readonly FeedingOptions _options;

        public FeedingService(ISatisfactionService satisfactionService, 
            IOptions<FeedingOptions> feedingOptions)
        {
            _satisfactionService = satisfactionService;
            _options = feedingOptions.Value;
        }

        public TType Perform<TType>(TType tamagotchi) where TType : Models.Tamagotchi
        {
            tamagotchi.Hunger = _satisfactionService.Perform(tamagotchi.Hunger, _options.HungerChangeEvery);
            return tamagotchi;
        }

        public TType Check<TType>(TType tamagotchi) where TType : Models.Tamagotchi
        {
            tamagotchi.Hunger = _satisfactionService.Check(tamagotchi.Hunger, _options.HungerChangeEvery);
            return tamagotchi;
        }
    }
}