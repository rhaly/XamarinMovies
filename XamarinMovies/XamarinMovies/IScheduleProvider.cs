using System.Reactive.Concurrency;
using ReactiveUI;

namespace XamarinMovies.Common
{
    public interface IScheduleProvider
    {
        IScheduler CurrentThread { get; }

        IScheduler UiScheduler { get; }

        IScheduler Immediate { get; }

        IScheduler NewThread { get; }

        IScheduler TaskPool { get; }
    }

    public sealed class ScheduleProvider : IScheduleProvider
    {
        public IScheduler CurrentThread => Scheduler.CurrentThread;

        public IScheduler UiScheduler => RxApp.MainThreadScheduler;

        public IScheduler Immediate => Scheduler.Immediate;

        public IScheduler NewThread => NewThreadScheduler.Default;

        public IScheduler TaskPool => RxApp.TaskpoolScheduler;
    }
}