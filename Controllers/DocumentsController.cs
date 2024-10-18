using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_ClaimSystem__CMCS_.Controllers
{
    public class DocumentsController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }
    }
}
