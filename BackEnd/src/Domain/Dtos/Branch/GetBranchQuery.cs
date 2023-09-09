namespace Domain.Dtos.Branch
{
    public class GetBranchQuery : QueryBase<BaseResponse<List<BranchDto>>>
    {
        public int? BranchId { get; set; }
    }
}
