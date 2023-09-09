namespace Services.CommandHandlers
{
    using AutoMapper;
    using Data;
    using Domain.Dtos;
    using Domain.Dtos.Currency;
    using Domain.Dtos.Currency;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;

    public class CurrencyCommandHandlers
    {
        public class AddCurrencyCommandHandler : IRequestHandler<AddCurrencyCommand, BaseResponse<bool>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public AddCurrencyCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BaseResponse<bool>> Handle(AddCurrencyCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var currency = _mapper.Map<CurrencyEntity>(command);
                    await _context.DLO_Currencies.AddAsync(currency, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<bool>("Added successfully!", true);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<bool>(ex.Message + " " + ex.StackTrace, false, null);
                }
            }
        }

        public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, BaseResponse<bool>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public UpdateCurrencyCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BaseResponse<bool>> Handle(UpdateCurrencyCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var currency = await _context.DLO_Currencies.FirstAsync(Currency => Currency.CurrencyId == command.CurrencyId, cancellationToken);
                    _mapper.Map(command, currency);
                    _context.DLO_Currencies.Update(currency);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<bool>("Updated successfully!", true);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<bool>(ex.Message + " " + ex.StackTrace, false, null);
                }
            }
        }

        public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, BaseResponse<CurrencyDto>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public DeleteCurrencyCommandHandler(IContext databaseContext, IMapper mapper)
            {
                _context = databaseContext;
                _mapper = mapper;
            }
            public async Task<BaseResponse<CurrencyDto>> Handle(DeleteCurrencyCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _context.DLO_Currencies.FirstAsync(t => t.CurrencyId.Equals(command.CurrencyId));
                    _context.DLO_Currencies.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<CurrencyDto>("Updated successfully!", _mapper.Map(entity, new CurrencyDto()));
                }
                catch (Exception ex)
                {
                    return new BaseResponse<CurrencyDto>(ex.Message + " " + ex.StackTrace, new CurrencyDto(), null);
                }
            }
        }
    }
}
