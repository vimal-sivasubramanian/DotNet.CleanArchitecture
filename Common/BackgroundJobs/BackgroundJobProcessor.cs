using DotNet.CleanArchitecture.Core;
using DotNet.CleanArchitecture.Core.Interfaces;
using Hangfire;
using MediatR;

namespace DotNet.CleanArchitecture.Common.BackgroundJobs
{
    internal class BackgroundJobProcessor : IBackgroundJobProcessor<IRequest>
    {
        private readonly IMediator _mediator;

        public BackgroundJobProcessor(IMediator mediator) => _mediator = mediator;

        [JobDisplayName("{0}")]
        [AutomaticRetry(Attempts = 3)]
        public void Process(string jobName, IRequest jobRequest)
        {
            _mediator.Send(jobRequest).SafeResult();
        }
    }
}
