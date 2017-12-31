using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface ICsvExportWorker : INonReturningWorker
    {
        List<MovieEntry> Movies { get; set; }
        string OutputPath { get; set; }
    }
}
