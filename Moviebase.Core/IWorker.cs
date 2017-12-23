﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moviebase.Core
{
    public interface IWorker
    {
        IEnumerable<Task> CreateTasks();
    }
}
