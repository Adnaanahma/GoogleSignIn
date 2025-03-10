

namespace GoogleSignIn.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task Login(string redirectUrl)
        {
            if (string.IsNullOrWhiteSpace(redirectUrl))
                throw new ArgumentException("Redirect URL cannot be null or empty", nameof(redirectUrl));

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is not available");

            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            await httpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);
        }

        public async Task Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is not available");

            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public UserModel GetUserInfo()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
                return null;

            var user = httpContext.User;
            var name = user.Identity?.Name;
            var email = user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            return new UserModel { Name = name, Email = email };
        }
    }
}

