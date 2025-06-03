using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Sections
{
    public int id { get; set; }

    public int? id_leader { get; set; }

    public string? name { get; set; }

    public virtual ICollection<Coaches> Coaches { get; set; } = new List<Coaches>();

    public virtual ICollection<Groups> Groups { get; set; } = new List<Groups>();

    public virtual ICollection<Schedule> Schedule { get; set; } = new List<Schedule>();

    public virtual Leaders? id_leaderNavigation { get; set; }
}
