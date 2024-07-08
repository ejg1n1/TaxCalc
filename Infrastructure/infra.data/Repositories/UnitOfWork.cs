using Athena.Core.Interfaces;
using Athena.Infrastructure.Repositories;
using AutoMapper;
using Core.Interfaces;
using infra.data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    private readonly Lazy<IUserRepository> _usersRepository;
    private readonly Lazy<IRolesRepository> _rolesRepository;

    public UnitOfWork(ApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        
        _usersRepository = new Lazy<IUserRepository>(() => new UserRepository(_context, _mapper), true);
        _rolesRepository = new Lazy<IRolesRepository>(() => new RolesRepository(_context, mapper), true);
    }

    public IUserRepository UserRepository => _usersRepository.Value;
    public IRolesRepository RolesRepository => _rolesRepository.Value;
    
    public bool IsModified<T>(T entity)
    {
        return entity != null && _context.Entry(entity).State == EntityState.Modified;
    }

    public async Task<IDbContextTransaction> BeginDataBaseTransaction()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
