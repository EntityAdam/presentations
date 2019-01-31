using DataProvider;
using DataProvider.InMemoryProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViewComponentDemo.Pages
{
    public class RazorPageVcDemoModel : PageModel
    {
        private IFruitRepository _repo;

        public RazorPageVcDemoModel(IFruitRepository repo)
        {
            _repo = repo;
        }
        public void OnGet()
        {

        }

        public void OnPost([FromForm] int[] selected)
        {
            _repo.SelectFruits(selected);
        }
    }
}