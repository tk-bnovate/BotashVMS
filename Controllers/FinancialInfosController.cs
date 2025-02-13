using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialInfosController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public FinancialInfosController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<FinancialInfosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.FinancialInfos.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or financialInfo Site Administrator if error persists");
            }

        }

        // GET api/<FinancialInfosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _portalContext.FinancialInfos.SingleOrDefault(v => v.Id == id);

                if (result == null)
                {
                    return NotFound($"Financial Info with ID {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or financialInfo Site Administrator if error persists");
            }


        }

        // POST api/<FinancialInfosController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] FinancialInfo financialInfo)
        {
            try
            {
                var newFinancialInfos = _portalContext.Add(financialInfo);
                try
                {
                    _portalContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //return BadRequest("Error occured while creating FinancialInfos.");
                    return BadRequest(ModelState);

                }


                var newFinancialInfoID = _portalContext
                    .FinancialInfos
                    .OrderBy(v => v.Id)
                    .Last()
                    .Id;
                return Created("FinancialInfos", newFinancialInfoID);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or financialInfo Site Administrator if error persists");
            }


        }

        // PUT api/<FinancialInfosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FinancialInfo financialInfo)
        {
            try
            {


                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingFinancialInfo = _portalContext.FinancialInfos.Find(id);

                if (existingFinancialInfo != null)
                {
                    try
                    {
                        existingFinancialInfo.VendorName = financialInfo.VendorName;
                        existingFinancialInfo.AnnualTurnOver = financialInfo.AnnualTurnOver;
                        existingFinancialInfo.NumberOfEmployees = financialInfo.NumberOfEmployees;
                        existingFinancialInfo.TotalLabour = financialInfo.TotalLabour;
                        existingFinancialInfo.UnskilledLabour = financialInfo.UnskilledLabour;
                        existingFinancialInfo.SkilledLabour = financialInfo.SkilledLabour;
                        existingFinancialInfo.Managers = financialInfo.Managers;
                        existingFinancialInfo.Directors = financialInfo.Directors;
                        existingFinancialInfo.NatureOfBusiness = financialInfo.NatureOfBusiness;
                        existingFinancialInfo.NatureOfProducts = financialInfo.NatureOfProducts;
                        existingFinancialInfo.Financials = financialInfo.Financials;
                        existingFinancialInfo.SettlementTerms = financialInfo.SettlementTerms;
                        existingFinancialInfo.OffsetAccounts = financialInfo.OffsetAccounts;


                        _portalContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        BadRequest("Error occured while Updating Financial Information");
                    }
                }
                else
                {
                    return NotFound($"Financial Info with ID {id} not found");
                }
                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or financialInfo Site Administrator if error persists");
            }
        }
    }
}
