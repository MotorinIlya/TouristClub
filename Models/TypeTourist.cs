using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class TypeTourist
{
    public int id { get; set; }

    public string? type { get; set; }

    public virtual ICollection<Tourists> Tourists { get; set; } = new List<Tourists>();
}
