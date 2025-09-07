using System.Collections.Generic;
using System.Linq;
using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Repository;

public interface IMongoRepository <T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task AddAsync(T entity);
    //Task UpdateAsync(bool FactionStatus);
    Task UpdateAsync(string id, T entity );
    Task DeleteAsync(string id);
}