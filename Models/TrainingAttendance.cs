using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class TrainingAttendance
{
    public int id { get; set; }

    public int? id_training { get; set; }

    public int? id_tourist { get; set; }

    public virtual Tourists? id_touristNavigation { get; set; }

    public virtual Training? id_trainingNavigation { get; set; }
}
