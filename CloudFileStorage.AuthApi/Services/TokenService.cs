﻿using CloudFileStorage.AuthApi.Models.Entities;
using CloudFileStorage.AuthApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudFileStorage.AuthApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (string RefreshToken, DateTime RefreshExpire) GenerateRefreshToken()
        {
            var refreshToken = Guid.NewGuid().ToString();
            var refreshExpireDate = DateTime.Now.AddHours(int.Parse(_config["RefreshToken:ExpireHours"]!));
            return (refreshToken, refreshExpireDate);
        }

        public (string AccessToken, string RefreshToken, DateTime RefreshExpire) GenerateTokens(User user)
        {
            var accessToken = GenerateJwt(user);
            var (refreshToken, refreshExpire) = GenerateRefreshToken();
            return (accessToken, refreshToken, refreshExpire);
        }

    }
}
