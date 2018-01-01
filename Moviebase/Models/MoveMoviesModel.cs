using System.ComponentModel;
using System.Threading;
using BlastMVP;
using Moviebase.Entities;

namespace Moviebase.Models
{
    sealed class MoveMoviesModel : ModelBase
    {
        private string _lblCountText;
        private string _txtBrowseText;
        private string _cmdExecuteText;
        private bool _cmdExecuteEnabled;
        private bool _cmdBrowseEnabled;

        public MoveMoviesModel(SynchronizationContext context) : base(context)
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            DataView = new BindingList<MovedMovieEntry>();
            LblCountText = "Count: ";
            CmdExecuteText = StringResources.LiteralMoveText;
            CmdExecuteEnabled = true;
            CmdBrowseEnabled = true;
        }

        public BindingList<MovedMovieEntry> DataView { get; private set; }

        public string LblCountText
        {
            get => _lblCountText;
            set
            {
                if (value == _lblCountText) return;
                _lblCountText = value;
                OnPropertyChanged();
            }
        }

        public string TxtBrowseText
        {
            get => _txtBrowseText;
            set
            {
                if (value == _txtBrowseText) return;
                _txtBrowseText = value;
                OnPropertyChanged();
            }
        }

        public string CmdExecuteText
        {
            get => _cmdExecuteText;
            set
            {
                if (value == _cmdExecuteText) return;
                _cmdExecuteText = value;
                OnPropertyChanged();
            }
        }

        public bool CmdExecuteEnabled
        {
            get => _cmdExecuteEnabled;
            set
            {
                if (value == _cmdExecuteEnabled) return;
                _cmdExecuteEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool CmdBrowseEnabled
        {
            get => _cmdBrowseEnabled;
            set
            {
                if (value == _cmdBrowseEnabled) return;
                _cmdBrowseEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}
