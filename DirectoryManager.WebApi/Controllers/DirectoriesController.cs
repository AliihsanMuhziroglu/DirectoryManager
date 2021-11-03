using DirectoryManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectoriesController : ControllerBase
    {

        private readonly IDirectoryServices _directoryServices;
        public DirectoriesController(IDirectoryServices directoryServices)
        {
            _directoryServices = directoryServices;
        }

        [HttpGet]
        public IActionResult GetDirectories()
        {
            return Ok(_directoryServices.GetDirectories());
        }

 
    }
}
