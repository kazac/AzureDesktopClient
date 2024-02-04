using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Eraz51;

public class MyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public MyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock ) 
        : base(options, logger, encoder, clock)
    { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (authHeader != null && authHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
        {
            var token = new JwtSecurityToken(jwtEncodedString: authHeader.Substring(7));
            var name_claim = (from x in token.Claims where x.Type == "name" select new Claim("name", x.Value)).First();
            var claims = new[]
            {
                name_claim,
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "test_role")
                };
            var identity = new ClaimsIdentity(claims, "Bearer");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
        if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
        {
            var token = authHeader.Substring("Basic ".Length).Trim();
            System.Console.WriteLine(token);
            var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialstring.Split(':');
            if (credentials[0] == "Aladdin" && credentials[1] == "open sesame")  // Authorization: Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==
            {
                var claims = new[] 
                { 
                    new Claim("name", credentials[0]), 
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "test_role")

                };
                var identity = new ClaimsIdentity(claims, "Basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
            }
        }
        Response.StatusCode = 401;
        Response.Headers.Add("WWW-Authenticate", "Basic realm=\"kazac.net\"");
        return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
    }
}