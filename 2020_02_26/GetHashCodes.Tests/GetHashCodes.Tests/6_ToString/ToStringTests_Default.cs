using System;
using Xunit;

namespace GetHashCodes.Tests
{
    public class ToStringTests_Default
    {
        [Fact]
        public void BaseToString_IsTheSameAs_GetType_ToString()
        {
            var obj = new object();
            Assert.Equal(obj.GetType().ToString(), obj.ToString());

            //nameof does return a string representation of an object
            //but it is not fully qualified!
            Assert.NotEqual(nameof(Object), obj.ToString());
        }
    }
}