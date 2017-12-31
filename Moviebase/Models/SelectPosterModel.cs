using System.Threading;
using BlastMVP;
using Moviebase.Entities;

namespace Moviebase.Models
{
    class SelectPosterModel : ModelBase
    {
        private string _lblStatusText;
        private int _prgStatusValue;

        public SelectPosterModel(SynchronizationContext context) : base(context)
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            LblStatusText = StringResources.LiteralDownloadingText;
            PrgStatusValue = 0;
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
    }
}
