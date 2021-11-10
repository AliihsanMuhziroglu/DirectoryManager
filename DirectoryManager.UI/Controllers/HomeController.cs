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
            List<DirectoryModel> directories = new List<DirectoryModel>();
            HttpClient client = _directoryApi.Initial();
            HttpResponseMessage res = await client.GetAsync("Directories");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                directories = JsonConvert.DeserializeObject<List<DirectoryModel>>(results);
            }
            return View(directories);
        }

        public async Task<IActionResult> Details(DirectoryModel directoryData)
        {
            var directory = new DirectoryModel();
            HttpClient client = _directoryApi.Initial();
            HttpResponseMessage res = await client.GetAsync($"Directories/{directoryData.UUID}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                directory = JsonConvert.DeserializeObject<DirectoryModel>(results);
            }
            if (directory.ContactList == null)
                directory.ContactList = new List<ContactInfoModel>();
            return View(directory);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DirectoryModel directory)
        {
            HttpClient client = _directoryApi.Initial();
            var postTask = client.PostAsJsonAsync<DirectoryModel>("Directories", directory);
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
            var directory = new DirectoryModel();
            HttpClient client = _directoryApi.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"Directories/{id}");
            return RedirectToAction("Index");
        }

        public IActionResult Edit(DirectoryModel directory)
        {
            return View(directory);
        }

        [HttpPost]
        public IActionResult EditData(DirectoryModel directory)
        {
            HttpClient client = _directoryApi.Initial();
            var postTask = client.PutAsJsonAsync<DirectoryModel>("Directories", directory);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult CreateContactInfo(DirectoryModel directory)
        {
            var createContactInfoModel = new CreateContactInfoModel
            {
                directory = directory,
                contactInfo = new ContactInfoModel()
            };
            return View(createContactInfoModel);
        }


 


        [HttpPost]
        public IActionResult CreateContactInfoData(CreateContactInfoModel data)
        {
            if (data.directory.ContactList == null)
                data.directory.ContactList = new List<ContactInfoModel>();
            data.directory.ContactList.Add(data.contactInfo);
            HttpClient client = _directoryApi.Initial();
            var postTask = client.PutAsJsonAsync<DirectoryModel>("Directories", data.directory);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

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
