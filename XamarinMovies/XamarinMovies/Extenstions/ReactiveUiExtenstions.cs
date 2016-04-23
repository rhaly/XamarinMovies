using System;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace ReactiveUI
{
    public static class ReactiveUiExtenstions
    {
        public static IObservable<TValue> ObservableFor<TSender, TValue>(this TSender This,
            Expression<Func<TSender, TValue>> property, bool beforeChange = false, bool skipInitial = true)
        {
            return This.ObservableForProperty(property, beforeChange, skipInitial)
                .Where(v => v != null)
                .Select(v => v.Value);
        }
    }
}