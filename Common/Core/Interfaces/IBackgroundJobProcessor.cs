namespace DotNet.CleanArchitecture.Core.Interfaces
{
    public interface IBackgroundJobProcessor<in T>
    {
        void Process(string jobName, T jobRequest);
    }
}
