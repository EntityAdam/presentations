using GetHashCodes.Tests.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GetHashCodes.Tests
{
    public class Card_IsMutable : IEquatable<Card_IsMutable>
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }

        public Card_IsMutable(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public override bool Equals(object obj) => obj is Card_IsMutable c && Equals(c);

        public bool Equals(Card_IsMutable other) => Value == other.Value && Suit == other.Suit;

        public override int GetHashCode() => HashCode.Combine(Value, Suit);
    }

    public class Card_Equality_Wrong_Mutable
    {
        [Fact]
        public void CardInequality()
        {
            var card1 = new Card_IsMutable(CardValue.Two, CardSuit.Club);
            var card2 = new Card_IsMutable(CardValue.Two, CardSuit.Spade);
            Assert.NotEqual(card1, card2);
        }

        [Fact]
        public void CardEquality()
        {
            var card1 = new Card_IsMutable(CardValue.Two, CardSuit.Club);
            var card2 = new Card_IsMutable(CardValue.Two, CardSuit.Club);
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

            hash.ContainsKey(card1);
            hash.ContainsKey(card2);
        }

        [Fact]
        public void Card_IsMutable_CausesProblems()
        {
            //            var AceOfDiamonds = new Card_IsMutable(CardValue.Ace, CardSuit.Diamond);
            //            var AceOfHearts = new Card_IsMutable(CardValue.Ace, CardSuit.Heart);

            var mutableCard = new Card_IsMutable(CardValue.Ace, CardSuit.Diamond);

            //add the Ace Of Diamonds to the hash table
            var hash = new Hashtable();
            hash.Add(mutableCard, "");

            Assert.Equal(mutableCard, new Card_IsMutable(CardValue.Ace, CardSuit.Diamond));
            Assert.True(hash.ContainsKey(new Card_IsMutable(CardValue.Ace, CardSuit.Diamond)));

            //now we mutate the mutableCard from an Ace Of Diamonds to an Ace Of Hearts
            mutableCard.Suit = CardSuit.Heart;

            //the mutated card is equal to an Ace of Hearts
            Assert.Equal(mutableCard, new Card_IsMutable(CardValue.Ace, CardSuit.Heart));

            //but the Hashtable doesn't contain an ace of hearts
            Assert.True(hash.ContainsKey(new Card_IsMutable(CardValue.Ace, CardSuit.Heart))); //so this fails
        }

        [Fact]
        public void Card_IsMutable_CausesProblems_OnHashTable()
        {
            //            var AceOfDiamonds = new Card_IsMutable(CardValue.Ace, CardSuit.Diamond);
            //            var AceOfHearts = new Card_IsMutable(CardValue.Ace, CardSuit.Heart);

            var mutableCard = new Card_IsMutable(CardValue.Ace, CardSuit.Diamond);

            //add the Ace Of Diamonds to the hash table
            var hash = new HashSet<Card_IsMutable>();
            hash.Add(mutableCard);

            Assert.Equal(mutableCard, new Card_IsMutable(CardValue.Ace, CardSuit.Diamond));
            Assert.True(hash.Contains(new Card_IsMutable(CardValue.Ace, CardSuit.Diamond)));

            //now we mutate the mutableCard from an Ace Of Diamonds to an Ace Of Hearts
            mutableCard.Suit = CardSuit.Heart;

            //the mutated card is equal to an Ace of Hearts
            Assert.Equal(mutableCard, new Card_IsMutable(CardValue.Ace, CardSuit.Heart));

            //but the Hashtable doesn't contain an ace of hearts
            Assert.False(hash.Contains(new Card_IsMutable(CardValue.Ace, CardSuit.Heart))); //so this fails
            Assert.False(hash.Contains(new Card_IsMutable(CardValue.Ace, CardSuit.Diamond)));

            var i = hash.GetEnumerator();
            i.MoveNext();
            var cardFromHashTable = i.Current;

            Assert.Equal(cardFromHashTable, new Card_IsMutable(CardValue.Ace, CardSuit.Heart));

            //now we mutate the mutableCard from an Ace Of Diamonds to an Ace Of Hearts
            mutableCard.Suit = CardSuit.Diamond;
            Assert.False(hash.Contains(new Card_IsMutable(CardValue.Ace, CardSuit.Heart))); //so this fails
            Assert.True(hash.Contains(new Card_IsMutable(CardValue.Ace, CardSuit.Diamond)));
        }
    }
}