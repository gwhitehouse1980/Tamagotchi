using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Spi;
using Quartz.Util;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Interfaces;
using Tamagotchi.Core.Models;

namespace Tamagotchi.Implementations
{
    public class InitialiseService : BackgroundService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly ILifecycleService<Dragon> _lifecycleService;
        private readonly IContext _context;
        private readonly DragonAgeingOptions _options;
        private IScheduler Scheduler { get; set; }
        
        public InitialiseService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory,
            ILifecycleService<Dragon> lifecycleService, IContext context, IOptions<DragonAgeingOptions> options)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _lifecycleService = lifecycleService;
            _context = context;
            _options = options.Value;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.FromResult("s");
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var myDragon = _captureName();
            _context.Update(myDragon);

            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            var gameService = JobBuilder.Create(typeof(GameService)).Build();
            var gameTrigger = TriggerBuilder.Create().WithCronSchedule("0/1 * * * * ?").Build();

            await Scheduler.ScheduleJob(gameService, gameTrigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult("s");
        }

        private void _writeIntroMessage()
        {
            Console.Clear();
            Console.WriteLine("**************************************************************");
            Console.WriteLine("*****                                                    *****");
            Console.WriteLine("*****     Hello, and welcome to your new Tamagotchi!     *****");
            Console.WriteLine("*****                                                    *****");
            Console.WriteLine("*****     We need to know what you want to call your     *****");
            Console.WriteLine("*****     new Dragon!                                    *****");
            Console.WriteLine("*****                                                    *****");
            Console.WriteLine("**************************************************************");
        }

        private Dragon _captureName()
        {
            var dftForeColor = Console.ForegroundColor;
            Dragon myDragon = null;
            string errorMessage = null;
            
            while (myDragon == null)
            {
                _writeIntroMessage();

                if (errorMessage != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(errorMessage);
                    Console.ForegroundColor = dftForeColor;
                }
                
                Console.WriteLine("Type you Dragons name, and press enter when done:");
                var dragonsName = Console.ReadLine();

                try
                {
                    myDragon = _lifecycleService.Hatch(dragonsName, _options);
                }
                catch (ArgumentException ae)
                {
                    if (ae.ParamName is "name")
                    {
                        errorMessage = "We need a name before we can continue";
                    }
                }
            }

            return myDragon;
        }
    }
}