using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Ninject.Extensions.Interception;

namespace Moviebase.Core.MVP
{
    public class ModelBase : IAutoNotifyPropertyChanged
    {
        protected SynchronizationContext Context;
        public event PropertyChangedEventHandler PropertyChanged;

        public ModelBase(SynchronizationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Invoke(Action invoke)
        {
            if (invoke == null) throw new ArgumentNullException(nameof(invoke));
            Context.Send(_ => invoke.Invoke(), null);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Context.Send(x => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)), null);
        }
    }
}
