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


        public DirectoriesController()
        { 
        }

        [HttpGet]
        public IActionResult GetDirectories()
        {

        }

 
    }
}
