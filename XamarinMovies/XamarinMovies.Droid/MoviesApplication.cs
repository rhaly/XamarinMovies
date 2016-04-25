using System;
using System.ComponentModel;
using Android.App;
using Android.Runtime;
using Ninject;
using XamarinMovies.Common;

namespace XamarinMovies.Droid
{
    [Application(Theme = "@style/AppTheme")]
    public class MoviesApplication : Application
    {
        public MoviesApplication(IntPtr javaReference, JniHandleOwnership transfer): base(javaReference, transfer)
        {
        }

        public MoviesApplication()
        {
        }

        public override void OnCreate()
        {
            var kernel = new StandardKernel(new CommonModule(), new DroidModule());
            Container = kernel;
            base.OnCreate();
        }

        public static IKernel Container { get; set; }
    }
}