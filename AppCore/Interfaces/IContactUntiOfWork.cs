namespace AppCore.Interfaces;

public interface IContactUntiOfWork : IAsyncDisposable
{
    IPersonRepositoryAsync Persons { get; }
    ICompanyRepositoryAsync Companies { get; }
    IOrganizationRepositoryAsync Organizations { get; }
    IInteractionRepositoryAsync Interactions { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}