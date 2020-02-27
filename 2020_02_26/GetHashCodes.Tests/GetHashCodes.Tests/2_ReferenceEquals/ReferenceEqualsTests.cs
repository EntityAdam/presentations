using Xunit;

namespace GetHashCodes.Tests
{
    public class ReferenceEqualsTests
    {
        private class Animal
        { }

        [Fact]
        public void SameInstance_ChecksNothingElse()
        {
            Animal dog1 = new Animal();
            Animal dog2 = dog1;

            Assert.True(object.ReferenceEquals(dog1, dog2));
        }

        [Fact]
        public void NotSameInstance_ChecksNulls_OneSideNull_ReturnsFalse()
        {
            Animal dog1 = new Animal();
            Animal dog2 = null;

            Assert.False(object.ReferenceEquals(dog1, dog2));
        }

        [Fact]
        public void NotSameInstance_ChecksNulls_BothSidesNull_ReturnsTrue()
        {
            Animal dog1 = null;
            Animal dog2 = null;

            Assert.True(object.ReferenceEquals(dog1, dog2));

            //Same as
            Assert.True(object.ReferenceEquals(null, null));
        }
    }
}