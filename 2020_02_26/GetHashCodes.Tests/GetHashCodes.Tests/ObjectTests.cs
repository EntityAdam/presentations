using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace GetHashCodes.Tests
{
    public class ObjectTests
    {
        private class Person
        {
            public Person(string name)
            {
                Name = name;
            }

            private string Name { get; }
        }

        [Fact]
        public void EqualsTests()
        {
            Person bob1a = new Person("bob");
            Person bob1b = new Person("bob");

            Assert.False(object.Equals(bob1a, bob1b));
        }


        private class Car

        {
            public Car(string name)
            {
                Name = name;
            }

            public string Name { get; }

            public override bool Equals(object obj)
            {
                if (obj == null || !this.GetType().Equals(obj.GetType()))
                    return false;
                else
                {
                    Car c = (Car)obj;
                    return (this.Name == c.Name);
                }
            }
        }

        [Fact]
        public void EqualsTest2()
        {
            Car kitt1a = new Car("KITT");
            Car kitt1b = new Car("KITT");

            var result = object.Equals(kitt1a, kitt1b);
            Assert.True(result);
        }

        #region IEquatable<T>

        //Before
        //class Vehicle : IEquatable<Vehicle>
        //{
        //    public Vehicle(string name)
        //    {
        //        Name = name;
        //    }

        //    public string Name { get; }

        //    public bool Equals(Vehicle other)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public override bool Equals(object obj)
        //    {
        //        if (obj == null || !this.GetType().Equals(obj.GetType()))
        //            return false;
        //        else
        //        {
        //            Vehicle c = (Vehicle)obj;
        //            return (this.Name == c.Name);
        //        }
        //    }
        //}

        //After
        private class Vehicle : IEquatable<Vehicle>
        {
            public Vehicle(string name, string make, string model)
            {
                Name = name;
                Make = make;
                Model = model;
            }

            public string Name { get; }
            public string Make { get; }
            public string Model { get; }

            public override int GetHashCode() =>
                HashCode.Combine(Name, Make, Model);

            public bool Equals(Vehicle other) =>
                Name == other.Name &&
                Make == other.Make &&
                Model == other.Model;

            public override bool Equals(object obj) =>
                obj is Vehicle c && Equals(c);
        }

        #endregion IEquatable<T>

        private class Card
        { }

        [Fact]
        public void ReferenceEquals_Demo()
        {
            var card1 = new Card();
            var card2 = card1;

            Assert.True(EqualsOperator(card1, card2));
            Assert.True(object.ReferenceEquals(card1, card2));
        }

        private bool EqualsOperator(Card card1, Card card2) => card1 == card2;
    }
}