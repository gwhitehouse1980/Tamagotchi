using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Enums;

namespace Tamagotchi.Core.Interfaces
{
    public interface ILifecycleService<TType> where TType:Models.Tamagotchi
    {
        TType Hatch(string name, DragonAgeingOptions ageingOptions);
        TType Age(TType tamagotchi);
    }
}