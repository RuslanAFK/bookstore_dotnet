namespace Domain.Abstractions;

public interface IUnitOfWork
{
    Task<bool> CompleteAsync();
}