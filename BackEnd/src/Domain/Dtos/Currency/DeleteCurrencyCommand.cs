namespace Domain.Dtos.Currency
{
    public class DeleteCurrencyCommand : CommandBase<BaseResponse<CurrencyDto>>
    {
        public int CurrencyId { get; set; }
    }
}
