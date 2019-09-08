
using NUnit.Framework;

namespace Tests
{
  // Tips:
  // 1. Refer more information about delegate from in TEST/CLR
  // 2. event vs delegate https://dzone.com/articles/event-vs-delegate
  public class DelegateTest
  {
    private delegate int Transform(int input);

    private static int Transform1(int input) { return input + 1; }

    private int Transform2(int input) { return input * 3; }

    [Test]
    public void DelegateMulticastDelegate()
    {
      Transform t = new Transform((int input) => { return input * 2; });

      System.Type type = t.GetType();
      Assert.That("Transform", Is.EqualTo(type.Name));
      type = type.BaseType;
      Assert.That("MulticastDelegate", Is.EqualTo(type.Name));

      // Invoke is created by C# compiler
      Assert.That(4, Is.EqualTo(t.Invoke(2)));
    }

    [Test]
    public void DelegateSyntacticSugar()
    {
      Transform t = new Transform(Transform1);
      Assert.That(3, Is.EqualTo(t(2)));

      t += Transform2;
      Assert.That(6, Is.EqualTo(t(2)), "the final result is returned");

      int ret = 0;
      Transform t2;
      foreach(var x in t.GetInvocationList())
      {
        Assert.That(x is Transform, Is.True);
        t2 = (Transform)x;
        ret += t2(2);
      }
      Assert.That(9, Is.EqualTo(ret));

      t -= Transform2;
      Assert.That(3, Is.EqualTo(t(2)));
    }

    [Test]
    public void TestEventBasics()
    {
      Assert.That(false, Is.True);
    }
  }
}
