using GetHashCodes.Tests.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace GetHashCodes.Tests
{
    [SuppressMessage("Design", "CS0659:Type overrides Object.Equals(object o) but does not override Object.GetHashCode()", Justification = "Supressed for demonstration")]
    public class CardDefaultHashCode : IEquatable<CardDefaultHashCode>

    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }

        public CardDefaultHashCode(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public override bool Equals(object obj) => obj is CardDefaultHashCode c && Equals(c);

        public bool Equals(CardDefaultHashCode other) => Value == other.Value && Suit == other.Suit;
    }

    public class Card_Equality_Wrong_DefaultGetHashCode
    {
        [Fact]
        public void CardInequality()
        {
            var card1 = new CardDefaultHashCode(CardValue.Two, CardSuit.Club);
            var card2 = new CardDefaultHashCode(CardValue.Two, CardSuit.Spade);
            Assert.NotEqual(card1, card2);
        }

        [Fact]
        public void CardEquality()
        {
            var card1 = new CardDefaultHashCode(CardValue.Two, CardSuit.Club);
            var card2 = new CardDefaultHashCode(CardValue.Two, CardSuit.Club);
            Assert.Equal(card1, card2);
        }

        [Fact]
        public void Card_DoesntOverrideGetHashCode_HashCodes_ShouldBe_Equal_Fails()
        {
            var card1 = new CardDefaultHashCode(CardValue.Ace, CardSuit.Diamond);
            var card2 = new CardDefaultHashCode(CardValue.Ace, CardSuit.Diamond);

            Assert.Equal(card1, card2); //the cards are equal
            Assert.Equal(card1.GetHashCode(), card2.GetHashCode()); //but the hash codes are not. Fail.
        }

        [Fact]
        public void Card_DoesntOverrideGetHashCode_Dictionary_ShouldNotContain_DuplicateKeys_Fails()
        {
            var card1 = new CardDefaultHashCode(CardValue.Ace, CardSuit.Diamond);
            var card2 = new CardDefaultHashCode(CardValue.Ace, CardSuit.Diamond);

            var dictionary = new Dictionary<object, string>();
            dictionary.Add(card1, "");
            Assert.True(dictionary.ContainsKey(card1));

            Assert.Throws<ArgumentException>(() =>
                dictionary.Add(card2, "") //this should throw an exception
            );

            Assert.Single(dictionary); //there should only be one item in the dictionary, but there are two.
        }

        [Fact]
        public void HashTable_Key_MustBe_Unique_Fails()
        {
            var card1 = new CardDefaultHashCode(CardValue.Two, CardSuit.Club);
            var card2 = new CardDefaultHashCode(CardValue.Two, CardSuit.Club);

            var hash = new Hashtable();
            hash.Add(card1, "");

            Assert.Throws<ArgumentException>(() => hash.Add(card2, ""));
        }

        [Fact]
        public void HashTable_ContainsKey_ShouldBe_True_For_Like_Values_Fails()
        {
            var card1 = new CardDefaultHashCode(CardValue.Two, CardSuit.Club);
            var card2 = new CardDefaultHashCode(CardValue.Two, CardSuit.Club);

            var hash = new Hashtable();
            hash.Add(card1, "");

            Assert.True(hash.ContainsKey(card1));
            Assert.True(hash.ContainsKey(card2));
        }
    }
}