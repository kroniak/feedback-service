using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FeedBack.Core.Database.Interfaces;
using FeedBack.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FeedBack.WebApi.Services.Security
{
    /// <inheritdoc />
    public class SimpleAuthenticateService : ISimpleAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly byte[] _key;

        /// <inheritdoc />
        public SimpleAuthenticateService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            _key = SecurityServicesExtensions.GetKeyEncoded(configuration);
        }

        /// <inheritdoc />
        public async Task<string> CheckUserCredentials(string userName, string password)
        {
            var user = await _userRepository.GetSecureUserAsync(userName);

            if (user == null) return null;

            var verificationResult =
                new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, password);

            if (verificationResult != PasswordVerificationResult.Success) return null;

            // authentication successful. Generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }.Concat(GetUserRolesClaims(user))),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static IEnumerable<Claim> GetUserRolesClaims(User user) =>
            !user.Roles.Any()
                ? Enumerable.Empty<Claim>()
                : user.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString()));
    }
}