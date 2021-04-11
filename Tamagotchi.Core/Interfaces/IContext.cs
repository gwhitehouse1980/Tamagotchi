namespace Tamagotchi.Core.Interfaces
{
    public interface IContext
    {
        TType Get<TType>() where TType : Models.Tamagotchi;
        TType Update<TType>(TType tamagotchi) where TType : Models.Tamagotchi;
    }
}