using Tamagotchi.Core.Interfaces;

namespace Tamagotchi.Context.InMemory.Implementations
{
    /// <summary>
    /// The purpose of this class is to provide a quick and easy
    /// use of the context object where we are okay with a single
    /// instance of a Tamagotchi that is non-persisted per execution
    /// </summary>
    public class InMemoryContext : IContext
    {
        private static dynamic _current = null;

        public TType Get<TType>() where TType : Core.Models.Tamagotchi
        {
            return _current;
        }

        public TType Update<TType>(TType tamagotchi) where TType : Core.Models.Tamagotchi
        {
            _current = tamagotchi;
            return Get<TType>();
        }
    }
}