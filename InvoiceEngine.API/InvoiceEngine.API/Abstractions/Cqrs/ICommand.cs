namespace InvoiceEngine.API.Abstractions.Cqrs;

public interface ICommand :
    ICommand<Unit>
{

}
public interface ICommand<out TResponse> :
    IRequest<TResponse>
{

}
