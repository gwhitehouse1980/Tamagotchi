using Tamagotchi.Core.Enums;
using Tamagotchi.Core.Helpers;
using Tamagotchi.Core.Interfaces;
using Tamagotchi.Core.Models;

namespace Tamagotchi.Core.Implementations
{
    public class SatisfactionService : ISatisfactionService
    {
        private readonly IElapsedService _elapsedService;
        private readonly ITimeService _timeService;

        public SatisfactionService(IElapsedService elapsedService, ITimeService timeService)
        {
            _elapsedService = elapsedService;
            _timeService = timeService;
        }
        
        public ActionStatus Perform(ActionStatus actionStatus, int changeEvery)
        {
            // Check first so we have the correct status
            actionStatus = Check(actionStatus, changeEvery);
            
            // Debate here about how many times to pet before we go up a status
            //  for argument sake - let's say 1
            var current = (int)actionStatus.SatisfactionLevel;
            var maxValue = (int)Utils.MaxEnumValue<SatisfactionLevel>();
            current += 1;
            
            if (current > maxValue)
                current = maxValue;

            // Set our values
            actionStatus.SatisfactionLevel = (SatisfactionLevel) current;
            actionStatus.LastAction = _timeService.GetCurrentTime();
            
            return actionStatus;
        }

        public ActionStatus Check(ActionStatus actionStatus, int changeEvery)
        {
            var elapsedTime = _elapsedService.GetElapsedTime(actionStatus.LastAction);

            // Calculate how many changes in mood have happened since last petting time
            var seconds = (int)elapsedTime.TotalSeconds;

            // divide the seconds by the change every to get the number of changes
            var changes = seconds / changeEvery;
            
            // Get int value for the enum of lastchecked status
            var lastStatus = (int)actionStatus.SatisfactionLevel;
            var minSatisfactionLevel = (int)Utils.MinEnumValue<SatisfactionLevel>();

            // Caluculate the status
            if (changes != 0)
            {
                lastStatus -= changes;

                if (lastStatus < minSatisfactionLevel)
                {
                    lastStatus = minSatisfactionLevel;
                }    
                
                actionStatus.SatisfactionLevel = (SatisfactionLevel)lastStatus;
                actionStatus.LastAction = _timeService.GetCurrentTime();
            }

            actionStatus.LastChecked = _timeService.GetCurrentTime();
            
            return actionStatus;
        }
    }
}