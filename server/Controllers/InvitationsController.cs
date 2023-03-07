﻿using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {

        // POST api/<InvitationsController>
        [HttpPost]
        public IActionResult Post([FromBody] InvitePost invitation)
        {
            InvitationService invitationService = new InvitationService();

            bool addContact = invitationService.Invite(invitation.from, invitation.to, invitation.server);

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
