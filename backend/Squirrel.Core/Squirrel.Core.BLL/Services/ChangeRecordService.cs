﻿using AutoMapper;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services.Abstract;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.BLL.Services;

public sealed class ChangeRecordService : BaseService, IChangeRecordService
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IDbStructureSaverService _dBStructureSaver;

    public ChangeRecordService(SquirrelCoreContext context, IMapper mapper, IUserIdGetter userIdGetter, IDbStructureSaverService dBStructureSaver)
        : base(context, mapper)
    {
        _userIdGetter = userIdGetter;
        _dBStructureSaver = dBStructureSaver;
    }


    public async Task<Guid> AddChangeRecordAsync(Guid clientId)
    {
        ChangeRecord changeRecordEntity = new()
        {
            CreatedBy = _userIdGetter.GetCurrentUserId(),
            UniqueChangeId = Guid.NewGuid()
        };

        await _dBStructureSaver.SaveDbStructureToAzureBlobAsync(changeRecordEntity, clientId);

        var addedChangeRecord = (await _context.ChangeRecords.AddAsync(changeRecordEntity)).Entity;
        await _context.SaveChangesAsync();

        return addedChangeRecord.UniqueChangeId;
    }
}