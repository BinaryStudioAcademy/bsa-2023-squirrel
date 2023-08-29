﻿using Squirrel.Core.DAL.Entities.Common;

namespace Squirrel.SqlService.WebApi.Interfaces;

public interface IMongoService<T> where T : Entity<long>
{
    Task<ICollection<T>> GetAllAsync();

    Task<T> GetByIdAsync(int entityId);

    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(int entityId, T editedEntity);

    Task DeleteAsync(int entityId);
}
