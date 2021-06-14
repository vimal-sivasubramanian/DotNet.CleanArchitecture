using DotNet.EventSourcing.Core.Interfaces;
using DotNet.EventSourcing.Service.Application.Interfaces;
using Hangfire;
using Hangfire.States;
using MediatR;

namespace DotNet.EventSourcing.Service.Infrastructure.Services
{
    internal class HangfireBackgroundJobScheduler : IBackgroundJobScheduler
    {
        private readonly IBackgroundJobProcessor<IRequest> _jobProcessor;

        public HangfireBackgroundJobScheduler(IBackgroundJobProcessor<IRequest> jobProcessor) => _jobProcessor = jobProcessor;

        public void EnqueueWithHighPriority(string jobName, IRequest request)
        {
            var client = new BackgroundJobClient();
            var state = new EnqueuedState("high-priority");
            client.Create(() => _jobProcessor.Process(jobName, request), state);

        }

        public void Enqueue(string jobName, IRequest request)
        {
            var client = new BackgroundJobClient();
            var state = new EnqueuedState("normal-priority");
            client.Create(() => _jobProcessor.Process(jobName, request), state);
        }
    }
}
