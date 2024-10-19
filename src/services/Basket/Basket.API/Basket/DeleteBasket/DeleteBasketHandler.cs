
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Please Specify User Name");
        }
    }
    public class DeleteBasketCommandHandler(IBasketRepository _repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var IsResult = await _repository.DeleteBasket(command.UserName, cancellationToken);
            return new DeleteBasketResult(IsResult);
        }
    }
}
