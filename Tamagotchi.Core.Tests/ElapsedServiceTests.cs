using System;
using System.Runtime.InteropServices.ComTypes;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Tamagotchi.Core.Implementations;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Tests
{
    [TestFixture(30)]
    [TestFixture(60)]
    [TestFixture(90)]
    public class ElapsedServiceTests
    {
        private readonly int _compareTime;

        public ElapsedServiceTests(int compareTime)
        {
            _compareTime = compareTime;
        }
        
        [Test]
        public void DoesCalculateCorrectDifference()
        {
            var date1 = DateTime.Now;
            var date2 = date1.AddSeconds(_compareTime);
            
            var mockTime = new Mock<ITimeService>();
            mockTime.Setup(x => x.GetCurrentTime()).Returns(date2);
            
            var elapseService = new ElapsedService(mockTime.Object);

            var difference = elapseService.GetElapsedTime(date1);

            ((int) difference.TotalSeconds).Should().Be(_compareTime);
        } 
    }
}