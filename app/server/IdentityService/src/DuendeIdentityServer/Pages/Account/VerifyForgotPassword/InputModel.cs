// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.Pages.Account.VerifyForgotPassword;

public class InputModel
{
    [Required]
    public string OTP { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Retype Password is required")]
    public string RetypePassword { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
    public string? Button { get; set; }
}