using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataProvider;
using DataProvider.InMemoryProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ViewComponentDemo.ViewComponents;
using Xunit;

namespace ViewComponentDemo.Tests
{
    public class FruitsBasketViewComponentsTests
    {
        [Fact]
        public async Task FruitsBasketViewComponent_InitialState_ShouldHaveNoneSelected()
        {
            var repo = new FruitRepository();
            var vc = new FruitsBasketViewComponent(repo);

            var result = await vc.InvokeAsync(VcDemoType.RazorPagesPrg) as ViewViewComponentResult;
            var model = result.ViewData.Model as List<Fruit>;

            Assert.Equal(7, model.Count);
            Assert.All(model, f=>Assert.False(f.Selected));
        }

        [Fact]
        public async Task FruitsBasketViewComponent_SelectFruits_ShouldHaveSelected()
        {
            var repo = new FruitRepository();
            var vc = new FruitsBasketViewComponent(repo);

            repo.SelectFruits(new[] { 1, 2, 7 });
            
            var result = await vc.InvokeAsync(VcDemoType.RazorPagesPrg) as ViewViewComponentResult;
            
            var model = result.ViewData.Model as List<Fruit>;

            Assert.Equal(7, model.Count);
            Assert.Collection(model, f => Assert.True(f.Selected),
                                     f => Assert.True(f.Selected),
                                     f => Assert.False(f.Selected),
                                     f => Assert.False(f.Selected),
                                     f => Assert.False(f.Selected),
                                     f => Assert.False(f.Selected),
                                     f => Assert.True(f.Selected)
                );
        }
    }
}
