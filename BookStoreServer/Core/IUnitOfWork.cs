namespace BookStoreServer.Core;

public interface IUnitOfWork
{
    Task<int> CompleteAsync();
}