namespace Domain.Dtos.Branch
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Identification { get; set; }
        public string CurrencyId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
