using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Moviebase.Core;
using Moviebase.Core.MVP;
using Moviebase.Core.Natives;
using Moviebase.Entities;
using Ninject.Extensions.Interception.Attributes;

namespace Moviebase.Models
{
    [NotifyOfChanges]
    public class MainModel : ModelBase
    { 
        public MainModel(SynchronizationContext context) : base(context)
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            DataView = new BindingList<MovieEntry>();

            PicPosterImage = Commons.DefaultImage();
            LblTitleText = Strings.LiteralThreeDots;
            LblExtraInfoText = Strings.LiteralThreeDots;
            LblPlotText = Strings.LiteralThreeDots;
            
            CmdDirectoriesEnabled = true;
            CmdToolsEnabled = true;
            CmdActionsEnabled = true;
            CmdStopEnabled = false;

            LblStatusText = Strings.LiteralReadyText;
            PrgStatusValue = 0;
            PrgStatusStyle = ProgressBarStyle.Blocks;
            LblPercentageText = "0%";
        }

        [DoNotNotifyOfChanges]
        public BindingList<MovieEntry> DataView { get; private set; }

        public virtual bool GridViewEnabled { get; set; }
        
        public virtual string LblTitleText { get; set; }

        public virtual string LblExtraInfoText { get; set; }

        public virtual string LblPlotText { get; set; }

        public virtual Image PicPosterImage { get; set; }

        public virtual bool CmdDirectoriesEnabled { get; set; }

        public virtual bool CmdToolsEnabled { get; set; }
        
        public virtual bool CmdActionsEnabled { get; set; }

        public virtual bool CmdStopEnabled { get; set; }

        public virtual string LblStatusText { get; set; }

        public virtual int PrgStatusValue { get; set; }

        public virtual ProgressBarStyle PrgStatusStyle { get; set; }

        public virtual string LblPercentageText { get; set; }
    }
}
