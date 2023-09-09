namespace Services.MapperConfiguration
{
    using AutoMapper;
    using Domain.Dtos.Branch;
    using Domain.Entities;

    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<BranchEntity, BranchDto>(MemberList.Source)
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Currency.Currency));
            CreateMap<AddBranchCommand, BranchEntity>(MemberList.Source);
            CreateMap<UpdateBranchCommand, BranchEntity>(MemberList.Source);
        }
    }
}
