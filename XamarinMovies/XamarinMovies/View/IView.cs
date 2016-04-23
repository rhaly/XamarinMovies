namespace XamarinMovies.Common.View
{
    public interface IView
    {
        void CloseView();

        bool IsVisible { get; }
    }
}