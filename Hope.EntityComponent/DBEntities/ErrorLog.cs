using System;
using System.Collections.Generic;

namespace Hope.EntityComponent.DBEntities;

public partial class ErrorLog
{
    public int ErrorId { get; set; }

    public string ErrorMessage { get; set; }

    public string ErrorException { get; set; }

    public string ModuleName { get; set; }

    public DateTime TransactionDate { get; set; }

    public int? UserId { get; set; }

    public virtual User User { get; set; }
}
