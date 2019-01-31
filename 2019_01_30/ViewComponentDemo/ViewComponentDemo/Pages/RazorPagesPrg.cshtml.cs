using DataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViewComponentDemo.Pages
{
    public class RazorPagesPrgModel : PageModel
    {
        private readonly IFruitRepository _repo;

        public RazorPagesPrgModel(IFruitRepository repo)
        {
            _repo = repo;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost([FromForm] int[] selected)
        {
            if (ModelState.IsValid)
            {
                _repo.SelectFruits(selected);
                return RedirectToPage("RazorPagesPrg");
            }
            else
            {
                return Page();
            }
        }
    }
}