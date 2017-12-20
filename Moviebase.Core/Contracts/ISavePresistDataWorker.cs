using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface ISavePresistDataWorker : IWorker
    {
        List<MovieEntryFacade> SaveItems { get; set; }
    }
}
