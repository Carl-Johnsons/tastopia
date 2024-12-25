// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace DuendeIdentityServer.Pages.Account.Register;

public static class RegisterOptions
{
    public static readonly bool AllowLocalLogin = true;
    public static readonly bool AllowRememberLogin = true;
    public static readonly TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    public static readonly string InvalidCredentialsErrorMessage = "Invalid username or password";
    public static readonly string AccountDisabledErrorMessage = "Your account has been disabled";
}