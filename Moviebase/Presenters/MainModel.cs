using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Moviebase.Annotations;
using Moviebase.Entities;

namespace Moviebase.Presenters
{
    class MainModel : INotifyPropertyChanged
    { 
        private readonly SynchronizationContext _syncContext;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _cmdDirectoriesEnabled;
        private bool _cmdToolsEnabled;
        private bool _cmdActionsEnabled;
        private string _lblStatusText;
        private int _prgStatusValue;
        private ProgressBarStyle _prgStatusStyle;
        private string _lblPercentageText;
        private bool _cmdStopEnabled;

        public MainModel(SynchronizationContext context)
        {
            _syncContext = context;
            DataView = new BindingList<MovieEntryFacade>();

            InitializeValues();
        }

        private void InitializeValues()
        {
            CmdDirectoriesEnabled = true;
            CmdToolsEnabled = true;
            CmdActionsEnabled = true;
            CmdStopEnabled = false;

            LblStatusText = StringResources.ReadyText;
            PrgStatusValue = 0;
            PrgStatusStyle = ProgressBarStyle.Blocks;
            LblPercentageText = "0%";
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

        public BindingList<MovieEntryFacade> DataView { get; }

        public void Invoke(Action action)
        {
            _syncContext.Post(x => action.Invoke(), null);
        }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _syncContext.Post(d => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)), null);
        }
    }
}
