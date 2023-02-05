namespace BookStoreServer.Core.Services;

public interface IUnitOfWork
{
    Task<int> CompleteAsync();
}