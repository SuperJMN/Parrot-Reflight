using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NodaTime;
using Reflight.Core;

namespace Reflight.Universal
{
    public class UwpContentMatcher : IVideoMediaMatcher
    {
        public IObservable<string> Lookup(Interval interval, IList<string> folders)
        {
            return Observable.Return(@"E:\Local\Parrot\Anafi\P1950198.MP4");
        }
    }
}