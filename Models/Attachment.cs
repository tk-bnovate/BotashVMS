using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class Attachment
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public string? VendorName { get; set; }

    public string? DocumentType { get; set; }

    public string? DocumentPath { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Vendor? Vendor { get; set; } 
}
