using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ViewComponentDemo.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; private set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }
    }
}
