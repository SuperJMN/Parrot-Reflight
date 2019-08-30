using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using NodaTime.Extensions;
using Xunit;

namespace Reflight.Core.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var fileSystem = new[]
            {
                (@"c:\Folder1\Video1.mp4", new DateTimeOffset(2000, 1, 1, 12, 0, 0, 0, TimeSpan.Zero), 10000ul),
                (@"c:\Folder1\Video2.mp4", new DateTimeOffset(2000, 1, 1, 13, 0, 0, 0, TimeSpan.Zero), 10000ul),
                (@"c:\Folder2\Video3.mp4", new DateTimeOffset(2000, 1, 1, 14, 0, 0, 0, TimeSpan.Zero), 10000ul),
                (@"c:\Folder2\Video4.mp4", new DateTimeOffset(2000, 1, 1, 15, 0, 0, 0, TimeSpan.Zero), 10000ul),
                (@"c:\Folder2\Video5.mp4", (DateTimeOffset?)null, 10000ul),
            };

            var sut = new MediaStore(new TestFileSystem(fileSystem));
            var start = new DateTimeOffset(2000, 1, 1, 10, 0, 0, 0, TimeSpan.Zero);
            var end = new DateTimeOffset(2000, 1, 1, 14, 1, 0, 0, TimeSpan.Zero);
            var interval = new Interval(start.ToInstant(), end.ToInstant());
            var matches = await sut.RecordingsBetween(interval).ToList();

            var expected = new[]
            {
                @"c:\Folder1\Video1.mp4",
                @"c:\Folder1\Video2.mp4",
                @"c:\Folder2\Video3.mp4",
            };

            matches.Should().BeEquivalentTo(expected);
        }
    }
}
