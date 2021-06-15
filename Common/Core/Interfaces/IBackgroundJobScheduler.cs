namespace DotNet.CleanArchitecture.Core.Interfaces
{
    public interface IBackgroundJobScheduler<in T>
    {
        void Enqueue(string jobName, T request);

        void EnqueueWithHighPriority(string jobName, T request);
    }
}
