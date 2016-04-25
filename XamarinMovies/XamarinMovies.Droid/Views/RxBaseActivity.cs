using System;
using System.Reactive.Disposables;
using Android.OS;
using ReactiveUI;
using XamarinMovies.Common.ViewModel;

namespace XamarinMovies.Droid.Views
{
    public class RxBaseActivity<T> : BaseActivity, IViewFor<T> where T : class, IRxViewModel
    {        

        private CompositeDisposable _subscriptions;

        private T _viewModel;

        public T ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        object IViewFor.ViewModel
        {
            get { return _viewModel; }

            set { _viewModel = (T)value; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            _subscriptions = new CompositeDisposable();

            base.OnCreate(bundle);
        }

        protected void AddSubscription(IDisposable subscription)
        {
            if (_subscriptions == null)
                _subscriptions = new CompositeDisposable();
            _subscriptions.Add(subscription);
        }

        protected override void OnDestroy()
        {
            if (_subscriptions != null)
            {
                _subscriptions.Dispose();
                _subscriptions = null;
            }
            ViewModel?.ClearSubscriptions();
            base.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();
            ViewModel?.OnNavigatedTo();
        }

        protected override void OnPause()
        {
            base.OnPause();
            ViewModel?.OnNavigatedFrom();
        }
    }
}