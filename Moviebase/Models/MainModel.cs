using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Core;
using Moviebase.Entities;

namespace Moviebase.Models
{
    sealed class MainModel : ModelBase
    { 
        private bool _cmdDirectoriesEnabled;
        private bool _cmdToolsEnabled;
        private bool _cmdActionsEnabled;
        private string _lblStatusText;
        private int _prgStatusValue;
        private ProgressBarStyle _prgStatusStyle;
        private string _lblPercentageText;
        private bool _cmdStopEnabled;
        private Image _picPosterImage;

        public MainModel(SynchronizationContext context) : base(context)
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            DataView = new BindingList<MovieEntry>();

            PicPosterImage = Commons.DefaultImage;
            LblTitleText = StringResources.LiteralThreeDots;
            LblExtraInfoText = StringResources.LiteralThreeDots;
            LblPlotText = StringResources.LiteralThreeDots;
            
            CmdDirectoriesEnabled = true;
            CmdToolsEnabled = true;
            CmdActionsEnabled = true;
            CmdStopEnabled = false;

            LblStatusText = StringResources.LiteralReadyText;
            PrgStatusValue = 0;
            PrgStatusStyle = ProgressBarStyle.Blocks;
            LblPercentageText = "0%";
        }

        public BindingList<MovieEntry> DataView { get; private set; }

        public string LblTitleText { get; set; }

        public string LblExtraInfoText { get; set; }

        public string LblPlotText { get; set; }
        
        public Image PicPosterImage
        {
            get => _picPosterImage;
            set
            {
                if (value == _picPosterImage) return;
                _picPosterImage = value;
                OnPropertyChanged();
            }
        }


        public bool CmdDirectoriesEnabled
        {
            get => _cmdDirectoriesEnabled;
            set
            {
                if (value == _cmdDirectoriesEnabled) return;
                _cmdDirectoriesEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool CmdToolsEnabled
        {
            get => _cmdToolsEnabled;
            set
            {
                if (value == _cmdToolsEnabled) return;
                _cmdToolsEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool CmdActionsEnabled
        {
            get => _cmdActionsEnabled;
            set
            {
                if (value == _cmdActionsEnabled) return;
                _cmdActionsEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool CmdStopEnabled
        {
            get => _cmdStopEnabled;
            set
            {
                if (value == _cmdStopEnabled) return;
                _cmdStopEnabled = value;
                OnPropertyChanged();
            }
        }

        public string LblStatusText
        {
            get => _lblStatusText;
            set
            {
                if (value == _lblStatusText) return;
                _lblStatusText = value;
                OnPropertyChanged();
            }
        }

        public int PrgStatusValue
        {
            get => _prgStatusValue;
            set
            {
                if (value == _prgStatusValue) return;
                _prgStatusValue = value;
                OnPropertyChanged();
            }
        }

        public ProgressBarStyle PrgStatusStyle
        {
            get => _prgStatusStyle;
            set
            {
                if (value == _prgStatusStyle) return;
                _prgStatusStyle = value;
                OnPropertyChanged();
            }
        }

        public string LblPercentageText
        {
            get => _lblPercentageText;
            set
            {
                if (value == _lblPercentageText) return;
                _lblPercentageText = value;
                OnPropertyChanged();
            }
        }
    }
}
