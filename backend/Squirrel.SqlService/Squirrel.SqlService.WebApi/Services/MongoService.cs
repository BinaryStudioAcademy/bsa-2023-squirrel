using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Squirrel.Core.DAL.Entities.Common;
using Squirrel.SqlService.WebApi.Interfaces;
using Squirrel.SqlService.WebApi.Options;

namespace Squirrel.SqlService.WebApi.Services;
public class MongoService<T> : IMongoService<T> where T : Entity<long>
{
    private readonly IMongoCollection<T> _mongoCollection;

    public MongoService(IOptions<MongoDatabaseConnectionSettings> mongoDatabaseSettings, string collectionName)
    {
        var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);

        _mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _mongoCollection.InsertOneAsync(entity);

        return entity;
    }

    public async Task DeleteAsync(int entityId)
    {
        await _mongoCollection.DeleteOneAsync(x => x.Id == entityId);
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        var entities = await _mongoCollection.Find(_ => true).ToListAsync();
        return entities;
    }

    public async Task<T> GetByIdAsync(int entityId)
    {
        var entityById = await _mongoCollection.Find(x => x.Id == entityId).FirstOrDefaultAsync();
        return entityById;
    }

    public async Task<T> UpdateAsync(int entityId, T editedEntity)
    {
        await _mongoCollection.ReplaceOneAsync(x => x.Id == entityId, editedEntity);

        return editedEntity;
    }
}
