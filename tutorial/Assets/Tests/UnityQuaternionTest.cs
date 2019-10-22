using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnityTest {
  public class UnityQuaternionTest
  {
    // A Test behaves as an ordinary method
    [Test]
    public void TestQuaternion() {
        Assert.That(true, Is.False);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator UnityQuaternionTestWithEnumeratorPasses() {
      // Use the Assert class to test conditions.
      // Use yield to skip a frame.
      yield return null;
    }
  }
}
