using AddressBook_API.Interfaces;
using AddressBook_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookService bookService;

        public AddressBookController(IAddressBookService bookService)
        {
            this.bookService = bookService;
            var x = true;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> GetContacts()
        {
            var serviceResponse = await this.bookService.GetContacts();
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }

        [HttpGet("contacts/count")]
        public async Task<ActionResult<int>> GetContactsCount()
        {
            var count = await this.bookService.GetContactsCount();
            return Ok(count);
        }

        [HttpGet("search/{page}/{contactsPerPage}")]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> GetContacts(int page, int contactsPerPage)
        {
            var serviceResponse = await this.bookService.GetContacts(page, contactsPerPage);
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }

        [HttpGet("/firstName/{firstName}")]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> SearchByFirstName(string firstName)
        {
            var serviceResponse = await this.bookService.SearchByFirstName(firstName);
            return Ok(serviceResponse);
        }

        [HttpGet("/lastName/{lastName}")]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> SearchBylastName(string lastName)
        {
            var serviceResponse = await this.bookService.SearchByLastName(lastName);
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }

        [HttpGet("/address/{adddress}")]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> SearchByAddress(string address)
        {
            var serviceResponse = await this.bookService.SearchByAddress(address);
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }

        [HttpGet("/phoneNumber/{phoneNumber}")]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> SearchByPhoneNumber(string phoneNumber)
        {
            var serviceResponse = await this.bookService.SearchByPhoneNumber(phoneNumber);
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<Contact>>> Create(Contact contact)
        {
            var serviceResponse = await this.bookService.Create(contact);
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }

        [HttpPut()]
        public async Task<ActionResult<ServiceResponse<Contact>>> Update(Contact contact)
        {
            var serviceResponse = await this.bookService.Update(contact);
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<Contact>>> Delete(int id)
        {
            var serviceResponse = await this.bookService.Delete(id);
            if (serviceResponse.Successfull)
                return Ok(serviceResponse);

            return BadRequest(serviceResponse);
        }



    }
}
