using System;
using System.Reactive.Linq;
using XamarinMovies.Common.Services;
using XamarinMovies.Droid.Views;

namespace XamarinMovies.Droid.Services
{
    public class DialogService : IDialogService
    {
        public IObservable<DialogResult> ShowMessageDialog(string content, string positive = null, string negative = null)
        {
            var currentActivity = BaseActivity.CurrentActivity;
            if(currentActivity == null)
                return Observable.Empty<DialogResult>();
            MessageDialogFragment mdf = currentActivity.FragmentManager.FindFragmentByTag(MessageDialogFragment.Tag) as MessageDialogFragment;

            if (mdf == null && currentActivity.IsVisible)
            {
                mdf = MessageDialogFragment.NewInstance(content, positive, negative);
                mdf.Show(currentActivity.FragmentManager, MessageDialogFragment.Tag);
                return mdf.ResultStream;
            }
            return Observable.Empty<DialogResult>();
        }
    }
}