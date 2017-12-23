using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface ISavePresistDataWorker : INonReturningWorker
    {
        List<MovieEntryFacade> SaveItems { get; set; }
    }
}
