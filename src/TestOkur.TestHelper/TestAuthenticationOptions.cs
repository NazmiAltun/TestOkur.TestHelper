namespace TestOkur.TestHelper
{
    using Microsoft.AspNetCore.Authentication;
    using System;
    using System.Security.Claims;

    public class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public virtual ClaimsIdentity Identity { get; } = new ClaimsIdentity(
            new[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Guid.NewGuid().ToString())
            }, "test");
    }
}
