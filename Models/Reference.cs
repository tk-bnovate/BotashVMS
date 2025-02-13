using System;
using System.Collections.Generic;

namespace BotashVMS.Models;

public partial class Reference
{
    public int Id { get; set; }

    public int ProcessId { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public string? ApproximationValue { get; set; }

    public string? ProjectName { get; set; }

    public string? ProjectLocation { get; set; }

    public string? GoodsOrServices { get; set; }

    public string? Referee { get; set; }

    public string? ContactDetails { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Vendor? Process { get; set; } 
}
