using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Athletes
{
    public int id { get; set; }

    public int? tourist_id { get; set; }

    public virtual Tourists? tourist { get; set; }
}
