using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Implementations;
using Tamagotchi.Core.Interfaces;
using Tamagotchi.Core.Models;

namespace Tamagotchi.Core.Tests
{
    public class SatisfactionServiceTests
    {
        [Test]
        public void RepeatedChecksGiveExpectedBehaviour()
        {
            var moodChangeEvery = 10;
            var startFrom = DateTime.Now;
            

            var actionStatus = new ActionStatus
            {
                LastAction = startFrom,
                LastChecked = startFrom,
                SatisfactionLevel = SatisfactionLevel.Neutral
            };
            
            for (var i = 0; i <= 20; i++)
            {
                var compareWith = startFrom.AddSeconds(i);
                var mockTime = new Mock<ITimeService>();
                mockTime.Setup(x => x.GetCurrentTime()).Returns(compareWith);

                var elapsedService = new ElapsedService(mockTime.Object);
                var satisfactionService = new SatisfactionService(elapsedService, mockTime.Object);
                
                actionStatus = satisfactionService.Check(actionStatus, moodChangeEvery);
            }
            
            actionStatus.SatisfactionLevel.Should().Be(SatisfactionLevel.VeryBad);
        }
    }
}