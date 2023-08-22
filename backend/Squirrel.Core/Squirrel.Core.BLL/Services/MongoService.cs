using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.Models;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Services
{
    public class MongoService<T> : BaseService, IMongoService<T> where T : Entity<long>
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoService(SquirrelCoreContext context, IMapper mapper, IOptions<MongoDatabaseConnectionSettings> mongoDatabaseSettings) : base(context, mapper)
        {
            var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);

            string collectionName = typeof(T) switch
            {
                Type t when t == typeof(Sample) => "SampleCollection",
                // Add more cases for other entity types as needed
                _ => throw new ArgumentException("Unknown entity type")
            };

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
}
