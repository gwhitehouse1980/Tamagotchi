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
    [TestFixture(30, 15, SatisfactionLevel.VeryGood, SatisfactionLevel.VeryGood)]
    [TestFixture(30, 30, SatisfactionLevel.VeryGood, SatisfactionLevel.Good)]
    [TestFixture(30, 45, SatisfactionLevel.VeryGood, SatisfactionLevel.Good)]
    [TestFixture(30, 60, SatisfactionLevel.VeryGood, SatisfactionLevel.Neutral)]
    [TestFixture(30, 75, SatisfactionLevel.VeryGood, SatisfactionLevel.Neutral)]
    [TestFixture(30, 90, SatisfactionLevel.VeryGood, SatisfactionLevel.Bad)]
    [TestFixture(30, 105, SatisfactionLevel.VeryGood, SatisfactionLevel.Bad)]
    [TestFixture(30, 120, SatisfactionLevel.VeryGood, SatisfactionLevel.VeryBad)]
    public class StatelessSatisfactionServiceDecreaseTests
    {
        private readonly int _moodChangeEvery;
        private readonly int _secondsSinceLastInteraction;
        private readonly SatisfactionLevel _startLevel;
        private readonly SatisfactionLevel _expectedLevel;

        public StatelessSatisfactionServiceDecreaseTests(int moodChangeEvery, int secondsSinceLastInteraction, 
            SatisfactionLevel startLevel, SatisfactionLevel expectedLevel)
        {
            _moodChangeEvery = moodChangeEvery;
            _secondsSinceLastInteraction = secondsSinceLastInteraction;
            _startLevel = startLevel;
            _expectedLevel = expectedLevel;
        }
        
        [Test]
        public void MoodChangesAsExpected()
        {
            var startFrom = DateTime.Now;
            var compareWith = startFrom.AddSeconds(_secondsSinceLastInteraction);
            var mockTime = new Mock<ITimeService>();
            mockTime.Setup(x => x.GetCurrentTime()).Returns(compareWith);

            var elapsedService = new ElapsedService(mockTime.Object);
            var satisfactionService = new SatisfactionService(elapsedService, mockTime.Object);

            var actionStatus = new ActionStatus
            {
                LastAction = startFrom,
                LastChecked = startFrom,
                SatisfactionLevel = _startLevel
            };
            
            actionStatus = satisfactionService.Check(actionStatus, _moodChangeEvery);
            actionStatus.SatisfactionLevel.Should().Be(_expectedLevel);

            // Check again, expect no change
            actionStatus.SatisfactionLevel.Should().Be(_expectedLevel);
        }
    }
}