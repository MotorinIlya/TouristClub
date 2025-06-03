using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Training
{
    public int id { get; set; }

    public int? id_group { get; set; }

    public DateTime? time_to_training { get; set; }

    public int? id_coach { get; set; }

    public virtual ICollection<TrainingAttendance> TrainingAttendance { get; set; } = new List<TrainingAttendance>();

    public virtual Coaches? id_coachNavigation { get; set; }

    public virtual Groups? id_groupNavigation { get; set; }
}
