using System;

namespace XamarinMovies.Common.Services
{
    public interface IDialogService
    {
        IObservable<DialogResult> ShowMessageDialog(string content, string positive = null, string negative = null);
    }

    public enum DialogResult
    {
        Positive = 1,
        Neutral,
        Negative,
        Error
    }
}