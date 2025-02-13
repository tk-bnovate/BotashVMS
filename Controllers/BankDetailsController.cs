using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankDetailsController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public BankDetailsController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<BankDetailsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.BankDetails.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }

        }

        // GET api/<BankDetailsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _portalContext.BankDetails.SingleOrDefault(v => v.ProcessId == id);

                if (result == null)
                {
                    return NotFound($"Bank Detaials with ID {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // POST api/<BankDetailsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] BankDetail BankDetails)
        {
            try
            {
                var newBankDetails = _portalContext.Add(BankDetails);
                try
                {
                    _portalContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //return BadRequest("Error occured while creating BankDetails.");
                    return BadRequest(ModelState);

                }


                var newBankDetailsID = _portalContext
                    .BankDetails
                    .OrderBy(v => v.ProcessId)
                    .Last()
                    .ProcessId;
                return Created("BankDetails", newBankDetailsID);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // PUT api/<BankDetailsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BankDetail BankDetails)
        {
            try
            {


                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingBankDetails = _portalContext.BankDetails.Find(id);

                if (existingBankDetails != null)
                {
                    try
                    {
                        existingBankDetails.VendorName = BankDetails.VendorName;
                        existingBankDetails.AccountHolderName = BankDetails.AccountHolderName;
                        existingBankDetails.BankName = BankDetails.BankName;
                        existingBankDetails.AccountNumber = BankDetails.AccountNumber;
                        existingBankDetails.Currency = BankDetails.Currency;
                        existingBankDetails.BranchCode = BankDetails.BranchCode;
                        existingBankDetails.BankBrachAddress = BankDetails.BankBrachAddress;
                        existingBankDetails.SwiftCode = BankDetails.SwiftCode;
                        _portalContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        BadRequest("Error occured while Updating BankDetails");
                    }
                }
                else
                {
                    return NotFound($"BankDetails with ID {id} not found");
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
