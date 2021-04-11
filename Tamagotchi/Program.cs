using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using Quartz.Spi;
using Tamagotchi.Context.InMemory.Implementations;
using Tamagotchi.Core.Configuration;
using Tamagotchi.Core.Implementations;
using Tamagotchi.Core.Implementations.Dragon;
using Tamagotchi.Core.Interfaces;
using Tamagotchi.Core.Models;
using Tamagotchi.Implementations;

namespace Tamagotchi
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    
                    services.AddHostedService<InitialiseService>();
                    
                    services.Configure<DragonAgeingOptions>(configuration.GetSection("DragonAgeingOptions"));
                    services.Configure<FeedingOptions>(configuration.GetSection("FeedingOptions"));
                    services.Configure<PettingOptions>(configuration.GetSection("PettingOptions"));
                    
                    // Register services
                    services.AddSingleton<GameService>();
                    
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton<IJobFactory, JobFactory>();
                    services.AddSingleton<IContext, InMemoryContext>();
                    
                    services.AddSingleton<IElapsedService, ElapsedService>();
                    services.AddSingleton<IFeedingService, FeedingService>();
                    services.AddSingleton<ILifecycleService<Dragon>, LifecycleService>();
                    services.AddSingleton<IPettingService, PettingService>();
                    services.AddSingleton<ISatisfactionService, SatisfactionService>();
                    services.AddSingleton<ITimeService, TimeService>();

                });
    }
}