using MediatR;

namespace DotNet.CleanArchitecture.Service.Application.Interfaces
{
    public interface IBackgroundJobScheduler
    {
        void Enqueue(string jobName, IRequest request);

        void EnqueueWithHighPriority(string jobName, IRequest request);
    }
}
