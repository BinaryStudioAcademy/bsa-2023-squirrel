using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.Common.DTO;
using Squirrel.Core.Common.Models;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Services
{
    public class MongoService : BaseService, IMongoService
    {
        private readonly IMongoCollection<Sample> _sampleCollection;

        public MongoService(SquirrelCoreContext context, IMapper mapper, IOptions<MongoDatabaseConnectionSettings> mongoDatabaseSettings) : base(context, mapper)
        {
            var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);

            _sampleCollection = mongoDatabase.GetCollection<Sample>(mongoDatabaseSettings.Value.CollectionName);
        }

        public async Task<SampleDto> CreateAsync(NewSampleDto sampleDto)
        {
            var sample = _mapper.Map<Sample>(sampleDto, opts => opts.AfterMap((src, dst) =>
            {
                dst.CreatedAt = DateTime.Now;
                // other assignation logic if needed
            }));

            await _sampleCollection.InsertOneAsync(sample);

            return _mapper.Map<SampleDto>(sample);
        }

        public async Task DeleteAsync(int sampleId)
        {
            await _sampleCollection.DeleteOneAsync(x => x.Id == sampleId);
        }

        public async Task<ICollection<SampleDto>> GetAllAsync()
        {
            var samples = await _sampleCollection.Find(_ => true).ToListAsync();
            return _mapper.Map<ICollection<SampleDto>>(samples);
        }

        public async Task<SampleDto> GetByIdAsync(int sampleId)
        {
            var sampleById = await _sampleCollection.Find(x => x.Id == sampleId).FirstOrDefaultAsync();
            return _mapper.Map<SampleDto>(sampleById);
        }

        public async Task<SampleDto> UpdateAsync(int sampleId, NewSampleDto sampleDto)
        {
            var existedSample = await _sampleCollection.Find(x => x.Id == sampleId).FirstOrDefaultAsync();

            var mergedSample = _mapper.Map(sampleDto, existedSample);

            await _sampleCollection.ReplaceOneAsync(x => x.Id == sampleId, mergedSample);

            return _mapper.Map<SampleDto>(mergedSample);
        }
    }
}
