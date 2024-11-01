using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LibraryWebApp.Application.DTO;

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

        public string GenerateAccessToken(string userLogin, string role)
        {
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings").GetSection("SecretKey").Value);

            //var claims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name, userLogin),
            //    new Claim(ClaimTypes.Role, role)
            //};

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.AddMinutes(5),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            //            SecurityAlgorithms.HmacSha256)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);

            var jwtSettings = _configuration.GetSection("JwtSettings");

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, userLogin),
            new Claim(ClaimTypes.Role, role)
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
    

        public async Task<RefreshToken> GenerateRefreshToken(string userLogin, string role)
        {
            var refreshToken = new RefreshToken(Guid.NewGuid(),
                Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                DateTime.UtcNow.AddDays(7),
                false,
                DateTime.UtcNow,
                userLogin,
                role);

            //user.RefreshTokens.Add(refreshToken);
            //_context.RefreshTokens.Add(_mapper.Map<RefreshTokenEntity>(refreshToken));
            //await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<List<TokenDTO>> GetAll()
        {
            var tokenEntities = await _unitOfWork.RefreshTokens.GetAll();

            var tokensDTO = new List<TokenDTO>();

            foreach (var token in tokenEntities)
            {
                var tokenDTO = _unitOfWork._tokenMapper.ToDTO(token);
                tokenDTO.AccessToken = GenerateAccessToken(token.UserLogin, token.UserRole);
                tokensDTO.Add(tokenDTO);
            }

            return tokensDTO;
        }

        public async Task<TokenDTO> GetById(Guid id)
        {
            var tokenDTO = _unitOfWork._tokenMapper.ToDTO(await _unitOfWork.RefreshTokens.GetById(id));
            tokenDTO.AccessToken = GenerateAccessToken(tokenDTO.UserLogin, tokenDTO.Role);
            return tokenDTO;
        }

        public async Task<TokenDTO> RefreshJWTToken(TokenDTO oldToken)
        {
            //var storedToken = await _context.RefreshTokens
            //.Include(rt => rt.User)
            //.FirstOrDefaultAsync(rt => rt.Token.Equals(refreshToken) && !rt.IsRevoked);
            var storedToken = (await _unitOfWork.RefreshTokens.GetAll())
                .Where(token => token.Token.Equals(oldToken.RefreshToken)).FirstOrDefault();

            if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
                throw new SecurityTokenException("Invalid or expired refresh token");

            storedToken.IsRevoked = true;
            await _unitOfWork.RefreshTokens.Update(storedToken.Id, storedToken);

            var newAccessToken = GenerateAccessToken(oldToken.UserLogin, oldToken.Role);
            var newRefreshToken = await GenerateRefreshToken(storedToken.UserLogin, storedToken.UserRole);

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
