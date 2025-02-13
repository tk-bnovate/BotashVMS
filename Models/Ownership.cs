using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class Ownership
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public string? VendorName { get; set; }

    public string? Name { get; set; }

    public string? Sex { get; set; }

    public string? Nationality { get; set; }

    public string? Idnumber { get; set; }

    public string? Position { get; set; }

    public int? NumberOfShares { get; set; }

    public string? SharePercentage { get; set; }

    public string? OwnershipAttachement { get; set; }

    public string? OwnershipType { get; set; }

    public string? Country { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Vendor? Vendor { get; set; } 
}
