using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface ICsvExportWorker : INonReturningWorker
    {
        List<MovieEntryFacade> Movies { get; set; }
        string OutputPath { get; set; }
    }
}
