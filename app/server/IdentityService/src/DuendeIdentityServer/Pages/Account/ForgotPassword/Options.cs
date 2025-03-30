// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace DuendeIdentityServer.Pages.Account.ForgotPassword;

public static class Options
{
    public static readonly bool AllowLocalLogin = true;
    public static readonly bool AllowRememberLogin = true;
    public static readonly TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    public static readonly string NotFound = "No account associate with this identifier";
    public static readonly string IdentifierRequired = "Identifier field is required";
    public static readonly string AccountDisabledErrorMessage = "Your account has been disabled";
}