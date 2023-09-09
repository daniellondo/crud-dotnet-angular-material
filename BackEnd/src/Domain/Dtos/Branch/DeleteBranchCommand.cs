namespace Domain.Dtos.Branch
{
    public class DeleteBranchCommand : CommandBase<BaseResponse<BranchDto>>
    {
        public int BranchId { get; set; }
    }
}
