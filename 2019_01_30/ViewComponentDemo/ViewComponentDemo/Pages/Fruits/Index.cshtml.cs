using DataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViewComponentDemo.Pages.Fruits
{
    public class IndexModel : PageModel
    {
        private readonly IFruitRepository _repo;

        public IndexModel(IFruitRepository repo)
        {
            _repo = repo;

        }
        public IActionResult OnGet()
        {
            return new JsonResult(_repo.GetFruits());
        }

        public IActionResult OnGetSelected()
        {
            return new JsonResult(_repo.GetSelectedFruits());
        }

        public void OnPost([FromForm] int[] selected)
        {
            _repo.SelectFruits(selected);
        }
    }
}