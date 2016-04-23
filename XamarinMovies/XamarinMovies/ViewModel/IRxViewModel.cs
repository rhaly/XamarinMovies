using XamarinMovies.Common.Model;
using XamarinMovies.Common.View;

namespace XamarinMovies.Common.ViewModel
{
    public interface IRxViewModel : IBaseModel
    {
        void ClearSubscriptions();

        IView View { get; set; }

        void OnNavigatedFrom();

        void OnNavigatedTo();

    }
}