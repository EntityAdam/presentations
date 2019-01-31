using System.Collections.Generic;

namespace DataProvider
{
    public interface IFruitRepository
    {
        List<Fruit> GetFruits();
        List<Fruit> GetSelectedFruits();
        void SelectFruits(int[] ids);
    }
}