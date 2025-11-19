using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Domain.Users.Interfaces;
using ACleanAPI.Infrastructure.Persistence;
using ACleanAPI.Infrastructure.Users.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ACleanAPI.Infrastructure.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IUserModelMapper _mapper;

    public UserRepository(AppDbContext context, IUserModelMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);
        return users.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return _mapper.MapToEntity(user);
    }
}
