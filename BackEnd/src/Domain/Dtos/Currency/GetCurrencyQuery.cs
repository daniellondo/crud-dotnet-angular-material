namespace Domain.Dtos.Currency
{
    public class GetCurrencyQuery : QueryBase<BaseResponse<List<CurrencyDto>>>
    {
        public int? CurrencyId { get; set; }
    }
}
