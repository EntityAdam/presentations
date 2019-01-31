using System.Collections.Generic;
using System.Linq;

namespace DataProvider.InMemoryProvider
{
    public class FruitRepository : IFruitRepository
    {
        private List<Fruit> _fruits;
        public FruitRepository()
        {
            _fruits = new List<Fruit>();
            SeedMe();
        }
        public List<Fruit> GetFruits() => _fruits;
        public Fruit GetFruitById(int id) => _fruits.Single(x => x.Id == id);
        public List<Fruit> GetSelectedFruits() => _fruits.Where(x => x.Selected).ToList();
        public void SelectFruits(int[] ids)
        {
            Reset();
            foreach (var id in ids)
            {
                var fruit = GetFruitById(id);
                fruit.Selected = true;
            }
        }
        private void Reset()
        {
            foreach (var fruit in _fruits)
            {
                fruit.Selected = false;
            }
        }
        private void SeedMe()
        {
            if (!_fruits.Any())
            {
                _fruits = new List<Fruit>()
                {
                    new Fruit() {Id = 1, Name = "Apple", Selected = false},
                    new Fruit() {Id = 2, Name = "Banana", Selected = false},
                    new Fruit() {Id = 3, Name = "Cherry", Selected = false},
                    new Fruit() {Id = 4, Name = "Durian", Selected = false},
                    new Fruit() {Id = 5, Name = "Elderberry", Selected = false},
                    new Fruit() {Id = 6, Name = "Fig", Selected = false},
                    new Fruit() {Id = 7, Name = "Grape", Selected = false},
                };
            }
        }
    }
}
