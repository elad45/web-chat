﻿using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.Request;
using System.Web;

// return Ok(Whatever) - means status code 200
// return StatusCode(201,WHATEVER I WANT TO RETURN)
namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IUserDataService service;
        private IConversationService conversationService;
        public ContactsController()
        {
            service = new UserDataService();
            conversationService = new ConversationService();
        }
        // GET: api/<ContactsController>
        //have to be checked
        [HttpGet]
        // returns all the contacts of the current user
        public IActionResult Get(string user)
        {
            User u = service.Get(user);
            if (u == null)
                return NotFound();
            return Ok(u.ContactsList);
        }

        [Route("allContacts")]
        [HttpGet]
        //we use it
        // returns all the contacts of the current user
        public IActionResult GetMessagesTimeAgo(string user)
        {
            User u = service.Get(user);
            if (u == null)
                return NotFound();
            List<ContactFixed> contacts = new List<ContactFixed>();
            foreach (Contact contact in u.ContactsList)
            {
                ContactFixed newContact = new ContactFixed(contact.Id, contact.Name, contact.Server);
                newContact.Last = contact.Last;
                newContact.Lastdate = TimeAgo(contact.Lastdate);
                contacts.Add(newContact);

            }
            return Ok(contacts);
        }

        // POST api/<ContactsController>
        [HttpPost]
        //we use it
        // creates a new contact for the current user
        public IActionResult Post([FromBody] AddContactPost request)
        {
            User user = service.Get(request.User);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = new Contact(request.Id, request.Name, request.Server);
            user.AddContact(c);
            //trying to add conv ------ have to be tested ----
            Conversation conv = new Conversation(conversationService.nextConvId(), request.Id, request.User);
            conversationService.Add(conv);
            //
            return StatusCode(201);
        }
        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        // returns data about contact id = {id}
        public IActionResult Get(string id, string user)
        {

            User u = service.Get(user);
            if (u == null)
                return NotFound();
            Contact c = u.ContactsList.Find(x => x.Id == id);
            if (c == null)
                return NotFound();
            return Ok(c);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        // updates data about contact id = {id}
        public IActionResult Put(string id, [FromBody] EditContactPut request)
        {
            User user = service.Get(request.UserId);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = user.ContactsList.Find(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            c.Server = request.Server;
            c.Name = request.Name;
            return StatusCode(204);
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        // deletes data about contact id = {id}
        public IActionResult Delete(string id, string userId)
        {
            User user = service.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = user.ContactsList.Find(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            user.ContactsList.Remove(c);
            return StatusCode(204);
        }

        //we use it
        [HttpGet("{id}/messages")]
        // returns all the messages received/sent by the current logged user
        public IActionResult GetMessages(string id, string user)
        {
            User u = service.Get(user);
            if (u == null)
            {
                return NotFound();
            }
            Contact c = u.ContactsList.Find(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }

            List<MessageGet> messagesConverted = conversationService.GetMessagesConverted(id,user);
            return Ok(messagesConverted);
        }

        //we use it
        [HttpPost("{id}/messages")]
        // creates a new message between the contact and the logged user
        public IActionResult AddMessage(string id, [FromBody] AddMessage msg)
        {

            //User user = service.Get(msg.UserId);
            User user = service.Get(msg.UserId);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = user.ContactsList.Find(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }

            conversationService.AddMessage(id, msg.Content, msg.UserId);
            c.Last = msg.Content;
            c.Lastdate = DateTime.Now;
            return StatusCode(201);
        }

        [HttpPost("{id}/messagesSecondSide")]
        // creates a new message between the contact and the logged user
        public IActionResult updateLastMsg2(string id, [FromBody] AddMessage msg)
        {
            User user = service.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = user.ContactsList.Find(x => x.Id == msg.UserId);
            if (c == null)
            {
                return NotFound();
            }

            c.Last = msg.Content;
            c.Lastdate = DateTime.Now;
            return StatusCode(201);
        }

        [HttpGet("{id}/messages/{id2}")]
        //check if works
        //id is contactId and id2 is msgID
        // returns a message of ID = {id2}, of the contact that has id = {id}
        public IActionResult GetMsgById(string id, string id2, string user)
        {
            User u = service.Get(user);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = u.ContactsList.Find(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            MessageGet msgConverted = conversationService.GetMsgByIdConverted(id, id2, user);
            if (msgConverted == null)
                return NotFound();
            return Ok(msgConverted);
        }

        [HttpPut("{id}/messages/{id2}")]
        // edits a message of ID = {id2}, of the contact that has id = {id}
        public IActionResult PutMsgById(string id, string id2, [FromBody] PutMsgById msg)
        {
            User user = service.Get(msg.UserId);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = user.ContactsList.Find(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            Message msgToChange = conversationService.GetMsgById(id, id2,msg.UserId);
            if (msg == null)
                return NotFound();
            msgToChange.Content = msg.Content;
            return StatusCode(204);
        }

        [HttpDelete("{id}/messages/{id2}")]
        // deletes a message of ID = {id2}, of the contact that has id = {id}
        public IActionResult DeleteMsgById(string id, string id2,string userId)
        {
            User user = service.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            Contact c = user.ContactsList.Find(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            conversationService.DeleteMsgById(id, id2, userId);
            return StatusCode(204);
        }
        private static string TimeAgo(DateTime? time)
        {
            string result = string.Empty;
            if (time == null)
                return null;
            var timeSpan = DateTime.Now.Subtract((DateTime)time);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0}s ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = String.Format("{0}m ago", timeSpan.Minutes);
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = String.Format("{0}h ago", timeSpan.Hours);
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = String.Format("{0}d ago", timeSpan.Days);
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = String.Format("{0}m ago", timeSpan.Days / 30);
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("about {0}y ago", timeSpan.Days / 365) :
                    "about a year ago";
            }
            return result;
        }
    }
}