using Tamagotchi.Core.Models;

namespace Tamagotchi.Core.Interfaces
{
    public interface ISatisfactionService
    {
        ActionStatus Perform(ActionStatus actionStatus, int changeEvery);
        ActionStatus Check(ActionStatus actionStatus, int changeEvery);
    }
}