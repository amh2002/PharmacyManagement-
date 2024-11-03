﻿using System;
using System.Collections.Generic;

namespace Hope.EntityComponent.DBEntities;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }

    public string MobileNumber { get; set; }

    public bool Gender { get; set; }

    public string Address { get; set; }

    public bool ShiftType { get; set; }

    public decimal Salary { get; set; }

    public int JobDescriptionId { get; set; }

    public DateTime JoinDate { get; set; }

    public DateTime? ResignationDate { get; set; }

    public bool IsActive { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public virtual ICollection<AssignUsersToRole> AssignUsersToRoles { get; set; } = new List<AssignUsersToRole>();

    public virtual ICollection<ErrorLog> ErrorLogs { get; set; } = new List<ErrorLog>();

    public virtual JobDescription JobDescription { get; set; }
}
