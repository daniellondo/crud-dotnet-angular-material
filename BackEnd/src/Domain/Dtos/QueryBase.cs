namespace Domain.Dtos
{
    using MediatR;

    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
    }
}
