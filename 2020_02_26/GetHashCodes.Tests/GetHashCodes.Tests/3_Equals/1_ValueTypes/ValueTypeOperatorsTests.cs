using System;
using Xunit;

namespace GetHashCodes.Tests
{
    public class ValueTypeOperatorsTests
    {
        private struct FastPizza1 : IEquatable<FastPizza1>
        {
            public FastPizza1(int slices, int diameter)
            {
                Slices = slices;
                Diameter = diameter;
            }

            public int Slices { get; }
            public int Diameter { get; }

            public bool Equals(FastPizza1 other) =>
                Slices == other.Slices && Diameter == other.Diameter;

            public override int GetHashCode() => HashCode.Combine(Slices, Diameter);

            //comment these two operators to see that there is no default
            //implementation for == and != on a value type
            public static bool operator ==(FastPizza1 p1, FastPizza1 p2) =>
                p1.Equals(p2);
            public static bool operator !=(FastPizza1 p1, FastPizza1 p2) =>
                !p1.Equals(p2);
        }

        [Fact]
        public void ValueType_DoesNotHave_DefaultEqualsAndNotEqualsOverloadOperators()
        {
            var fp1a = new FastPizza1(1, 1);
            var fp1b = new FastPizza1(1, 1);

            Assert.True(fp1a.Equals(fp1b));
            Assert.True(fp1a == fp1b); //compiler error, there are no default == and != operators!

            var fp2a = new FastPizza1(2, 2);

            Assert.False(fp1a.Equals(fp1b));
            Assert.False(fp1a == fp2a); //compiler error, there are no default == and != operators!
        }
    }
}