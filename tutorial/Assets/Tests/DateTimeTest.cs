using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// 1. time unit in DateTime is Tick(10000 Ticks per millisecond). 0 tick is not absolute time!

// TODO:
// 1. Some TimeZone can cause some tests failed.
// 2. DateTime Format
// 3. Conversation between local time and utc time (https://docs.microsoft.com/en-us/dotnet/api/system.timezoneinfo.converttime?view=netcore-2.2)

namespace Tests {
  public class DateTimeTest {
    [SetUp]
    public void SetUp() {

    }

    // Helper Functions
    private string _FormatDateTime(DateTime dt) {
      return string.Format("{0} Kind = {1}", dt.ToString("@yyy/M/d HH:mm:ss tt"), dt.Kind);
    }

    [Test]
    public void TestDateTimeUnit() {
      // Tick Unit
      var d1 = new DateTime(2019, 6, 28, 16, 0, 2, 10);
      var d2 = new DateTime(2019, 6, 28, 16, 0, 2, 11);

      Assert.That(10000, Is.EqualTo(d2.Ticks - d1.Ticks));
      Assert.That(10000, Is.EqualTo(TimeSpan.TicksPerMillisecond));

      // Tick 0 is not abosolute
      var now = DateTime.Now;
      var nowUtc = DateTime.UtcNow;
      Assert.That(now.Ticks, Is.Not.EqualTo(nowUtc.Ticks));
    }

    [Test]
    public void TestDateTimeUsage() {
      var d1 = new DateTime(2019, 6, 28, 16, 0, 2, 10, DateTimeKind.Local);
      var d2 = new DateTime(d1.Ticks, DateTimeKind.Utc);
      Assert.That(d1, Is.EqualTo(d2));

      Assert.That(2019, Is.EqualTo(d1.Year));
      Assert.That(6, Is.EqualTo(d1.Month));
      Assert.That(28, Is.EqualTo(d1.Day));
      Assert.That(16, Is.EqualTo(d1.Hour));
      Assert.That(0, Is.EqualTo(d1.Minute));
      Assert.That(2, Is.EqualTo(d1.Second));
      Assert.That(10, Is.EqualTo(d1.Millisecond));
     }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DateTimeTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
  }
}
