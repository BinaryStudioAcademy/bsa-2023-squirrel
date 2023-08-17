using Squirrel.Core.Common.DTO;

namespace Squirrel.Core.BLL.Interfaces
{
    public interface ISampleService
    {
        Task<ICollection<SampleDto>> GetAllSamplesAsync();

        Task<SampleDto> GetSampleAsync(int sampleId);

        Task<ICollection<SampleDto>> GetUserSamplesAsync(int userId);

        Task<SampleDto> CreateSampleAsync(NewSampleDto sampleDto);

        Task<SampleDto> UpdateSampleAsync(int sampleId, NewSampleDto sampleDto);

        Task DeleteSampleAsync(int sampleId);
    }
}
