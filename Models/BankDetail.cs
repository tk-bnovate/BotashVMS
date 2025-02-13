using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class BankDetail
{
    public int Id { get; set; }

    public int? ProcessId { get; set; }

    public string? VendorName { get; set; }

    public string? AccountHolderName { get; set; }

    public string? BankName { get; set; }

    public string? AccountNumber { get; set; }

    public string? Currency { get; set; }

    public string? BranchCode { get; set; }

    public string? BankBrachAddress { get; set; }

    public string? SwiftCode { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Vendor? Process { get; set; }
}
