namespace Services.QueryHandlers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Dtos;
    using Domain.Dtos.Branch;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class BranchQueryHandlers
    {
        public class GetBranchQueryHandler : IRequestHandler<GetBranchQuery, BaseResponse<List<BranchDto>>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;

            public GetBranchQueryHandler(IContext databaseContext, IMapper mapper)
            {
                _context = databaseContext;
                _mapper = mapper;
            }

            public async Task<BaseResponse<List<BranchDto>>> Handle(GetBranchQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var listDB = _context.DLO_Branches.AsQueryable();
                    if (query.BranchId is not null)
                        listDB = listDB.Where(t => t.BranchId == query.BranchId);
                    var response = await listDB.ProjectTo<BranchDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                    return new BaseResponse<List<BranchDto>>("", response);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<List<BranchDto>>("Error getting data", null, ex);
                }

            }
        }
    }
}
