using System.Threading;
using Moviebase.Core.MVP;
using Moviebase.Entities;
using Ninject.Extensions.Interception.Attributes;

namespace Moviebase.Models
{
    [NotifyOfChanges]
    public class SelectPosterModel : ModelBase
    {
        public SelectPosterModel(SynchronizationContext context) : base(context)
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            LblStatusText = StringResources.LiteralDownloadingText;
            PrgStatusValue = 0;
        }

        public virtual string LblStatusText { get; set; }

        public virtual int PrgStatusValue { get; set; }
    }
}
