using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface ISavePresistDataWorker : INonReturningWorker
    {
        List<MovieEntry> SaveItems { get; set; }
    }
}
