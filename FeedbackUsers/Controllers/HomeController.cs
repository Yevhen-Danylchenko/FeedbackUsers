using FeedbackUsers.Models;
using FeedbackUsers.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FeedbackUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly AzureTableService _tableService;

        public HomeController(AzureTableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var feedbacks = await _tableService.GetAllFeedbackAsync();
            return View(feedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FeedbackModel model)
        {
            if (ModelState.IsValid)
            {
                await _tableService.AddFeedbackAsync(model);
                return RedirectToAction("Index");
            }
            var feedbacks = await _tableService.GetAllFeedbackAsync();
            return View(feedbacks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new FeedbackModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackModel model)
        {
            if (ModelState.IsValid)
            {
                await _tableService.AddFeedbackAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
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
