using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.App;
using Android.OS;
using Android.Runtime;
using XamarinMovies.Common.Services;
using Com.Afollestad.Materialdialogs;


namespace XamarinMovies.Droid.Views
{
    public class MessageDialogFragment : DialogFragment
    {
        public new const string Tag = "MessageDialogFragment";

        public const string ArgCustomViewResId = "custom_view_res_id";
        public const string ArgContentResId = "content_res_id";
        public const string ArgContent = "content";
        public const string ArgPositiveResId = "positive_res_id";
        public const string ArgPositive = "positive";
        public const string ArgNegativeResId = "negative_res_id";
        public const string ArgNegative = "negative";

        private readonly ButtonCallback _resultCallback;
        private bool _dismissOnResume;

        public IObservable<DialogResult> ResultStream
        {
            get { return _resultCallback != null ? _resultCallback.ResultStream : Observable.Empty<DialogResult>(); }
        }

        public MessageDialogFragment()
        {
            _resultCallback = new ButtonCallback();
        }

        public static MessageDialogFragment NewInstance(int content, int positiveResId, int negativeResId)
        {
            Bundle args = new Bundle();
            args.PutInt(ArgContentResId, content);
            args.PutInt(ArgPositiveResId, positiveResId);
            args.PutInt(ArgNegativeResId, negativeResId);

            MessageDialogFragment pdf = new MessageDialogFragment { Arguments = args };
            return pdf;
        }

        public static MessageDialogFragment NewInstance(string content, int positiveResId, int negativeResId)
        {
            Bundle args = new Bundle();
            args.PutString(ArgContent, content);
            args.PutInt(ArgPositiveResId, positiveResId);
            args.PutInt(ArgNegativeResId, negativeResId);

            MessageDialogFragment pdf = new MessageDialogFragment { Arguments = args };
            return pdf;
        }

        public static MessageDialogFragment NewInstance(string content, string positive, string negative)
        {
            Bundle args = new Bundle();
            args.PutString(ArgContent, content);
            args.PutString(ArgPositive, positive);
            args.PutString(ArgNegative, negative);

            MessageDialogFragment pdf = new MessageDialogFragment { Arguments = args };
            return pdf;
        }


        public static MessageDialogFragment NewInstanceCustomView(int customViewResId, int positiveResId, int negativeResId)
        {
            Bundle args = new Bundle();
            args.PutInt(ArgCustomViewResId, customViewResId);
            args.PutInt(ArgPositiveResId, positiveResId);
            args.PutInt(ArgNegativeResId, negativeResId);

            MessageDialogFragment pdf = new MessageDialogFragment { Arguments = args };
            return pdf;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            RetainInstance = true;
            base.OnCreate(savedInstanceState);
        }

        public override void Show(FragmentManager manager, string tag)
        {
            try
            {
                base.Show(manager, tag);
            }
            catch (Exception e)
            {
                _resultCallback.OnError(e);
            }
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            int contentResId = Arguments.GetInt(ArgContentResId, -1);
            int positiveResId = Arguments.GetInt(ArgPositiveResId, -1);
            int negativeResId = Arguments.GetInt(ArgNegativeResId, -1);
            var content = contentResId == -1 ? Arguments.GetString(ArgContent) : Activity.GetString(contentResId);
            int customViewResId = Arguments.GetInt(ArgCustomViewResId, -1);
            string positiveText = positiveResId == -1 ? Arguments.GetString(ArgPositive, null) : Activity.GetString(positiveResId);
            string negativeText = negativeResId == -1 ? Arguments.GetString(ArgNegative, null) : Activity.GetString(negativeResId);
            Cancelable = false;

            var builer = new MaterialDialog.Builder(Activity);
            if (customViewResId == -1)
            {
                builer.Content(content);
            }
            else
            {
                builer.CustomView(customViewResId, true);
            }

            builer
                .BackgroundColorRes(Resource.Color.dialog_background)
                .ContentColorRes(Resource.Color.dialog_content);
            if (!string.IsNullOrEmpty(positiveText))
            {
                builer.PositiveText(positiveText)
                    .PositiveColorRes(Resource.Color.dialog_positive);
            }

            if (!string.IsNullOrEmpty(negativeText))
            {
                builer.NegativeText(negativeText)
                    .NegativeColorRes(Resource.Color.dialog_negative);
            }

            builer.Callback(_resultCallback);
            return builer.Build();
        }

        public override void OnDestroyView()
        {
            if (Dialog != null && RetainInstance)
            {
                Dialog.SetDismissMessage(null);
            }
            base.OnDestroyView();
        }

        public override void OnResume()
        {
            base.OnResume();

            //Dialog.Window.SetLayout((int) DisplayUtils.Dp2Px(278), ViewGroup.LayoutParams.WrapContent);

            if (_dismissOnResume)
            {
                _dismissOnResume = false;
                Dismiss();
            }
        }

        public override void Dismiss()
        {
            if (IsResumed)
            {
                base.Dismiss();
            }
            else
            {
                _dismissOnResume = true;
            }
        }

        private class ButtonCallback : MaterialDialog.ButtonCallback
        {
            private readonly ISubject<DialogResult> _resultSubject = new Subject<DialogResult>();

            public ButtonCallback()
            {
            }

            public ButtonCallback(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
            {
            }

            public override void OnPositive(MaterialDialog dlg)
            {
                NotifyResult(DialogResult.Positive);
            }

            public override void OnNegative(MaterialDialog dlg)
            {
                NotifyResult(DialogResult.Negative);
            }

            public override void OnNeutral(MaterialDialog dlg)
            {
                NotifyResult(DialogResult.Neutral);
            }

            private void NotifyResult(DialogResult result)
            {
                _resultSubject.OnNext(result);
            }

            public IObservable<DialogResult> ResultStream => _resultSubject.AsObservable();

            public void OnError(Exception e)
            {
                _resultSubject.OnError(e);
            }
        }
    }
}