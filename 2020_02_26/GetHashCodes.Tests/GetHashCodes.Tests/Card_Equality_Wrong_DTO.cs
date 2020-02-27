using GetHashCodes.Tests.Models;
using Xunit;

namespace GetHashCodes.Tests
{
    public class Card_DTO
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
    }

    public class Card_Equality_Wrong_DTO
    {
        [Fact]
        public void CardInequality()
        {
            var card1 = new Card_DTO() { Value = CardValue.Two, Suit = CardSuit.Club };
            var card2 = new Card_DTO() { Value = CardValue.Two, Suit = CardSuit.Spade };
            Assert.NotEqual(card1, card2);
        }

        [Fact]
        public void CardEquality_Fails()
        {
            var card1 = new Card_DTO() { Value = CardValue.Two, Suit = CardSuit.Club };
            var card2 = new Card_DTO() { Value = CardValue.Two, Suit = CardSuit.Club };
            Assert.Equal(card1, card2);
        }

        [Fact]
        public void Card_HashCodes_ShouldBe_Equal_Fails()
        {
            var card1 = new Card_DTO() { Value = CardValue.Ace, Suit = CardSuit.Diamond };
            var card2 = new Card_DTO() { Value = CardValue.Ace, Suit = CardSuit.Diamond };

            Assert.Equal(card1, card2);
            Assert.Equal(card1.GetHashCode(), card2.GetHashCode());
        }
    }
}