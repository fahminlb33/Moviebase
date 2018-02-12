using System;
using System.Threading;

namespace Moviebase.Core.MVP
{
    public class PercentageCalculator
    {
        private int _total, _current;

        public PercentageCalculator(int total)
        {
            _total = total;
        }

        public void Update(int count)
        {
            Interlocked.Exchange(ref _current, count);
        }

        public int Increment()
        {
            Interlocked.Increment(ref _current);
            return ToPercentage();
        }

        public int ToPercentage()
        {
            return Convert.ToInt32((double) Interlocked.CompareExchange(ref _current, 0, 0) / _total * 100);
        }
    }
}
