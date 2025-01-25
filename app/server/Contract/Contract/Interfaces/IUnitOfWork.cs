namespace Contract.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);
}