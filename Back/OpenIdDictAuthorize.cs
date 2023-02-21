using Microsoft.AspNetCore.Authorization;
using OpenIddict.Validation.AspNetCore;

namespace Back;

public class OpenIdDictAuthorize : AuthorizeAttribute
{
    public OpenIdDictAuthorize() =>
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
}