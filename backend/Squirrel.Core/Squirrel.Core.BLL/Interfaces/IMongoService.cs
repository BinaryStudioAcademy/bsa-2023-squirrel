using Squirrel.Core.Common.DTO;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface IMongoService
    {
        Task<ICollection<SampleDto>> GetAllAsync();

        Task<SampleDto> GetByIdAsync(int sampleId);

        Task<SampleDto> CreateAsync(NewSampleDto sampleDto);

        Task<SampleDto> UpdateAsync(int sampleId, NewSampleDto sampleDto);

        Task DeleteAsync(int sampleId);
    }
}
