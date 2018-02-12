using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Moviebase.Core.MVP
{
    public static class DataBinderExtensions
    {
        public static IBindingTarget Bind<T, TP>(this T ctl, Expression<Func<T, TP>> action)
            where T : IBindableComponent
        {
            var expr = (MemberExpression) action.Body;
            return new BindingTarget
            {
                PropertyName = expr.Member.Name,
                Control = ctl
            };
        }

        public static void To<T, TP>(this IBindingTarget target, T model, Expression<Func<T, TP>> action)
            where T : class
        {
            var expr = (MemberExpression) action.Body;
            target.Control.DataBindings.Add(target.PropertyName, model, expr.Member.Name);
        }
    }
}
