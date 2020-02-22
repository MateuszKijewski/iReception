using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iReception.App.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    return View("NotFound");
                    break;

                case 500:
                    ViewBag.ErrorMessage = "Sorry, something went wrong!";
                    return View("ServerError");
                    break;


            }

            return View();
        }
    }
}
