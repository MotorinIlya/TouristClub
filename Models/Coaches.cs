using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Coaches
{
    public int id { get; set; }

    public int? tourist_id { get; set; }

    public int? section_id { get; set; }

    public virtual ICollection<Groups> Groups { get; set; } = new List<Groups>();

    public virtual ICollection<Schedule> Schedule { get; set; } = new List<Schedule>();

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();

    public virtual Sections? section { get; set; }

    public virtual Tourists? tourist { get; set; }
}
