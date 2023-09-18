using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.DAL.Context;

namespace Squirrel.Core.BLL.Services;

public sealed class CommitFileService: ICommitFileService
{
    private readonly SquirrelCoreContext _context;

    public CommitFileService(SquirrelCoreContext context)
    {
        _context = context;
    }
    
    public async Task<List<string>> GetBlobIdsByCommitId(int commitId)
    {
        return await _context.CommitFiles
            .Where(f => f.CommitId == commitId)
            .Select(r => r.BlobId).ToListAsync();
    }
}