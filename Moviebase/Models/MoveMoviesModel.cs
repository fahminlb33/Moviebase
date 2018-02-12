using System.ComponentModel;
using System.Threading;
using Moviebase.Core.MVP;
using Moviebase.Entities;
using Ninject.Extensions.Interception.Attributes;

namespace Moviebase.Models
{
    [NotifyOfChanges]
    public class MoveMoviesModel : ModelBase
    {
        public MoveMoviesModel(SynchronizationContext context) : base(context)
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            DataView = new BindingList<MovedMovieEntry>();
            LblCountText = "Count: ";
            CmdExecuteText = Strings.LiteralMoveText;
            CmdExecuteEnabled = true;
            CmdBrowseEnabled = true;
        }

        [DoNotNotifyOfChanges]
        public BindingList<MovedMovieEntry> DataView { get; private set; }

        public virtual string LblCountText { get; set; }

        public virtual string TxtBrowseText { get; set; }

        public virtual string CmdExecuteText { get; set; }

        public virtual bool CmdExecuteEnabled { get; set; }

        public virtual bool CmdBrowseEnabled { get; set; }
    }
}
