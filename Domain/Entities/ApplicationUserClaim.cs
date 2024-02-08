﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class ApplicationUserClaim: IdentityUserClaim<Guid>
{
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;
}