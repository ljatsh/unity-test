using System;
using NUnit.Framework;

// TODO. Ulps https://nunit.org/docs/2.5/equalConstraint.html
// TODO. Single precision in ToString

namespace CSharp
{
  public class PrimitiveTest
  {
    // A Test behaves as an ordinary method
    [Test]
    public void TestConvert2Single()
    {
      // Convert string to single, the logic is different from atof
      Assert.That(Convert.ToSingle("100.3"), Is.EqualTo(100.3f).Within(0.01).Percent);
      Assert.That(Convert.ToSingle("3.402823E+38"), Is.EqualTo(Single.MaxValue).Within(0.01).Percent);

      Assert.Throws<FormatException>(()=> { Convert.ToSingle("a100.3"); });
      Assert.Throws<FormatException>(()=> { Convert.ToSingle("100.3a"); });
      Assert.Throws<OverflowException>(()=> { Convert.ToSingle("3.402824E+38"); });

      // Convert integer to single
      Assert.That(Convert.ToSingle(100), Is.EqualTo(100f).Within(0.01).Percent);
    }

    [Test]
    public void TestSingle2Other() {
      // Convert single to integer
      // rounded, the middle number is truncated to event number
      Assert.That(Convert.ToInt32(3.4f), Is.EqualTo(3));
      Assert.That(Convert.ToInt32(3.6f), Is.EqualTo(4));
      Assert.That(Convert.ToInt32(-3.4f), Is.EqualTo(-3));
      Assert.That(Convert.ToInt32(-3.6f), Is.EqualTo(-4));

      Assert.That(Convert.ToInt32(3.5f), Is.EqualTo(4));
      Assert.That(Convert.ToInt32(4.5f), Is.EqualTo(4));
      Assert.That(Convert.ToInt32(-3.5f), Is.EqualTo(-4));
      Assert.That(Convert.ToInt32(-4.5f), Is.EqualTo(-4));

      // Convert single to string
      Assert.That(Convert.ToString(3.00001f), Is.EqualTo("3.00001"));
      Assert.That(Convert.ToString(3.000010f), Is.EqualTo("3.00001"));
    }
  }
}
