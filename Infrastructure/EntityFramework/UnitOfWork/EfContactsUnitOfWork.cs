using AppCore.Interfaces;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.UnitOfWork;

public class EfContactsUnitOfWork(
    IPersonRepositoryAsync personRepository,
    ICompanyRepositoryAsync companyRepository,
    IOrganizationRepositoryAsync organizationRepository,
    IInteractionRepositoryAsync interactionRepository,
    ContactsDbContext context
    ) : IContactUntiOfWork
{
    
    public ValueTask DisposeAsync()
    {
        return context.DisposeAsync();
    }

    public IPersonRepositoryAsync Persons => personRepository;
    public ICompanyRepositoryAsync Companies => companyRepository;
    public IOrganizationRepositoryAsync Organizations => organizationRepository;
    public IInteractionRepositoryAsync Interactions => interactionRepository;
    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    public Task BeginTransactionAsync()
    {
        return context.Database.BeginTransactionAsync();
    }

    public Task CommitTransactionAsync()
    {
        return context.Database.CommitTransactionAsync();
    }

    public Task RollbackTransactionAsync()
    {
        return context.Database.RollbackTransactionAsync();
    }
}