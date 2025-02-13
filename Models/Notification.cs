using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime? DateSent { get; set; }

    public int? MarkedAsRead { get; set; }

    public virtual Vendor? Vendor { get; set; } 
}
