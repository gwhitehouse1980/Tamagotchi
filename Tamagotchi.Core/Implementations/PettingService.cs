using System;
using Microsoft.Extensions.Options;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Implementations
{
    public class PettingService : IPettingService
    {
        private readonly ISatisfactionService _satisfactionService;
        private readonly PettingOptions _options;

        public PettingService(ISatisfactionService satisfactionService, 
            IOptions<PettingOptions> pettingOptions)
        {
            _satisfactionService = satisfactionService;
            _options = pettingOptions.Value;
        }

        public TType Perform<TType>(TType tamagotchi) where TType : Models.Tamagotchi
        {
            tamagotchi.Happiness = _satisfactionService.Perform(tamagotchi.Happiness, _options.MoodChangeEvery);
            return tamagotchi;
        }

        public TType Check<TType>(TType tamagotchi) where TType : Models.Tamagotchi
        {
            tamagotchi.Happiness = _satisfactionService.Check(tamagotchi.Happiness, _options.MoodChangeEvery);
            return tamagotchi;
        }
    }
}