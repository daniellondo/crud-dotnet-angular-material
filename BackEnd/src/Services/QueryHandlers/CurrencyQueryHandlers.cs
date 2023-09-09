namespace Services.QueryHandlers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Dtos;
    using Domain.Dtos.Currency;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class CurrencyQueryHandlers
    {
        public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, BaseResponse<List<CurrencyDto>>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;

            public GetCurrencyQueryHandler(IContext databaseContext, IMapper mapper)
            {
                _context = databaseContext;
                _mapper = mapper;
            }

            public async Task<BaseResponse<List<CurrencyDto>>> Handle(GetCurrencyQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var listDB = _context.DLO_Currencies.AsQueryable();
                    if (query.CurrencyId is not null)
                        listDB = listDB.Where(t => t.CurrencyId == query.CurrencyId);
                    var response = await listDB.ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                    return new BaseResponse<List<CurrencyDto>>("", response);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<List<CurrencyDto>>("Error getting data", null, ex);
                }

            }
        }
    }
}
