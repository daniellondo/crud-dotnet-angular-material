namespace Services.MapperConfiguration
{
    using AutoMapper;
    using Domain.Dtos.Currency;
    using Domain.Entities;

    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<CurrencyEntity, CurrencyDto>(MemberList.Source);
            CreateMap<AddCurrencyCommand, CurrencyEntity>(MemberList.Source);
            CreateMap<UpdateCurrencyCommand, CurrencyEntity>(MemberList.Source);
        }
    }
}
