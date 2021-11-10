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

        [HttpGet("{id}",Name ="GetBook")]
        public IActionResult GetDirectory(string id)
        {
            return Ok(_directoryServices.GetDirectory(id));
        }
        

        [HttpPost]
        public IActionResult AddDirectory(Directory directory)
        {
            _directoryServices.AddDirectory(directory);
            return CreatedAtRoute("GetDirectory", new { id = directory.UUID }, directory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDirectory(string id)
        {
            _directoryServices.DeleteDirectory(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateDirectory(Directory directory)
        {
            return Ok(_directoryServices.UpdateDirectory(directory));
        }
 
    }
}
