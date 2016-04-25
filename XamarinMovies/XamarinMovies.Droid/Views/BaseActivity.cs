using Android.Support.V7.App;
using XamarinMovies.Common.View;

namespace XamarinMovies.Droid.Views
{
    public class BaseActivity : AppCompatActivity, IView
    {
        public static BaseActivity CurrentActivity { get; set; }

        protected override void OnPause()
        {
            IsVisible = false;
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            IsVisible = true;
        }

        public void CloseView()
        {
            Finish();
        }

        public bool IsVisible { get; private set; }
    }
}