using FluentAssertions;
using NUnit.Framework;
using Tamagotchi.Context.InMemory.Implementations;
using Tamagotchi.Core.Models;

namespace Tamagotchi.Context.InMemory.Tests
{
    public class InMemoryContextTests
    {
        [Test]
        public void DoesPersistInMemory()
        {
            var context = new InMemoryContext();

            var tamagotchi = context.Get<Dragon>();
            tamagotchi.Should().BeNull();

            //tamagotchi = new Dragon("Logan")
            //{
            //    Name = "Logan"
            //};

            var contextTamagotchi = context.Update(tamagotchi);
            contextTamagotchi.Should().Be(tamagotchi);

            contextTamagotchi = context.Get<Dragon>();
            contextTamagotchi.Should().Be(tamagotchi);
        }
    }
}
