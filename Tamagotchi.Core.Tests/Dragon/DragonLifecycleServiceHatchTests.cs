using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Implementations.Dragon;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Tests.Dragon
{
    public class DragonLifecycleServiceHatchTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CanHatchDragon()
        {
            const string name = "Logan";
            var current = DateTime.Now;

            var myDragon = Common.HatchDragon(name, current);
            
            myDragon.Name.Should().Be(name);
            myDragon.Age.Should().Be(0);
            myDragon.LifeStage.Should().Be(DragonLifeStage.Baby);
            myDragon.Weight.Should().Be(10);
            myDragon.Hatched.Should().Be(current);
            myDragon.Happiness.LastAction.Should().Be(current);
            myDragon.Happiness.SatisfactionLevel.Should().Be(SatisfactionLevel.Neutral);
            myDragon.Hunger.LastAction.Should().Be(current);
            myDragon.Hunger.SatisfactionLevel.Should().Be(SatisfactionLevel.Neutral);
        }
        
        [Test]
        public void HatchingDragonWithNoNameThrowsErrorDragon()
        {
            Func<string, DateTime, DragonAgeingOptions, Models.Dragon> hatcher =  Common.HatchDragon;

            hatcher.Invoking(y => y("", DateTime.Now, null))
                .Should().Throw<ArgumentException>();
        }
    }
}