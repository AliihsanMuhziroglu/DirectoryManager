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

        public async Task<IActionResult> Details(DirectoryModel directoryModel)
        {
            var directory = await GetDirectoryByUUID(directoryModel.UUID);
            return View(directory);
        }

        public async Task<DirectoryModel> GetDirectoryByUUID(string uuid)
        {
            var directory = new DirectoryModel();
            HttpClient client = _directoryApi.Initial();
            HttpResponseMessage res = await client.GetAsync($"Directories/{uuid}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                directory = JsonConvert.DeserializeObject<DirectoryModel>(results);
            }
            if (directory.ContactList == null)
                directory.ContactList = new List<ContactInfoModel>();
            return directory;
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


        public async Task<IActionResult> CreateContactInfo(DirectoryModel directory)
        {
            var dir = await GetDirectoryByUUID(directory.UUID);
            var createContactInfoModel = new CreateContactInfoModel
            {
                directory = dir,
                contactInfo = new ContactInfoModel()
            };
            return View(createContactInfoModel);
        }


 


        [HttpPost]
        public async Task<IActionResult> CreateContactInfoData(CreateContactInfoModel data)
        {
            var dir = await GetDirectoryByUUID(data.directory.UUID);
            if (dir.ContactList == null)
                dir.ContactList = new List<ContactInfoModel>();
            data.contactInfo.ContactId = Guid.NewGuid().ToString();
            dir.ContactList.Add(data.contactInfo);
            HttpClient client = _directoryApi.Initial();
            var postTask = client.PutAsJsonAsync<DirectoryModel>("Directories", dir);
            postTask.Wait();

            var result = postTask.Result;
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
