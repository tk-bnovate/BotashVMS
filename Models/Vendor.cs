using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class Vendor
{
    public int ProcessId { get; set; }

    public string? CompanyName { get; set; }

    public string? TradingAs { get; set; }

    public string? CompanyType { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? VatNumber { get; set; }

    public string? PhysicalAddress { get; set; }

    public string? District { get; set; }

    public string? Town { get; set; }

    public string? PostalAddress { get; set; }

    public string? ContactNumber { get; set; }

    public string? FaxNumber { get; set; }

    public string? SeparateHqaddress { get; set; }

    public string? ParentCompanyAddress { get; set; }

    public string? ContractType { get; set; }

    public string? SysproId { get; set; }

    public int? IsFcapproved { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public string? ProfileStatus { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<BankDetail> BankDetails { get; set; } = new List<BankDetail>();

    public virtual ICollection<Compliance> Compliances { get; set; } = new List<Compliance>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<FinancialInfo> FinancialInfos { get; set; } = new List<FinancialInfo>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();

    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
}
