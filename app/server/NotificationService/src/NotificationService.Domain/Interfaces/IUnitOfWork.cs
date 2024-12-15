namespace NotificationService.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);
}