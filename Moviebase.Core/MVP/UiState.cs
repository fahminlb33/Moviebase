using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviebase.Core.MVP
{
    public enum UiState
    {
        Ready,
        Working,
        Cancelling,
        StatusUpdate,
    }
}
