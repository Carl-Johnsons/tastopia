// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string AvatarUrl { get; set; } = string.Empty;

    [Required]
    public string BackgroundUrl { get; set; } = string.Empty;

    [Required]
    public DateTime Dob { get; set; }

    [Required]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public bool Active { get; set; }

}
