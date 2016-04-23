using System;
using ReactiveUI;

namespace XamarinMovies.Common.Model
{
    public interface IBaseModel : IReactiveObject
    {
        IObservable<string> PropertyChangedStream { get; }

        IObservable<Exception> ThrownExceptions { get; }

    }
}