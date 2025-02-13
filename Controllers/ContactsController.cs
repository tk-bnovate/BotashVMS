using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private readonly BotashPortalContext _portalContext;

        public ContactsController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.Contacts.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }

        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _portalContext.Contacts.SingleOrDefault(v => v.Id == id);

                if (result == null)
                {
                    return NotFound($"Contact with ID {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // POST api/<ContactsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Contact contact)
        {
            try
            {
                var newContacts = _portalContext.Add(contact);
                try
                {
                    _portalContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //return BadRequest("Error occured while creating Contacts.");
                    return BadRequest(ModelState);

                }


                var newContactID = _portalContext
                    .Contacts
                    .OrderBy(v => v.Id)
                    .Last()
                    .Id;
                return Created("Contacts", newContactID);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Contact contact)
        {
            try
            {


                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingContact = _portalContext.Contacts.Find(id);

                if (existingContact != null)
                {
                    try
                    {
                        existingContact.CompanyName = contact.CompanyName;
                        existingContact.SalesContactName = contact.SalesContactName;
                        existingContact.SalesTelephoneNumber = contact.SalesTelephoneNumber;
                        existingContact.SalesCellphoneNumber = contact.SalesCellphoneNumber;
                        existingContact.SalesEmailAddress = contact.SalesEmailAddress;
                        existingContact.WebAddress = contact.WebAddress;
                        existingContact.FinanceContactName = contact.FinanceContactName;
                        existingContact.FinanceTelephoneNumber = contact.FinanceTelephoneNumber;
                        existingContact.FinanceCellphoneNumber = contact.FinanceCellphoneNumber;
                        existingContact.FinanceEmailAddress = contact.FinanceEmailAddress;


                        _portalContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        BadRequest("Error occured while Updating Contacts");
                    }
                }
                else
                {
                    return NotFound($"Contact with ID {id} not found");
                }
                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }
        }
    }
}
