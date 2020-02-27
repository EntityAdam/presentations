using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace GetHashCodes.Tests
{
    internal struct Pizza
    {
        public Pizza(int slices, int diameter)
        {
            Slices = slices;
            Diameter = diameter;
        }

        public int Slices { get; }
        public int Diameter { get; }
    }

    internal struct FastPizza : IEquatable<FastPizza>
    {
        public FastPizza(int slices, int diameter)
        {
            Slices = slices;
            Diameter = diameter;
        }

        public int Slices { get; }
        public int Diameter { get; }

        public bool Equals(FastPizza other) =>
            Slices == other.Slices && Diameter == other.Diameter;

        public static bool operator ==(FastPizza p1, FastPizza p2) =>
            p1.Equals(p2);

        public static bool operator !=(FastPizza p1, FastPizza p2) =>
            !p1.Equals(p2);

        public override bool Equals(object obj) =>
            obj is FastPizza p && Equals(p);

        public override int GetHashCode() => HashCode.Combine(Slices, Diameter);
    }

    public class ValueTypeIEquatableTests
    {
        private readonly ITestOutputHelper output;

        public ValueTypeIEquatableTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void BaseEquals_DoesCheck_ValueEquality()
        {
            var fp1a = new Pizza(1, 1);
            var fp1b = new Pizza(1, 1);

            Assert.True(fp1a.Equals(fp1b));
            //Assert.True(fp1a == fp1b); //compiler error, there are no default == and != operators!

            var fp2a = new FastPizza(2, 2);

            Assert.False(fp1a.Equals(fp1b));
            //Assert.False(fp1a == fp2a); //compiler error, there are no default == and != operators!
        }

        [Fact]
        public void OverrideEquals_DoesCheck_ValueEquality()
        {
            var fp1a = new FastPizza(1, 1);
            var fp1b = new FastPizza(1, 1);

            Assert.True(fp1a.Equals(fp1b));
            Assert.True(fp1a == fp1b);

            var fp2a = new FastPizza(2, 2);

            Assert.False(fp1a.Equals(fp1b));
            Assert.False(fp1a == fp2a);
        }

        [Fact(Skip = "Benchmark")]
        public void SpeedTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var pizzas = PizzaHelper.GetHeapsOfPizza();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            output.WriteLine($"Making {pizzas.Count} pizzas ");
            output.WriteLine("RunTime " + elapsedTime);
        }

        [Fact(Skip = "Benchmark")]
        public void SpeedTest2()
        {
            var sw = new Stopwatch();
            sw.Start();
            var pizzas = PizzaHelper.GetHeapsOfFastPizza();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            output.WriteLine($"Making {pizzas.Count} pizzas ");
            output.WriteLine("RunTime " + elapsedTime);
        }
    }

    internal static class PizzaHelper
    {
        public static HashSet<Pizza> GetHeapsOfPizza()
        {
            var hash = new HashSet<Pizza>();
            for (var i = 0; i < 50_000; i++)
            {
                hash.Add(new Pizza(i, i));
            }
            return hash;
        }

        public static HashSet<FastPizza> GetHeapsOfFastPizza()
        {
            var hash = new HashSet<FastPizza>();
            for (var i = 0; i < 50_000; i++)
            {
                hash.Add(new FastPizza(i, i));
            }
            return hash;
        }
    }
}