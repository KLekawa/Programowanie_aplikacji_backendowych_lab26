using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T> : IGenericRepositoryAsync<T> where T : EntityBase
{
    protected Dictionary<Guid, T> _data = new();
    
    
    public Task<T?> FindByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
        return Task.FromResult(_data.Values.AsEnumerable());
    }

    public Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        if(page <= 0)
            throw new ArgumentOutOfRangeException(nameof(page), "page must be greater than zero");
        
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize must be greater than zero");
        
        var totalCount = _data.Count;

        var data = _data .Values
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PagedResult<T>(data, totalCount, page, pageSize);
        
        return Task.FromResult(result);
    }

    public Task<T> AddAsync(T entity)
    {
        if(entity.Id == Guid.Empty)
            entity.Id = Guid.NewGuid();
        if (_data.TryAdd(entity.Id, entity))
            return Task.FromResult(entity);
                
        throw new Exception("Cannot add entity");
    }

    public Task<T> UpdateAsync(T entity)
    {
        if(!_data.ContainsKey(entity.Id))
            throw new KeyNotFoundException($"Entity with id {entity.Id} not found");
        _data[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public Task RemoveByIdAsync(Guid id)
    {
        if(!_data.Remove(id))
            throw new KeyNotFoundException($"Entity with id {id} not found");
        return Task.CompletedTask;
    }
}