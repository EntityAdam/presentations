using GetHashCodes.Tests.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GetHashCodes.Tests
{
    class Card : IEquatable<Card>
    {
        public CardSuit Suit { get; }
        public CardValue Value { get; }

        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public bool Equals(Card other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value && Suit == other.Suit;
        }

        public static bool operator ==(Card cardA, Card cardB) => Equals(cardA, cardB);

        public static bool operator !=(Card cardA, Card cardB) => !Equals(cardA, cardB);

        public override bool Equals(object obj) =>
            Equals(obj is Card);

        public override int GetHashCode() =>
            HashCode.Combine(Value, Suit);
    }

    public class Card_Equality_Right
    {
        [Fact]
        public void CardInequality()
        {
            var card1 = new Card(CardValue.Two, CardSuit.Club);
            var card2 = new Card(CardValue.Two, CardSuit.Spade);
            Assert.NotEqual(card1, card2);
        }

        [Fact]
        public void CardEquality()
        {
            var card1 = new Card(CardValue.Two, CardSuit.Club);
            var card2 = new Card(CardValue.Two, CardSuit.Club);
            Assert.Equal(card1, card2);
        }

        [Fact]
        public void Card_HashCodes_ShouldBe_Equal()
        {
            var card1 = new Card(CardValue.Ace, CardSuit.Diamond);
            var card2 = new Card(CardValue.Ace, CardSuit.Diamond);

            Assert.Equal(card1, card2);
            Assert.Equal(card1.GetHashCode(), card2.GetHashCode());
        }

        [Fact]
        public void Card_Dictionary_ShouldNotContain_DuplicateKeys()
        {
            var card1 = new Card(CardValue.Ace, CardSuit.Diamond);
            var card2 = new Card(CardValue.Ace, CardSuit.Diamond);

            var dictionary = new Dictionary<object, string>();
            dictionary.Add(card1, "");
            Assert.True(dictionary.ContainsKey(card1));

            Assert.Throws<ArgumentException>(() =>
                dictionary.Add(card2, "")
            );

            Assert.Single(dictionary);
        }

        [Fact]
        public void HashTable_Key_MustBe_Unique()
        {
            var card1 = new Card(CardValue.Two, CardSuit.Club);
            var card2 = new Card(CardValue.Two, CardSuit.Club);

            var hash = new Hashtable();
            hash.Add(card1, "");

            Assert.Throws<ArgumentException>(() => hash.Add(card2, ""));
        }

        [Fact]
        public void HashTable_ContainsKey_ShouldBe_True_For_Like_Values()
        {
            var card1 = new Card(CardValue.Two, CardSuit.Club);
            var card2 = new Card(CardValue.Two, CardSuit.Club);

            var hash = new Hashtable();
            hash.Add(card1, "");

            Assert.True(hash.ContainsKey(card1));
            Assert.True(hash.ContainsKey(card2));
        }
    }
}