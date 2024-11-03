using System;
using System.Collections.Generic;

namespace Hope.EntityComponent.DBEntities;

public partial class Module
{
    public int ModuleId { get; set; }

    public string ModuleName { get; set; }

    public virtual ICollection<ModuleRole> ModuleRoles { get; set; } = new List<ModuleRole>();
}
