namespace Domain.Dtos.Currency
{
    public class AddCurrencyCommand : CommandBase<BaseResponse<bool>>
    {
        public string RefCode { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
