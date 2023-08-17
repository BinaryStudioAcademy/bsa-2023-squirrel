namespace Squirrel.Core.Common.DTO
{
    public class SampleDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
