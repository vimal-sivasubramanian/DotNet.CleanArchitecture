using MediatR;

namespace DotNet.EventSourcing.Service.Application.Interfaces
{
    public interface IBackgroundJobScheduler
    {
        void Enqueue(string jobName, IRequest request);

        void EnqueueWithHighPriority(string jobName, IRequest request);
    }
}
