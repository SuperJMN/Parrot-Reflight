using System;
using System.Collections.Generic;
using NodaTime;

namespace Reflight.Core
{
    public interface IMediaStore
    {
        IObservable<IFile> RecordingsBetween(Interval interval);
    }
}