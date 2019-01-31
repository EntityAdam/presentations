using System;
using System.Threading.Tasks;
using DataProvider;
using Microsoft.AspNetCore.Mvc;

namespace ViewComponentDemo.ViewComponents
{
    public class FruitsBasketViewComponent : ViewComponent
    {
        private readonly IFruitRepository _repo;

        public FruitsBasketViewComponent(IFruitRepository repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(VcDemoType vcDemoType)
        {
            var fruits = _repo.GetFruits();

            switch (vcDemoType)
            {
                case VcDemoType.RazorPages:
                    return await Task.FromResult<IViewComponentResult>(View("RazorPages", fruits));
                case VcDemoType.RazorPagesPrg:
                    return await Task.FromResult<IViewComponentResult>(View("RazorPagesPrg", fruits));
                case VcDemoType.Api:
                    return await Task.FromResult<IViewComponentResult>(View("Api", fruits));
                case VcDemoType.ApiAjax:
                    return await Task.FromResult<IViewComponentResult>(View("ApiAjax", fruits));
                default:
                    return await Task.FromResult<IViewComponentResult>(View(fruits));
            }
        }
    }
}
