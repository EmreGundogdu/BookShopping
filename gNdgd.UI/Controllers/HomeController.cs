using gNdgd.UI.Models;
using gNdgd.UI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace gNdgd.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IHomeRepository homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            this.homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index(string sterm="",int genreId = 0)
        {
            var books =await homeRepository.DisplayBooks(sterm, genreId);
            var genres =await homeRepository.Genres();
            BookDisplayModel bookModel = new()
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };
            return View(bookModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}