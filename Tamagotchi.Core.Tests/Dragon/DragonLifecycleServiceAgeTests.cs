using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Implementations;
using Tamagotchi.Core.Implementations.Dragon;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Tests.Dragon
{
    [TestFixture(10, 20, 30, 40, 10, DragonLifeStage.Baby)]
    [TestFixture(10, 20, 30, 40, 11, DragonLifeStage.Child)]
    [TestFixture(10, 20, 30, 40, 20, DragonLifeStage.Child)]
    [TestFixture(10, 20, 30, 40, 21, DragonLifeStage.Teen)]
    [TestFixture(10, 20, 30, 40, 30, DragonLifeStage.Teen)]
    [TestFixture(10, 20, 30, 40, 31, DragonLifeStage.Adult)]
    [TestFixture(10, 20, 30, 40, 40, DragonLifeStage.Adult)]
    [TestFixture(10, 20, 30, 40, 41, DragonLifeStage.Dead)]
    public class DragonLifecycleServiceAgeTests
    {
        private readonly int _childAfter;
        private readonly int _teenAfter;
        private readonly int _adultAfter;
        private readonly int _deadAfter;
        private readonly int _elapsedTime;
        private readonly DragonLifeStage _expectedStage;

        public DragonLifecycleServiceAgeTests(int childAfter, int teenAfter, int adultAfter, int deadAfter, 
            int elapsedTime, DragonLifeStage expectedStage)
        {
            _childAfter = childAfter;
            _teenAfter = teenAfter;
            _adultAfter = adultAfter;
            _deadAfter = deadAfter;
            _elapsedTime = elapsedTime;
            _expectedStage = expectedStage;
        }

        [Test]
        public void DragonAgesCorrectly()
        {
            const string name = "Logan";
            var current = DateTime.Now;
            var compareWith = current.AddSeconds(_elapsedTime);

            var myDragon = Common.HatchDragon(name, current, new DragonAgeingOptions()
            {
                AdultAfter = _adultAfter,
                ChildAfter = _childAfter,
                DeadAfter = _deadAfter,
                TeenAfter = _teenAfter
            });
            
            var mockTime = new Mock<ITimeService>();
            mockTime.Setup(x => x.GetCurrentTime()).Returns(compareWith);
            var elapsedService = new ElapsedService(mockTime.Object);
            
            var lifecycleService = new LifecycleService(elapsedService, mockTime.Object);

            myDragon = lifecycleService.Age(myDragon);

            myDragon.LifeStage.Should().Be(_expectedStage);
        }
    }
}