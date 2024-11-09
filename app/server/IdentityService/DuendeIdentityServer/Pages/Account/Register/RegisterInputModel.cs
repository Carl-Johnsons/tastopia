// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.Pages.Account.Register;

public class RegisterInputModel
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Dob { get; set; }

    [Required]
    public string Gender { get; set; } = string.Empty;

    public bool RememberLogin { get; set; }
    public string? ReturnUrl { get; set; }
    public string? Button { get; set; }
}