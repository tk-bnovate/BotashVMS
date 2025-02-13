using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class FinancialInfo
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public string? VendorName { get; set; }

    public int? AnnualTurnOver { get; set; }

    public int? NumberOfEmployees { get; set; }

    public int? TotalLabour { get; set; }

    public int? UnskilledLabour { get; set; }

    public int? SkilledLabour { get; set; }

    public int? Managers { get; set; }

    public int? Directors { get; set; }

    public string? NatureOfBusiness { get; set; }

    public string? NatureOfProducts { get; set; }

    public string? Financials { get; set; }

    public string? SettlementTerms { get; set; }

    public string? OffsetAccounts { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Vendor? Vendor { get; set; } 
}
