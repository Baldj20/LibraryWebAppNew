﻿using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(TokenDTO dto)
        {
            var entity = await _unitOfWork._tokenMapper.ToEntity(dto);
            await _unitOfWork.RefreshTokens.Add(entity); ;
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.RefreshTokens.Delete(id);
        }

        public string GenerateAccessToken(User user)
        {

            var jwtSettings = _configuration.GetSection("JwtSettings");

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("SecretKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                audience: jwtSettings.GetSection("Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("Expires").Value)),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    

        public async Task<RefreshToken> GenerateRefreshToken(string userLogin)
        {
            var refreshToken = new RefreshToken(Guid.NewGuid(),
                Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                DateTime.UtcNow.AddDays(7),
                false,
                DateTime.UtcNow,
                userLogin);

            return refreshToken;
        }

        public async Task<List<TokenDTO>> GetAll()
        {
            var tokenEntities = await _unitOfWork.RefreshTokens.GetAll();

            var tokensDTO = new List<TokenDTO>();

            foreach (var token in tokenEntities)
            {
                var tokenDTO = _unitOfWork._tokenMapper.ToDTO(token);
                tokenDTO.AccessToken = GenerateAccessToken(await _unitOfWork.Users.GetByLogin(token.UserLogin));
                tokensDTO.Add(tokenDTO);
            }

            return tokensDTO;
        }

        public async Task<TokenDTO> GetById(Guid id)
        {
            var refreshToken = await _unitOfWork.RefreshTokens.GetById(id);
            var tokenDTO = _unitOfWork._tokenMapper.ToDTO(refreshToken);
            tokenDTO.AccessToken = GenerateAccessToken(await _unitOfWork.Users.GetByLogin(refreshToken.UserLogin));
            return tokenDTO;
        }

        public async Task<TokenDTO> RefreshJWTToken(TokenDTO oldToken)
        {
            var storedToken = (await _unitOfWork.RefreshTokens.GetAll())
                .Where(token => token.Token.Equals(oldToken.RefreshToken)).FirstOrDefault();

            if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
                throw new SecurityTokenException("Invalid or expired refresh token");

            storedToken.IsRevoked = true;
            await _unitOfWork.RefreshTokens.Update(storedToken.Id, storedToken);

            var newAccessToken = GenerateAccessToken(await _unitOfWork.Users.GetByLogin(oldToken.UserLogin));
            var newRefreshToken = await GenerateRefreshToken(storedToken.UserLogin);

            return new TokenDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                UserLogin = oldToken.UserLogin,
            };
        }

        public async Task Update(Guid id, TokenDTO dto)
        {
            await Delete(id);
            await Add(dto);
        }
    }
}
