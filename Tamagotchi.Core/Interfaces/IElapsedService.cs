using System;

namespace Tamagotchi.Core.Interfaces
{
    public interface IElapsedService
    {
        TimeSpan GetElapsedTime(DateTime since);
    }
}