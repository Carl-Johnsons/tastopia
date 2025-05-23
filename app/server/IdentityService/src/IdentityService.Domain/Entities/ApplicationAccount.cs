﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Domain.Entities;

// Add profile data for application users by adding properties to the ApplicationUser class
[Table("Account")]
public class ApplicationAccount : IdentityUser
{
    [MaxLength(6)]
    public string? EmailOTP { get; set; } = null!;
    public DateTime? EmailOTPCreated { get; set; }
    public DateTime? EmailOTPExpiry { get; set; }
    [MaxLength(6)]
    public string? PhoneOTP { get; set; } = null!;
    public DateTime? PhoneOTPCreated { get; set; }
    public DateTime? PhoneOTPExpiry { get; set; }
    [MaxLength(6)]
    public string? ForgotPasswordOTP { get; set; } = null!;
    public DateTime? ForgotPasswordCreated { get; set; }
    public DateTime? ForgotPasswordExpiry { get; set; }
    public int RequestOTPCount { get; set; } = 0;
    public bool IsFirstTimeLogin { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
