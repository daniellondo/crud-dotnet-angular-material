namespace Domain.Dtos
{
    using MediatR;
    public abstract class CommandBase<T> : IRequest<T>
    {
    }
}
