namespace TestOkur.TestHelper
{
    using Microsoft.AspNetCore.Authentication;
    using System.Security.Claims;

    public abstract class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public abstract ClaimsIdentity Identity { get; }
    }
}
