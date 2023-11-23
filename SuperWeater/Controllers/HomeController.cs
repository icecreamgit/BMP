using Microsoft.AspNetCore.Mvc;
using SuperWeather.Contracts;
using SuperWeather.Models;
using System.Diagnostics;

namespace SuperWeather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public async Task Index()
        {
            string content = @"<form method='post'>
                <label>Name:</label><br />
                <input name='name' /><br />
                <input type='submit' value='Send' />
            </form>";
            Response.ContentType = "text/html;charset=utf-8";
            await Response.WriteAsync(content);
        }


        [HttpPost]
        public string Index(string name)
        {
            string apiKey = "23e5ff7e3ef7ea4bd4b7875fea2fb326";
            CreateRequestGetResponse createRequestGetResponse = new CreateRequestGetResponse(apiKey);

            var result = createRequestGetResponse.API(name);

            return result;
        }
        
    }
}