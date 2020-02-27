using GetHashCodes.Tests.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace GetHashCodes.Tests
{
    public class Card_GetHashCodeThrowsException : IEquatable<Card_GetHashCodeThrowsException>
    {
        public CardSuit Suit { get; }
        public CardValue Value { get; }

        public Card_GetHashCodeThrowsException(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public override bool Equals(object obj) => obj is Card_GetHashCodeThrowsException c && Equals(c);

        public bool Equals(Card_GetHashCodeThrowsException other) => Value == other.Value && Suit == other.Suit;

        [SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "Supressed for demonstration")]
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    public class Card_Equality_Wrong_ThrowException
    {
        [Fact]
        public void CardInequality()
        {
            var card1 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Club);
            var card2 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Spade);
            Assert.NotEqual(card1, card2);
        }

        [Fact]
        public void CardEquality()
        {
            var card1 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Club);
            var card2 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Club);
            Assert.Equal(card1, card2);
        }

        [Fact]
        public void Card_HashCodes_ShouldBe_Equal_Fails()
        {
            var card1 = new Card_GetHashCodeThrowsException(CardValue.Ace, CardSuit.Diamond);
            var card2 = new Card_GetHashCodeThrowsException(CardValue.Ace, CardSuit.Diamond);
            Assert.Equal(card1.GetHashCode(), card2.GetHashCode()); //ArgumentException
        }

        [Fact]
        public void Card_Dictionary_ShouldNotContain_DuplicateKeys()
        {
            var card1 = new Card_GetHashCodeThrowsException(CardValue.Ace, CardSuit.Diamond);
            var card2 = new Card_GetHashCodeThrowsException(CardValue.Ace, CardSuit.Diamond);

            var dictionary = new Dictionary<Card_GetHashCodeThrowsException, string>();
            dictionary.Add(card1, "");
            Assert.True(dictionary.ContainsKey(card1));

            Assert.Throws<ArgumentException>(() =>
                dictionary.Add(card2, "")
            );

            Assert.Single(dictionary);
        }

        [Fact]
        public void HashTable_Key_MustBe_Unique_Fails()
        {
            var card1 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Club);
            var card2 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Club);

            //we can't add to a hashtable, because GetHashCode() throws NotImplementedException
            var hash = new Hashtable();
            hash.Add(card1, "");  //fails: throws NotImplementedException
            Assert.Throws<ArgumentException>(() => hash.Add(card2, "")); //fails: throws NotImplementedException
        }

        [Fact]
        public void HashTable_ContainsKey_ShouldBe_True_For_Like_Values_Fails()
        {
            var card1 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Club);
            var card2 = new Card_GetHashCodeThrowsException(CardValue.Two, CardSuit.Club);

            //we can't add to a hashtable, because GetHashCode() throws NotImplementedException
            var hash = new Hashtable();
            hash.Add(card1, ""); //fails: throws NotImplementedException

            Assert.True(hash.ContainsKey(card1)); //fails: false
            Assert.True(hash.ContainsKey(card2)); //fails: false
        }
    }
}