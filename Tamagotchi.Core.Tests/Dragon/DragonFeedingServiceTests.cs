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
    public class DragonFeedingServiceTests
    {
        private IOptions<FeedingOptions> _someOptions;

        [SetUp]
        public void Setup()
        {
            _someOptions = Options.Create(new FeedingOptions()
            {
                HungerChangeEvery = 30
            });
        }
        
        [Test]
        public void PettingDragonDoesUpdateHappiness()
        {
            var startFrom = DateTime.Now;
            var compareWith = startFrom.AddSeconds(_someOptions.Value.HungerChangeEvery);
            var newStatus = new ActionStatus()
            {
                LastAction = compareWith,
                LastChecked = compareWith,
                SatisfactionLevel = SatisfactionLevel.VeryGood
            };
            
            var myDragon = Common.HatchDragon("Logan", DateTime.Now);

            myDragon.Hunger.Should().NotBe(newStatus);

            var mockSatisfactionService = new Mock<ISatisfactionService>();
            mockSatisfactionService
                .Setup(x => x.Perform(myDragon.Hunger, _someOptions.Value.HungerChangeEvery))
                .Returns(newStatus);
            
            var feedingService = new FeedingService(mockSatisfactionService.Object, _someOptions);

            myDragon = feedingService.Perform(myDragon);

            myDragon.Hunger.Should().Be(newStatus);
        }
        
        [Test]
        public void CheckingDragonDoesUpdateHappiness()
        {
            var startFrom = DateTime.Now;
            var compareWith = startFrom.AddSeconds(_someOptions.Value.HungerChangeEvery);
            var newStatus = new ActionStatus()
            {
                LastAction = compareWith,
                LastChecked = compareWith,
                SatisfactionLevel = SatisfactionLevel.VeryGood
            };
            
            var myDragon = Common.HatchDragon("Logan", DateTime.Now);

            myDragon.Hunger.Should().NotBe(newStatus);

            var mockSatisfactionService = new Mock<ISatisfactionService>();
            mockSatisfactionService
                .Setup(x => x.Check(myDragon.Hunger, _someOptions.Value.HungerChangeEvery))
                .Returns(newStatus);
            
            var feedingService = new FeedingService(mockSatisfactionService.Object, _someOptions);

            myDragon = feedingService.Check(myDragon);

            myDragon.Hunger.Should().Be(newStatus);
        }
    }
}