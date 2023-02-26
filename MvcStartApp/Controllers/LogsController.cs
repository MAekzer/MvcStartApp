using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Middleware.Repositories;
using MvcStartApp.Models.db;

namespace MvcStartApp.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogRepository repo;

        public LogsController(ILogRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var logs = await repo.GetRequestLogs();
            return View(logs);
        }
    }
}
