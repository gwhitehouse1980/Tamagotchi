using System;
using System.Linq;

namespace Tamagotchi.Core.Helpers
{
    public static class Utils
    {
        public static TType? MaxEnumValue<TType>()
        {
            return Enum.GetValues(typeof(TType)).Cast<TType>().Max();
        }
        
        public static TType? MinEnumValue<TType>()
        {
            return Enum.GetValues(typeof(TType)).Cast<TType>().Min();
        }
    }
}