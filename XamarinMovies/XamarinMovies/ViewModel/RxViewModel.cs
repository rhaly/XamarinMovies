using System;
using System.Reactive.Disposables;
using XamarinMovies.Common.Model;
using XamarinMovies.Common.View;

namespace XamarinMovies.Common.ViewModel
{
    public abstract class RxViewModel : BaseModel, IRxViewModel
    {
        private CompositeDisposable _subscriptions = new CompositeDisposable();

        protected void AddSubscription(IDisposable subscription)
        {
            if (_subscriptions == null)
                _subscriptions = new CompositeDisposable();
            _subscriptions.Add(subscription);
        }


        public void ClearSubscriptions()
        {
            if (_subscriptions != null)
            {
                _subscriptions.Dispose();
                _subscriptions = null;
            }
        }

        private WeakReference<IView> _view;

        public IView View
        {
            get
            {
                IView view;
                _view.TryGetTarget(out view);
                return view;

            }
            set
            {
                if (value == null)
                {
                    _view = null;
                    return;
                }

                _view = new WeakReference<IView>(value);
            }
        }

        public virtual void OnNavigatedFrom(){}

        public virtual void OnNavigatedTo(){}
    }
}