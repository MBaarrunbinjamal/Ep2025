using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_project2025.Areas.Identity.Data;

// Add profile data for application users by adding properties to the E_project2025User class
public class E_project2025User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [DefaultValue(false)]
    public bool isApproved { get; set; }
}

