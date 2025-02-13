using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public AttachmentsController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<VendorController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.Attachments.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }

        }


        // GET api/<VendorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _portalContext.Attachments.SingleOrDefault(v => v.Id == id);

                if (result == null)
                {
                    return NotFound($"Attachment with {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // POST api/<VendorController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Attachment attachment)
        {
            try
            {
                var newAttachment = _portalContext.Add(attachment);
                try
                {
                    _portalContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //return BadRequest("Error occured while creating Vendor.");
                    return BadRequest(ModelState);

                }
                

                var newAttachmentId = _portalContext
                    .Attachments
                    .OrderBy(v => v.Id)
                    .Last()
                    .Id;
                return Created("Attachment", newAttachmentId);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // PUT api/<VendorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Attachment attachment)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingAttachment = _portalContext.Attachments.Find(id);

                if (existingAttachment != null)
                {
                    try
                    {
                        existingAttachment.VendorName = attachment.VendorName ?? existingAttachment.VendorName;
                        existingAttachment.DocumentType = attachment.DocumentType ?? existingAttachment.DocumentType;
                        existingAttachment.DocumentPath = attachment.DocumentPath ?? existingAttachment.DocumentPath;
                        _portalContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        BadRequest("Error occured while Updating Attachment");
                    }
                }
                else
                {
                    return NotFound($"Attachment with ID {id} not found");
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
