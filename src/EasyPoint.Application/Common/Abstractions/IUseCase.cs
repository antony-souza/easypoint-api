namespace EasyPoint.Application.Common.Abstractions;

public interface IUseCase
{
}

public interface IUseCase<TRequest, TResponse> : IUseCase
{
    TResponse Handler(TRequest request);
}
