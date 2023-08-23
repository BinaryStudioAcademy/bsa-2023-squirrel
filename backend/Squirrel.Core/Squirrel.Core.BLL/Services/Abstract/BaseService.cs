using AutoMapper;
using Squirrel.Core.DAL.Context;

namespace Squirrel.Core.BLL.Services.Abstract;

public abstract class BaseService
{
    private protected readonly SquirrelCoreContext _context;
    private protected readonly IMapper _mapper;

    public BaseService(SquirrelCoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}