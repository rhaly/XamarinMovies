using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;

namespace XamarinMovies.Common.Model
{
    public class BaseModel : ReactiveObject, IBaseModel
    {
        private readonly Subject<string> _propertyChangedSubject = new Subject<string>();

        protected BaseModel()
        {
            Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => PropertyChanged += h,
                    h => PropertyChanged -= h)
                .Select(x => x.EventArgs.PropertyName).Subscribe(p =>
                {
                    _propertyChangedSubject.OnNext(p);
                });
        }

        public IObservable<string> PropertyChangedStream => _propertyChangedSubject.AsObservable();
    }
}