using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        // POST api/<TransferController>
        [HttpPost]
        public IActionResult Post([FromBody] TransferPost transferData)
        {
            TransferService transferService = new TransferService();

            bool addContact = transferService.Send(transferData.from, transferData.to, transferData.content);

            if (addContact)
            {
                //signalIR things
                return StatusCode(201);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
