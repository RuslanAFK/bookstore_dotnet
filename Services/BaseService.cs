using Domain.Abstractions;

namespace Services;

public abstract class BaseService
{
    private readonly IUnitOfWork _unitOfWork;

    protected BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    protected async Task CompleteAndCheckIfCompleted()
    {
        await _unitOfWork.CompleteOrThrowAsync();
    }
}