namespace Domain.Entities
{
    public class CurrencyEntity
    {
        public int CurrencyId { get; set; }
        public string RefCode { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public List<BranchEntity> Branches { get; set; }
    }
}
