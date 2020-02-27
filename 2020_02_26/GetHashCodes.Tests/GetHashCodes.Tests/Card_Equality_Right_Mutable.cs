using GetHashCodes.Tests.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GetHashCodes.Tests
{
    public class Card_IsMutable_Correct : IEquatable<Card_IsMutable_Correct>
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }

        public Card_IsMutable_Correct(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public bool Equals(Card_IsMutable_Correct other)
        {
            if (other is null) return false; //TODO: POSSIBLE OOPS?!  what if 'this is null' and 'other is null'?
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value && Suit == other.Suit;
        }

        public override bool Equals(object obj) => Equals(obj is Card_IsMutable_Correct);

        public override int GetHashCode() => 1;
    }

    public class Card_Equality_Right_Mutable
    {
        [Fact]
        public void CardInequality()
        {
            var card1 = new Card_IsMutable_Correct(CardValue.Two, CardSuit.Club);
            var card2 = new Card_IsMutable_Correct(CardValue.Two, CardSuit.Spade);
            Assert.NotEqual(card1, card2);
        }

        [Fact]
        public void CardEquality()
        {
            var card1 = new Card_IsMutable_Correct(CardValue.Two, CardSuit.Club);
            var card2 = new Card_IsMutable_Correct(CardValue.Two, CardSuit.Club);
            Assert.Equal(card1, card2);
        }

        [Fact]
        public void Card_HashCodes_ShouldBe_Equal()
        {
            var card1 = new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond);
            var card2 = new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond);

            Assert.Equal(card1, card2);
            Assert.Equal(card1.GetHashCode(), card2.GetHashCode());
        }

        [Fact]
        public void Card_Dictionary_ShouldNotContain_DuplicateKeys()
        {
            var card1 = new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond);
            var card2 = new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond);

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
            var card1 = new Card_IsMutable_Correct(CardValue.Two, CardSuit.Club);
            var card2 = new Card_IsMutable_Correct(CardValue.Two, CardSuit.Club);

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
            var mutableCard = new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond);

            //add the Ace Of Diamonds to the hash table
            var hash = new Hashtable();
            hash.Add(mutableCard, "");

            Assert.Equal(mutableCard, new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond));
            Assert.True(hash.ContainsKey(new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond)));

            //now we mutate the mutableCard from an Ace Of Diamonds to an Ace Of Hearts
            mutableCard.Suit = CardSuit.Heart;

            //the mutated card is equal to an Ace of Hearts
            Assert.Equal(mutableCard, new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Heart));

            //but the Hashtable doesn't contain an ace of hearts
            Assert.True(hash.ContainsKey(new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Heart))); //so this fails
        }

        [Fact]
        public void Card_IsMutable_CausesProblems_OnHashTable()
        {
            var mutableCard = new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond);

            //add the Ace Of Diamonds to the hash table
            var hash = new HashSet<Card_IsMutable_Correct>();
            hash.Add(mutableCard);

            Assert.Equal(mutableCard, new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond));
            Assert.True(hash.Contains(new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond)));

            //now we mutate the mutableCard from an Ace Of Diamonds to an Ace Of Hearts
            mutableCard.Suit = CardSuit.Heart;

            //the mutated card is equal to an Ace of Hearts
            Assert.Equal(mutableCard, new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Heart));

            Assert.True(hash.Contains(new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Heart)));

            var i = hash.GetEnumerator();
            i.MoveNext();
            var cardFromHashTable = i.Current;

            Assert.Equal(cardFromHashTable, new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Heart));

            //now we mutate the mutableCard from an Ace Of Diamonds to an Ace Of Hearts
            mutableCard.Suit = CardSuit.Diamond;
            Assert.False(hash.Contains(new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Heart)));
            Assert.True(hash.Contains(new Card_IsMutable_Correct(CardValue.Ace, CardSuit.Diamond)));
        }
    }
}