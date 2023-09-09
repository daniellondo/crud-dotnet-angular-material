namespace Services.CommandHandlers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Dtos;
    using Domain.Dtos.Branch;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    public class BranchCommandHandlers
    {
        public class AddBranchCommandHandler : IRequestHandler<AddBranchCommand, BaseResponse<List<BranchDto>>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public AddBranchCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BaseResponse<List<BranchDto>>> Handle(AddBranchCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _mapper.Map<BranchEntity>(command);
                    await _context.DLO_Branches.AddAsync(entity, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    var listDB = _context.DLO_Branches.AsQueryable();
                        listDB = listDB.Where(t => t.Code == command.Code);
                    var response = await listDB.ProjectTo<BranchDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                    return new BaseResponse<List<BranchDto>>("Added successfully!", response);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<List<BranchDto>>(ex.Message + " " + ex.StackTrace, new List<BranchDto>(), null);
                }
            }
        }

        public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, BaseResponse<BranchDto>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public UpdateBranchCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BaseResponse<BranchDto>> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var branch = await _context.DLO_Branches.FirstAsync(Branch => Branch.BranchId == command.BranchId, cancellationToken);
                    _mapper.Map(command, branch);
                    _context.DLO_Branches.Update(branch);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<BranchDto>("Updated successfully!", _mapper.Map<BranchDto>(branch));
                }
                catch (Exception ex)
                {
                    return new BaseResponse<BranchDto>(ex.Message + " " + ex.StackTrace, new BranchDto(), null);
                }
            }
        }

        public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, BaseResponse<BranchDto>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public DeleteBranchCommandHandler(IContext databaseContext, IMapper mapper)
            {
                _context = databaseContext;
                _mapper = mapper;
            }
            public async Task<BaseResponse<BranchDto>> Handle(DeleteBranchCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _context.DLO_Branches.FirstAsync(t => t.BranchId.Equals(command.BranchId));
                    _context.DLO_Branches.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<BranchDto>("Updated successfully!", _mapper.Map(entity, new BranchDto()));
                }
                catch (Exception ex)
                {
                    return new BaseResponse<BranchDto>(ex.Message + " " + ex.StackTrace, new BranchDto(), null);
                }
            }
        }
    }
}
