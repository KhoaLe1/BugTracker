﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BugTracker.Models;

namespace BugTracker.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BugTrackerUser class
    public class BugTrackerUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string firstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string lastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public List<BT_UserRoles> selectRoles { get; set; }
    }
}
