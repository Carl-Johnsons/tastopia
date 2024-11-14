// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Domain.Entities;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [MaxLength(6)]
    public string? EmailConfirmationOTP { get; set; } = null!;
    public DateTime EmailConfirmationExpiry { get; set; } = DateTime.UtcNow;

    [Required]
    public bool Active { get; set; }
}
