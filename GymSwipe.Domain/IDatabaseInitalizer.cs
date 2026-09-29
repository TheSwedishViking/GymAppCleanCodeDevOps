using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain
{
    public interface IDatabaseInitalizer
    {
        Task InitalizeAsync(CancellationToken ct = default);
    }
}
