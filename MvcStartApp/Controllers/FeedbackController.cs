using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Middleware.Repositories;
using MvcStartApp.Models;
using MvcStartApp.Models.db;
using System.Diagnostics;

namespace MvcStartApp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IBlogRepository _repo;

        public FeedbackController(IBlogRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Feedback feedback)
        {
            await _repo.AddFeedback(feedback);

            return StatusCode(200, $"{feedback.From}, спасибо за отзыв!");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
