namespace TestOkur.TestHelper
{
    using Microsoft.AspNetCore.Authentication;
    using System.Security.Claims;

    public class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public virtual ClaimsIdentity Identity { get; }
    }
}
