using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class Contact
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public string? CompanyName { get; set; }

    public string? SalesContactName { get; set; }

    public string? SalesTelephoneNumber { get; set; }

    public string? SalesCellphoneNumber { get; set; }

    public string? SalesEmailAddress { get; set; }

    public string? WebAddress { get; set; }

    public string? FinanceContactName { get; set; }

    public string? FinanceTelephoneNumber { get; set; }

    public string? FinanceCellphoneNumber { get; set; }

    public string? FinanceEmailAddress { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Vendor? Vendor { get; set; } 
}
