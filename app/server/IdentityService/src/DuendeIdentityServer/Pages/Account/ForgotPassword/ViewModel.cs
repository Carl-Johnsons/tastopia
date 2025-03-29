// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Contract.DTOs.UserDTO;

namespace DuendeIdentityServer.Pages.Account.ForgotPassword;

public class ViewModel
{
    public string? Identifier { get; set; }
    public SimpleUser? User { get; set; }

}