using System.Linq;
using DataProvider.InMemoryProvider;
using Xunit;

namespace DataProvider.Tests
{
    public class InMemoryProviderTests
    {
        [Fact]
        public void GetFruits_NewInstance_HasFourFruits()
        {
            var repo = new FruitRepository();
            var fruits = repo.GetFruits();
            Assert.Equal(4, fruits.Count);
            
            var anySelected = fruits.Any(x => x.Selected);
            Assert.False(anySelected);
        }

        [Fact]
        public void GetFruits_NewInstance_HasNoSelectedFruits()
        {
            var repo = new FruitRepository();
            var fruits = repo.GetFruits();

            var anySelected = fruits.Any(x => x.Selected);
            Assert.False(anySelected);

            var selected = repo.GetSelectedFruits();
            Assert.Empty(selected);
        }

        [Fact]
        public void SelectFruits_FirstFruitSelected_ShouldBeSelected()
        {
            var repo = new FruitRepository();

            var ids = new[] { 1 };
            repo.SelectFruits(ids);

            var firstFruit = repo.GetFruitById(ids.First());
            Assert.True(firstFruit.Selected);
        }

        [Fact]
        public void SelectFruits_FirstFruitSelected_NoOtherFruitShouldBeSelected()
        {
            var repo = new FruitRepository();

            var ids = new[] { 1 };
            repo.SelectFruits(ids);
            var notSelectedFruits = repo.GetFruits().Where(x => x.Selected == false);

            Assert.Equal(3, notSelectedFruits.Count());
        }
    }
}
