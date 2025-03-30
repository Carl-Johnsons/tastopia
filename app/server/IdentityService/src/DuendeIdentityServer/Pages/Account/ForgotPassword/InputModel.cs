// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Contract.DTOs.UserDTO;
using System.ComponentModel.DataAnnotations;

namespace DuendeIdentityServer.Pages.Account.ForgotPassword;

public class InputModel
{
    [Required]
    public string Identifier { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
    public string? Button { get; set; }
}