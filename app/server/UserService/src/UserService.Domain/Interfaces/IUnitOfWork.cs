namespace UserService.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);
}