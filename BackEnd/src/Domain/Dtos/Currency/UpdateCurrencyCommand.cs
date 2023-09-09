namespace Domain.Dtos.Currency
{
    public class UpdateCurrencyCommand : CommandBase<BaseResponse<bool>>
    {
        public int CurrencyId { get; set; }
        public string RefCode { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
