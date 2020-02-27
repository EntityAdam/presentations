using Xunit;

namespace GetHashCodes.Tests
{
    public class MemberwiseCloneTests
    {
        private class Building
        {
            public Building(string street, int year)
            {
                Street = street;
                Year = year;
            }

            public string Street { get; }
            public int Year { get; }

            public Building ShallowCopy() =>
                (Building)this.MemberwiseClone();
        }

        [Fact]
        public void ShallowCopy_ValueType_CopiesValue()
        {
            var bldg1 = new Building("Moe Rd", 1950);
            var bldg2 = bldg1.ShallowCopy();

            Assert.True(object.Equals(bldg1.Year, bldg2.Year));
            Assert.False(object.ReferenceEquals(bldg1.Year, bldg2.Year));
        }

        [Fact]
        public void ShallowCopy_ReferenceType_CopiesReference()
        {
            var bldg1 = new Building("Moe Rd", 1950);
            var bldg2 = bldg1.ShallowCopy();

            Assert.True(object.Equals(bldg1.Street, bldg2.Street));
            Assert.True(object.ReferenceEquals(bldg1.Street, bldg2.Street));
        }
    }
}