using DotNet.CleanArchitecture.Core.Interfaces;
using Hangfire;
using Hangfire.States;
using MediatR;

namespace DotNet.CleanArchitecture.Common.BackgroundJobs
{
    internal class BackgroundJobScheduler : IBackgroundJobScheduler<IRequest>
    {
        private readonly IBackgroundJobProcessor<IRequest> _jobProcessor;

        public BackgroundJobScheduler(IBackgroundJobProcessor<IRequest> jobProcessor) => _jobProcessor = jobProcessor;

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
