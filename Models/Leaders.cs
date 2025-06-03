using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Leaders
{
    public int id { get; set; }

    public string? first_name { get; set; }

    public string? second_name { get; set; }

    public virtual ICollection<Sections> Sections { get; set; } = new List<Sections>();
}
