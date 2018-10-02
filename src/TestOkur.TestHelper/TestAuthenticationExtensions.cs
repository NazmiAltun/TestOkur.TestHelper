namespace TestOkur.TestHelper
{
    using System;
    using Microsoft.AspNetCore.Authentication;

    public static class TestAuthenticationExtensions
    {
        public static AuthenticationBuilder AddTestAuth<TTestAuthenticationOptions>(
            this AuthenticationBuilder builder,
            Action<TestAuthenticationOptions> configureOptions)
            where TTestAuthenticationOptions : TestAuthenticationOptions, new()
        {
            return builder
                .AddScheme<TTestAuthenticationOptions, TestAuthenticationHandler<TTestAuthenticationOptions>>(
                "Test Scheme",
                "Test Auth",
                configureOptions);
        }
    }
}
