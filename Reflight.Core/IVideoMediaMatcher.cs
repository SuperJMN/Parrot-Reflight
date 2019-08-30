using System;
using System.Collections.Generic;
using NodaTime;

namespace Reflight.Core
{
    public interface IVideoMediaMatcher
    {
        IObservable<string> RecordingsBetween(Interval interval);
    }
}