using AutoMapper;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _context;
        public RefreshTokenRepository(LibraryDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Add(RefreshToken entity)
        {
            var token = _mapper.Map<RefreshTokenEntity>(entity);

            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await _context.RefreshTokens
                .Where(token => token.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<RefreshToken>> GetAll()
        {
            return _mapper
                .Map<List<RefreshToken>>(await _context.RefreshTokens
                .AsNoTracking()
                .ToListAsync());   
        }

        public async Task<RefreshToken> GetById(Guid id)
        {
            return _mapper
                .Map<RefreshToken>(await _context.RefreshTokens
                .Where(token => token.Id == id)
                .AsNoTracking()
                .FirstAsync());
        }

        public async Task Update(Guid id, RefreshToken entity)
        {
            await Delete(id);
            await Add(entity);
        }
    }
}
