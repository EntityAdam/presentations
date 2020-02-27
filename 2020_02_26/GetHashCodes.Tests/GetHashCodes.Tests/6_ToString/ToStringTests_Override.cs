using Xunit;

namespace GetHashCodes.Tests
{
    public class ToStringTests_Override
    {
        private class Address
        {
            public Address(string street, string city, string state, string zip)
            {
                this.Street = street;
                this.City = city;
                this.State = state;
                this.Zip = zip;
            }

            public string Street { get; }
            public string City { get; }
            public string State { get; }
            public string Zip { get; }

            public override string ToString() =>
                $"{Street}\n{City}, {State} {Zip}";
        }

        [Fact]
        public void ToString_Override_CanDoFriendlyDisplay()
        {
            var generalElectricAddress = new Address("1 River Rd", "Schenectady", "NY", "12345");
            var result = generalElectricAddress.ToString();
            Assert.Equal("1 River Rd\nSchenectady, NY 12345", result);
        }
    }
}