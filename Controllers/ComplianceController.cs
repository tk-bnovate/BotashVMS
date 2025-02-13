using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplianceController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public ComplianceController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        // GET: api/<ComplianceController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _portalContext.Compliances.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }

        }

        // GET api/<ComplianceController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _portalContext.Compliances.SingleOrDefault(v => v.Id == id);

                if (result == null)
                {
                    return NotFound($"Compliance with ID {id} does not exist");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // POST api/<ComplianceController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Compliance compliance)
        {
            try
            {
                var newCompliance = _portalContext.Add(compliance);
                try
                {
                    _portalContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //return BadRequest("Error occured while creating Compliance.");
                    return BadRequest(ModelState);

                }


                var newComplianceID = _portalContext
                    .Compliances
                    .OrderBy(v => v.Id)
                    .Last()
                    .Id;
                return Created("Compliance", newComplianceID);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }


        }

        // PUT api/<ComplianceController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Compliance compliance)
        {
            try
            {


                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingCompliance = _portalContext.Compliances.Find(id);

                if (existingCompliance != null)
                {
                    try
                    {
                        existingCompliance.Isocertified = compliance.Isocertified;
                        existingCompliance.CertificateType = compliance.CertificateType;
                        existingCompliance.Bobs = compliance.Bobs;
                        existingCompliance.OtherCertificate = compliance.OtherCertificate;
                        existingCompliance.OperationPolicies = compliance.OperationPolicies;
                        existingCompliance.QualityStandards = compliance.QualityStandards;
                        existingCompliance.CorrectiveActionPlan = compliance.CorrectiveActionPlan;
                        existingCompliance.QualityControl = compliance.QualityControl;
                        existingCompliance.StoragePolicy = compliance.StoragePolicy;
                        existingCompliance.ProductWarranty = compliance.ProductWarranty;
                        existingCompliance.RiskAssessmentPlan = compliance.RiskAssessmentPlan;
                        existingCompliance.MaterialsSafety = compliance.MaterialsSafety;
                        existingCompliance.Hivaidspolicy = compliance.Hivaidspolicy;
                        existingCompliance.SiteSpecificSafetyPlan = compliance.SiteSpecificSafetyPlan;
                        existingCompliance.FitforDutyPolicy = compliance.FitforDutyPolicy;
                        existingCompliance.FirstAidTraining = compliance.FirstAidTraining;
                        existingCompliance.HearingConservation = compliance.HearingConservation;
                        existingCompliance.RespirationProtection = compliance.RespirationProtection;
                        existingCompliance.UseofPpe = compliance.UseofPpe;
                        existingCompliance.HazardousMaterialsPlan = compliance.HazardousMaterialsPlan;
                        existingCompliance.IncidentInvestigationProcedure = compliance.IncidentInvestigationProcedure;
                        existingCompliance.InspectionandMaintenanceRecords = compliance.InspectionandMaintenanceRecords;
                        existingCompliance.HealthCoordinator = compliance.HealthCoordinator;
                        existingCompliance.OrientationProgramme = compliance.OrientationProgramme;
                        existingCompliance.TrainingProgramme = compliance.TrainingProgramme;
                        existingCompliance.EnvironmentStandards = compliance.EnvironmentStandards;
                        _portalContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        BadRequest("Error occured while Updating Compliance");
                    }
                }
                else
                {
                    return NotFound($"Compliance with ID {id} not found");
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
