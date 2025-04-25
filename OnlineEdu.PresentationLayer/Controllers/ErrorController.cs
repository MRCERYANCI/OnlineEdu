using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class ErrorController : Controller
    {

        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult Error_Page(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public IActionResult BadRequestPage()
        {
            return View();
        }

        public IActionResult InternalServerError()
        {
            return View();
        }

        //[Route("Error/{statusCode}")]
        //public IActionResult HttpStatusCodeHandler(int statusCode)
        //{
        //    if (statusCode == 400)
        //    {
        //        return View("BadRequestPage"); // 400 hatası için özel sayfa
        //    }
        //    else if (statusCode == 500)
        //    {
        //        return View("InternalServerError"); // 400 hatası için özel sayfa
        //    }
        //    else if(statusCode == 404)
        //    {
        //        return View("NotFound"); // 400 hatası için özel sayfa
        //    }
        //    return View("GenericErrorPage"); // Diğer hatalar için genel hata sayfası
        //}
    }
}
