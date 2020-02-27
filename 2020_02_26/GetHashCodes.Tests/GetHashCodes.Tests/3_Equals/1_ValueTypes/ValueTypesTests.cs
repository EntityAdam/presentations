using Xunit;

namespace GetHashCodes.Tests
{
    public class ValueTypeTests
    {
        [Fact]
        public void ValueTypes_False_ForDifferentTypes()
        {
            byte value1 = 12;
            int value2 = 12;

            Assert.False(object.Equals(value1, value2));
        }

        private struct Point
        { }

        [Fact]
        public void ValueTypes_True_BothInstancesNull()
        {
            Point? p1 = null;
            Point? p2 = null;
            Assert.True(object.Equals(p1, p2));

            //Same as
            Assert.True(p1.Equals(p2));
        }

        private struct Employee
        {
            private string name;

            public Employee(string name)
            {
                this.name = name;
            }
        }

        private struct Manager
        {
            private string name;

            public Manager(string name)
            {
                this.name = name;
            }
        }

        [Fact]
        public void ValueTypeStruct()
        {
            Employee emp1 = new Employee("bob");
            Employee emp2 = new Employee("bob");
            Assert.True(object.Equals(emp1, emp2));
            Assert.False(object.ReferenceEquals(emp1, emp2));

            Employee emp3 = new Employee("joe");
            Assert.False(object.Equals(emp1, emp3));

            Manager mng1 = new Manager("bob");
            Assert.False(object.Equals(emp2, mng1));
        }
    }
}