using BotashVMS.Models;
using BotashVMS.Models.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public VendorController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<VendorController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.Vendors.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }

        }

        // GET api/<VendorController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            //Check if valid id is entered
            if(id <= 0) return BadRequest("Invalid ID");

            try
            {
                var result = _portalContext.Vendors.SingleOrDefault(v => v.ProcessId == id);

                if (result == null)
                {
                    return NotFound($"Supplier with VendorID {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        [HttpGet("by-erp/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBySysproID(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("ERP-ID cannot be null or empty");

            try
            {
                var result = _portalContext
                    .Vendors
                    .SingleOrDefault(v => v.SysproId == id);

                if (result == null)
                {
                    return NotFound($"Vendor with ERP-ID: {id} does not exist");
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
        public IActionResult Post([FromBody] Vendor vendor)
        {
            try
            {

                //Validate User Input
                var validator = new VendorValidator();
                var validationResult = validator.Validate(vendor);

                if (!validationResult.IsValid)
                {
                    var response = new
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        FormData = vendor
                    };

                    return BadRequest(response);
                }

                if (!string.IsNullOrEmpty(vendor.SysproId))
                {
                    var duplicateSysproId = _portalContext
                        .Vendors
                        .SingleOrDefault(v => v.SysproId == vendor.SysproId);

                    if(duplicateSysproId != null) {
                        return BadRequest($"Vendor with SysproID {vendor.SysproId} Already Exists");
                    }
                } 

                //Trim all the User Inputs
                vendor.CompanyName?.Trim();
                vendor.TradingAs?.Trim();
                vendor.CompanyType?.Trim();
                vendor.RegistrationNumber?.Trim();
                vendor.VatNumber?.Trim();
                vendor.PhysicalAddress?.Trim();
                vendor.District?.Trim();
                vendor.Town?.Trim();
                vendor.PostalAddress?.Trim();
                vendor.ContactNumber?.Trim();
                vendor.FaxNumber?.Trim();
                vendor.SeparateHqaddress?.Trim();
                vendor.ParentCompanyAddress?.Trim();
                vendor.ContractType?.Trim();
                vendor.SysproId?.Trim();

                //Proceed to add vendor to Db
                var newVendor = _portalContext.Add(vendor);
                _portalContext.SaveChanges();
                

                var newVendorID = _portalContext
                    .Vendors
                    .Where(v => v.CompanyName == vendor.CompanyName)
                    .OrderBy(v => v.ProcessId)
                    .Last()
                    .ProcessId;

                return Created("Vendor", newVendorID);
            }
            catch (Exception e)
            {
                //return StatusCode(500, e);
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        

        // PUT api/<VendorController>/5
        [HttpPut("{id}")]
        
        public IActionResult Put(int id, [FromBody] Vendor vendor)
        {
            if (id <= 0) return BadRequest("Invalid VendorId");

            try
            {

                //Trim user Input
                vendor.CompanyName?.Trim();
                vendor.TradingAs?.Trim();
                vendor.CompanyType?.Trim();
                vendor.RegistrationNumber?.Trim();
                vendor.VatNumber?.Trim();
                vendor.PhysicalAddress?.Trim();
                vendor.District?.Trim();
                vendor.Town?.Trim();
                vendor.PostalAddress?.Trim();
                vendor.ContactNumber?.Trim();
                vendor.FaxNumber?.Trim();
                vendor.SeparateHqaddress?.Trim();
                vendor.ParentCompanyAddress?.Trim();
                vendor.ContractType?.Trim();
                vendor.SysproId?.Trim();

                //Update DB
                var existingVendor = _portalContext.Vendors.Find(id);

                if (existingVendor != null)
                {

                    existingVendor.CompanyName = vendor.CompanyName ?? existingVendor.CompanyName;
                    existingVendor.TradingAs = vendor.TradingAs ?? existingVendor.TradingAs;
                    existingVendor.CompanyType = vendor.CompanyType ?? existingVendor.CompanyType;
                    existingVendor.RegistrationNumber = vendor.RegistrationNumber ?? existingVendor.RegistrationNumber;
                    existingVendor.VatNumber = vendor.VatNumber ?? existingVendor.VatNumber;
                    existingVendor.PhysicalAddress = vendor.PhysicalAddress ?? existingVendor.PhysicalAddress;
                    existingVendor.District = vendor.District ?? existingVendor.District;
                    existingVendor.Town = vendor.Town ?? existingVendor.Town;
                    existingVendor.PostalAddress = vendor.PostalAddress ?? existingVendor.PostalAddress;
                    existingVendor.ContactNumber = vendor.ContactNumber ?? existingVendor.ContactNumber;
                    existingVendor.FaxNumber = vendor.FaxNumber ?? existingVendor.FaxNumber;
                    existingVendor.SeparateHqaddress = vendor.SeparateHqaddress ?? existingVendor.SeparateHqaddress;
                    existingVendor.ParentCompanyAddress = vendor.ParentCompanyAddress ?? existingVendor.ParentCompanyAddress;
                    existingVendor.ContractType = vendor.ContractType ?? existingVendor.ContractType;
                    //existingVendor.SysproId = vendor.SysproId ?? existingVendor.SysproId;



                    
                    _portalContext.SaveChanges();
                    
                }
                else
                {
                    return NotFound($"Vendor with VendorID {id} does not exist");
                }
                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }
        }


        [HttpPut("Approve")]
        public IActionResult Approve(int vendorID, string erpId)
        {

            if (vendorID == 0 || erpId.IsNullOrEmpty()) return BadRequest(ModelState);

            if (erpId.Length > 10) return BadRequest("Invalid ERP-ID: ID cannot be more that 10 characters");

            try
            {
                var vendor = _portalContext.Vendors.SingleOrDefault(v => v.ProcessId == vendorID);

                if (vendor == null) return NotFound($"Vendor with ID {vendorID} does not exist");

                //Marking the Vendor As approved wit their official SysproID
                vendor.SysproId = erpId;
                vendor.IsFcapproved = 1;

                _portalContext.SaveChanges();

                BadRequest("Error occured while Approving Vendor");

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }
        }


        [HttpPut("MarkAsComplete/{vendorId}")]
        public IActionResult MarkAsComplete(int vendorID)
        {
      
            if (vendorID == 0) return BadRequest(ModelState);
            try
            {
                var vendor = _portalContext.Vendors.SingleOrDefault(v => v.ProcessId == vendorID);

                if (vendor == null) return NotFound($"Vendor with ID {vendorID} does not exist");

                //Marking the Vendor As approved wit their official SysproID

                vendor.ProfileStatus = "COMPLETE";
                
                    _portalContext.SaveChanges();
                
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }
        }

    }
}
