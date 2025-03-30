// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace DuendeIdentityServer.Pages.Account.ForgotPassword;

public class InputModel
{
    public string? Identifier { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
    public string? Button { get; set; }
}