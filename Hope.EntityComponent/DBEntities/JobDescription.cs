using System;
using System.Collections.Generic;

namespace Hope.EntityComponent.DBEntities;

public partial class JobDescription
{
    public int JobDescriptionId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
