using DirectoryManager.UI.Helper;
using DirectoryManager.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DirectoryManager.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DirectoryApi _directoryApi = new DirectoryApi();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<DirectoryData> directories = new List<DirectoryData>();
            HttpClient client = _directoryApi.Initial();
            HttpResponseMessage res = await client.GetAsync("Directories");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                directories = JsonConvert.DeserializeObject<List<DirectoryData>>(results);
            }
            return View(directories);
        }

        public async Task<IActionResult> Details(string id)
        {
            var directory = new DirectoryData();
            HttpClient client = _directoryApi.Initial();
            HttpResponseMessage res = await client.GetAsync($"Directories/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                directory = JsonConvert.DeserializeObject<DirectoryData>(results);
            }
            return View(directory.ContactList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DirectoryData directory)
        {
            HttpClient client = _directoryApi.Initial();
            var postTask = client.PostAsJsonAsync<DirectoryData>("Directories", directory);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> Delete(string id)
        {
            var directory = new DirectoryData();
            HttpClient client = _directoryApi.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"Directories/{id}");
            return RedirectToAction("Index");
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
