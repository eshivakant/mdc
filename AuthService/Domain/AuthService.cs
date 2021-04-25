using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MDC.AuthService.Domain
{
    public class AuthService
    {
        private readonly AppSettings _appSettings;
        private readonly IBusinessUsersRepo _userRepo;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IBusinessUsersRepo userRepo, IOptions<AppSettings> appSettings, ILogger<AuthService> logger)
        {
            this._userRepo = userRepo;
            _logger = logger;
            this._appSettings = appSettings.Value;
        }

        public string Authenticate(string login, string pwd)
        {
            var user = _userRepo.FindByLogin(login);

            if (user == null)
                return null;

            if (!user.PasswordMatches(pwd))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", user.Login),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, "CONTRIBUTOR"),
                    new Claim(ClaimTypes.Role, "USER"),
                    new Claim("userType", "BUSINESSUSER")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public BusinessUser UserFromLogin(string login)
        {
            return _userRepo.FindByLogin(login);
        }
    }
}