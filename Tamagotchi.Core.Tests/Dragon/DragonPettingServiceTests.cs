using System;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Implementations;
using Tamagotchi.Core.Interfaces;
using Tamagotchi.Core.Models;

namespace Tamagotchi.Core.Tests.Dragon
{
    public class DragonPettingServiceTests
    {
        private IOptions<PettingOptions> _someOptions;

        [SetUp]
        public void Setup()
        {
            _someOptions = Options.Create(new PettingOptions()
            {
                MoodChangeEvery = 30
            });
        }
        
        [Test]
        public void PettingDragonDoesUpdateHappiness()
        {
            var startFrom = DateTime.Now;
            var compareWith = startFrom.AddSeconds(_someOptions.Value.MoodChangeEvery);
            var newStatus = new ActionStatus()
            {
                LastAction = compareWith,
                LastChecked = compareWith,
                SatisfactionLevel = SatisfactionLevel.VeryGood
            };
            
            var myDragon = Common.HatchDragon("Logan", DateTime.Now);

            myDragon.Happiness.Should().NotBe(newStatus);

            var mockSatisfactionService = new Mock<ISatisfactionService>();
            mockSatisfactionService
                .Setup(x => x.Perform(myDragon.Happiness, _someOptions.Value.MoodChangeEvery))
                .Returns(newStatus);
            
            var petService = new PettingService(mockSatisfactionService.Object, _someOptions);

            myDragon = petService.Perform(myDragon);

            myDragon.Happiness.Should().Be(newStatus);
        }
        
        [Test]
        public void CheckingDragonDoesUpdateHappiness()
        {
            var startFrom = DateTime.Now;
            var compareWith = startFrom.AddSeconds(_someOptions.Value.MoodChangeEvery);
            var newStatus = new ActionStatus()
            {
                LastAction = compareWith,
                LastChecked = compareWith,
                SatisfactionLevel = SatisfactionLevel.VeryGood
            };
            
            var myDragon = Common.HatchDragon("Logan", DateTime.Now);

            myDragon.Happiness.Should().NotBe(newStatus);

            var mockSatisfactionService = new Mock<ISatisfactionService>();
            mockSatisfactionService
                .Setup(x => x.Check(myDragon.Happiness, _someOptions.Value.MoodChangeEvery))
                .Returns(newStatus);
            
            var petService = new PettingService(mockSatisfactionService.Object, _someOptions);

            myDragon = petService.Check(myDragon);

            myDragon.Happiness.Should().Be(newStatus);
        }
    }
}