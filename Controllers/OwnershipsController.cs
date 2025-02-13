using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnershipsController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public OwnershipsController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<OwnershipsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.Ownerships.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or ownership Site Administrator if error persists");
            }

        }

        // GET api/<OwnershipsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _portalContext.Ownerships.SingleOrDefault(v => v.Id == id);

                if (result == null)
                {
                    return NotFound($"Ownership with ID {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or ownership Site Administrator if error persists");
            }


        }

        // POST api/<OwnershipsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Ownership ownership)
        {
            try
            {
                var newOwnerships = _portalContext.Add(ownership);
                try
                {
                    _portalContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //return BadRequest("Error occured while creating Ownerships.");
                    return BadRequest(ModelState);

                }


                var newOwnershipID = _portalContext
                    .Ownerships
                    .OrderBy(v => v.Id)
                    .Last()
                    .Id;
                return Created("Ownerships", newOwnershipID);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or ownership Site Administrator if error persists");
            }


        }

        // PUT api/<OwnershipsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ownership ownership)
        {
            try
            {


                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingOwnership = _portalContext.Ownerships.Find(id);

                if (existingOwnership != null)
                {
                    try
                    {
                        existingOwnership.VendorName = ownership.VendorName;
                        existingOwnership.Name = ownership.Name;
                        existingOwnership.Sex = ownership.Sex;
                        existingOwnership.Nationality = ownership.Nationality;
                        existingOwnership.Idnumber = ownership.Idnumber;
                        existingOwnership.Position = ownership.Position;
                        existingOwnership.NumberOfShares = ownership.NumberOfShares;
                        existingOwnership.SharePercentage = ownership.SharePercentage;
                        existingOwnership.OwnershipAttachement = ownership.OwnershipAttachement;
                        existingOwnership.OwnershipType = ownership.OwnershipType;
                        existingOwnership.Country = ownership.Country;

                        _portalContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        BadRequest("Error occured while Updating Ownerships");
                    }
                }
                else
                {
                    return NotFound($"Ownership with ID {id} not found");
                }
                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or ownership Site Administrator if error persists");
            }
        }
    }
}
