using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class TouristToGroup
{
    public int id { get; set; }

    public int? id_tourist { get; set; }

    public int? id_group { get; set; }

    public virtual Groups? id_groupNavigation { get; set; }

    public virtual Tourists? id_touristNavigation { get; set; }
}
