using Xunit;

namespace GetHashCodes.Tests
{
    public class GetTypeTests
    {
        private class ClassA
        {
            public string GetBase() => this.GetType().BaseType.FullName;
        }

        private class ClassB : System.Object
        {
            public string GetBase() => this.GetType().BaseType.FullName;
        }

        [Fact]
        public void Objects_DeriveFrom_SystemObject_Implicitly()
        {
            ClassA a = new ClassA();
            ClassB b = new ClassB();

            Assert.Equal("System.Object", a.GetBase());
            Assert.Equal("System.Object", b.GetBase());
        }
    }
}