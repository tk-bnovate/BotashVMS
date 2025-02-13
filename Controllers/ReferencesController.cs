using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public ReferencesController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<ReferencesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.References.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or reference Site Administrator if error persists");
            }

        }

        // GET api/<ReferencesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID");

            try
            {
                var result = _portalContext.References.SingleOrDefault(v => v.Id == id);

                if (result == null)
                {
                    return NotFound($"Reference with ID {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or reference Site Administrator if error persists");
            }


        }

        // POST api/<ReferencesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Reference reference)
        {
            try
            {
                var newReferences = _portalContext.Add(reference);
                try
                {
                    _portalContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //return BadRequest("Error occured while creating References.");
                    return BadRequest(ModelState);

                }


                var newReferenceID = _portalContext
                    .References
                    .Where(r => r.ProcessId == reference.ProcessId)
                    .OrderBy(v => v.Id)
                    .Last()
                    .Id;
                return Created("References", newReferenceID);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or reference Site Administrator if error persists");
            }


        }

        // PUT api/<ReferencesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Put(int id, [FromBody] Reference reference)
        {
            if (id <= 0) return BadRequest("Invalid reference ID");

            try
            {


                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingReference = _portalContext.References.Find(id);

                if (existingReference != null)
                {
                    try
                    {
                        existingReference.StartDate = reference.StartDate;
                        existingReference.EndDate = reference.EndDate;
                        existingReference.ApproximationValue = reference.ApproximationValue;
                        existingReference.ProjectName = reference.ProjectName;
                        existingReference.ProjectLocation = reference.ProjectLocation;
                        existingReference.GoodsOrServices = reference.GoodsOrServices;
                        existingReference.Referee = reference.Referee;
                        existingReference.ContactDetails = reference.ContactDetails;

                        _portalContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        BadRequest("Error occured while Updating References");
                    }
                }
                else
                {
                    return NotFound($"Reference with ID {id} not found");
                }
                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or reference Site Administrator if error persists");
            }
        }
    }
}
