using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class Compliance
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public string? Isocertified { get; set; }

    public string? CertificateType { get; set; }

    public string? Bobs { get; set; }

    public string? OtherCertificate { get; set; }

    public string? OperationPolicies { get; set; }

    public string? QualityStandards { get; set; }

    public string? CorrectiveActionPlan { get; set; }

    public string? QualityControl { get; set; }

    public string? StoragePolicy { get; set; }

    public string? ProductWarranty { get; set; }

    public string? RiskAssessmentPlan { get; set; }

    public string? MaterialsSafety { get; set; }

    public string? Hivaidspolicy { get; set; }

    public string? SiteSpecificSafetyPlan { get; set; }

    public string? FitforDutyPolicy { get; set; }

    public string? FirstAidTraining { get; set; }

    public string? HearingConservation { get; set; }

    public string? RespirationProtection { get; set; }

    public string? UseofPpe { get; set; }

    public string? HazardousMaterialsPlan { get; set; }

    public string? IncidentInvestigationProcedure { get; set; }

    public string? InspectionandMaintenanceRecords { get; set; }

    public string? HealthCoordinator { get; set; }

    public string? OrientationProgramme { get; set; }

    public string? TrainingProgramme { get; set; }

    public string? EnvironmentStandards { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Vendor? Vendor { get; set; } 
}
