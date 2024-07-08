using Athena.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Interfaces;

public interface IUnitOfWork
{

    IUserRepository UserRepository { get; }
    IRolesRepository RolesRepository { get; }

    bool IsModified<T>(T entity);
    Task<IDbContextTransaction> BeginDataBaseTransaction();
    Task<bool> CompleteAsync();
}
