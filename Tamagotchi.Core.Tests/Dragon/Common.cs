using Moq;
using System;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Implementations.Dragon;
using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Core.Tests.Dragon
{
    public static class Common
    {
        public static Models.Dragon HatchDragon(string name, DateTime current, DragonAgeingOptions ageingOptions = null)
        {
            var ageOptions = ageingOptions ?? new DragonAgeingOptions(30, 60, 90, 120);

            var mockTime = new Mock<ITimeService>();
            mockTime.Setup(x => x.GetCurrentTime()).Returns(current);
            var mockElapsed = new Mock<IElapsedService>();

            var lifecycleService = new LifecycleService(mockElapsed.Object, mockTime.Object);

            // Hatch the Dragon
            return lifecycleService.Hatch(name, ageOptions);
        }
    }
}
