using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        // POST api/<LoginController>
        //we use it
        [HttpPost]
        public IActionResult Post([FromBody] Login receivedUser)
        {
            UserDataService userService = new UserDataService();
            User currentUser = userService.Get(receivedUser.Id);
            if (currentUser == null)
                return StatusCode(404);
            else if (currentUser.Password == receivedUser.Password)
            {
                return StatusCode(204);
            }
            else
            {   
                //400
                return StatusCode(400);
            }
        }
    }
}