﻿using System;
using System.Collections.Generic;

namespace Hope.EntityComponent.DBEntities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; }

    public string Mobile { get; set; }

    public string SupplingArea { get; set; }

    public string CompanyName { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
