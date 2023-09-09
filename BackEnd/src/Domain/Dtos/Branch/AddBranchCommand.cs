namespace Domain.Dtos.Branch
{
    public class AddBranchCommand : CommandBase<BaseResponse<List<BranchDto>>>
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Identification { get; set; }
        public DateTime CreateDate { get; set; }
        public int CurrencyId { get; set; }
    }
}
