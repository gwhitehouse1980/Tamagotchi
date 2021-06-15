namespace Tamagotchi.Core.Configuration
{
    public record DragonAgeingOptions
    {
        public int ChildAfter { get; init; }
        public int TeenAfter { get; init; }
        public int AdultAfter { get; init; }
        public int DeadAfter { get; init; }

        public DragonAgeingOptions(int childAfter, int teenAfter, int adultAfter, int deadAfter)
        {
            ChildAfter = childAfter;
            TeenAfter = teenAfter;
            AdultAfter = adultAfter;
            DeadAfter = deadAfter;
        }

        public DragonAgeingOptions() { }
    }
}
