using System;
using NodaTime;
using NodaTime.Extensions;

namespace Reflight.Core
{
    public static class IntervalMixin
    {
        public static bool Contains(this Interval subject, Interval container)
        {
            return subject.Contains(container.Start) && subject.Contains(container.End);
        }

        public static Interval ToInterval(this DateTimeOffset start, TimeSpan span)
        {
            return new Interval(start.ToInstant(), start.ToInstant().Plus(span.ToDuration()));
        }
    }
}