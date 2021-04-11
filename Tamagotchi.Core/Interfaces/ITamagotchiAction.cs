namespace Tamagotchi.Core.Interfaces
{
    public interface ITamagotchiAction
    {
        TType Perform<TType>(TType tamagotchi) where TType : Models.Tamagotchi;
        TType Check<TType>(TType tamagotchi) where TType : Models.Tamagotchi;
    }
}