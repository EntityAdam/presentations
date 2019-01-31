using Microsoft.AspNetCore.Mvc;

namespace ViewComponentDemo.ViewComponents
{
    public class HelloWorld : ViewComponent
    {
        public IViewComponentResult Invoke(string message)
        {
            return View(null, message);
            //return new ViewViewComponentResult() {ViewName = "Default"};
            //return new ContentViewComponentResult("Hello Content!");
            //return new HtmlContentViewComponentResult(new StringHtmlContent("Hello HTML Content!"));
        }
    }
}
