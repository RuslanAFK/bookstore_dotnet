using Domain.Abstractions;
using Domain.Exceptions;

namespace Services
{
    public class BaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected async Task CompleteAndCheckIfCompleted()
        {
            var isCompleted = await _unitOfWork.CompleteAsync();
            if (!isCompleted)
            {
                string methodName = 
                    (new System.Diagnostics.StackTrace()).GetFrame(1)?.GetMethod()?.Name ?? "Operation";
                throw new OperationNotSuccessfulException(methodName);

            }
        }
    }
}
